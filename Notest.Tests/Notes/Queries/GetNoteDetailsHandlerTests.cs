using AutoMapper;
using Notes.Application.Notes.Queries.GetNoteDetails;
using Notes.Persistance;
using Notest.Tests.Common;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Notest.Tests.Notes.Queries
{
    [Collection("QueryCollection")]
    public class GetNoteDetailsHandlerTests
    {
        private readonly NotesDbContext context;
        private readonly IMapper mapper;

        public GetNoteDetailsHandlerTests(QueryTestFixture fixture)
        {
            context = fixture.Context;
            mapper = fixture.Mapper;
        }
        [Fact]
        public async Task GetNoteDetails_Success()
        {

            //Arrange
            var handler = new GetNoteDetailsQueryHandler(context, mapper);

            //Act
            var result = await handler.Handle
                (new
                GetNoteDetailsQuery
                {
                    UserId = NotesContextFactory.UserBId,
                    Id = Guid.Parse("C9F527FA-66C6-4145-9399-33DC1D64F03A")
                }
                , CancellationToken.None);
            //Assert
            result.ShouldBeOfType<NoteDetailsVm>();
            result.Title.ShouldBe("Title2");
            result.CreationDate.ShouldBe(DateTime.Today);
        }
    }
}
