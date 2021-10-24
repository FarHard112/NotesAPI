using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Notes.Application.Notes.Queries.GetNoteList
{
    public class GetNoteListQueryHandler
        : IRequestHandler<GetNoteListQuery, NoteListVm>
    {
        private readonly INotesDbContext context;
        private readonly IMapper mapper;
        public GetNoteListQueryHandler(INotesDbContext context,
            IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<NoteListVm> Handle(GetNoteListQuery request, CancellationToken cancellationToken)
        {
            var notesQuery = await context.Notes.
                Where(note => note.UserId == request.UserId)
                .ProjectTo<NoteLookUpDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new NoteListVm { Notes = notesQuery };
        }

    }
}
