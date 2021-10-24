using FluentValidation;
using System;

namespace Notes.Application.Notes.Queries.GetNoteDetails
{
    public class GetNoteDetailsValidator : AbstractValidator<GetNoteDetailsQuery>
    {
        public GetNoteDetailsValidator()
        {
            RuleFor(x => x.Id).NotEqual(Guid.Empty);
            RuleFor(x => x.UserId).NotEqual(Guid.Empty);
        }
    }
}
