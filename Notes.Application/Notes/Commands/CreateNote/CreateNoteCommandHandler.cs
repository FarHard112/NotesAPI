using MediatR;
using Notes.Application.Interfaces;
using Notes.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Notes.Application.Notes.Commands.CreateNote
{
    public class CreateNoteCommandHandler
        : IRequestHandler<CreateNoteCommand, Guid>
    {
        private readonly INotesDbContext context;

        public CreateNoteCommandHandler(INotesDbContext context)
        {
            this.context = context;
        }

        public async Task<Guid> Handle(CreateNoteCommand request,
            CancellationToken cancellationToken)
        {
            var note = new Note
            {
                UserId = request.UserId,
                Title = request.Title,
                Details = request.Details,
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                EditDate = null
            };
            await context.Notes.AddAsync(note, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return note.Id;
        }
    }
}
