using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Exceptions;
using Notes.Application.Interfaces;
using Notes.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace Notes.Application.Notes.Commands.UpdateNote
{
    public class UpdateNoteCommandHandler
        : IRequestHandler<UpdateNoteCommand>
    {

        public UpdateNoteCommandHandler(INotesDbContext context)
        {
            Context = context;
        }

        public INotesDbContext Context { get; }

        public async Task<Unit> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
        {

            var entity = await Context.Notes.FirstOrDefaultAsync
                (x => x.Id == request.Id, cancellationToken);
            if (entity == null || entity.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(Note), request.Id);
            }
            entity.Details = request.Details;
            entity.Title = request.Title;
            entity.EditDate = System.DateTime.Now;
            await Context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
