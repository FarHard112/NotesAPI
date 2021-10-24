using AutoMapper;
using Notes.Application.Notes.Queries.GetNoteList;
using Notes.Persistance;
using Notest.Tests.Common;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Notest.Tests.Notes.Queries
{
    [Collection("QueryCollection")]
    public class GetNoteListQueryHandlerTests
    {
        private readonly NotesDbContext context;
        private readonly IMapper mapper;
        public GetNoteListQueryHandlerTests(QueryTestFixture fixture)
        {
            context = fixture.Context;
            mapper = fixture.Mapper;

        }

        [Fact]
        public async Task GetNoteListQueryHandler_Success()
        {
            //Arrange 
            var handler = new GetNoteListQueryHandler(context, mapper);

            //Acts
            var result = await handler.Handle(
                new GetNoteListQuery { UserId = NotesContextFactory.UserBId }
                , CancellationToken.None);

            //Assert
            result.ShouldBeOfType<NoteListVm>();
            result.Notes.Count.ShouldBe(2);


        }
    }
}
