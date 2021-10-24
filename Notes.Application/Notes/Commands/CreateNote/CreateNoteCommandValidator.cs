using FluentValidation;
using System;

namespace Notes.Application.Notes.Commands.CreateNote
{
    public class CreateNoteCommandValidator : AbstractValidator<CreateNoteCommand>
    {
        public CreateNoteCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MinimumLength(256);
            RuleFor(x => x.UserId).NotEqual(Guid.Empty);

        }

    }
}
