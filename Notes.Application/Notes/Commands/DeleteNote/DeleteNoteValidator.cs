using FluentValidation;
using System;

namespace Notes.Application.Notes.Commands.DeleteNote
{
    public class DeleteNoteValidator : AbstractValidator<DeleteNoteCommand>
    {
        public DeleteNoteValidator()
        {
            RuleFor(x => x.Id).NotEqual(Guid.Empty);
            RuleFor(c => c.UserId).NotEqual(Guid.Empty);
        }
    }
}
