using MediatR;
using Notes.Application.Exceptions;
using Notes.Application.Interfaces;
using Notes.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace Notes.Application.Notes.Commands.DeleteNote
{
    public class DeleteNoteCommandHandler
        : IRequest<DeleteNoteCommand>
    {
        private readonly INotesDbContext context;

        public DeleteNoteCommandHandler(INotesDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
        {
            var entity =
                await context.Notes.FindAsync(new object[] { request.Id }, cancellationToken);
            if (entity == null || entity.Id != request.Id)
            {
                throw new NotFoundException(nameof(Note), request.Id);
            }
            context.Notes.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
