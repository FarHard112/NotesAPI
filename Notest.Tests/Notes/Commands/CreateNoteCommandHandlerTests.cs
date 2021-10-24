using Microsoft.EntityFrameworkCore;
using Notes.Application.Notes.Commands.CreateNote;
using Notes.Persistance;
using Notest.Tests.Common;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Notest.Tests.Notes.Commands
{
    public class CreateNoteCommandHandlerTests : TestCommandBase
    {
        public CreateNoteCommandHandlerTests(NotesDbContext context) : base(context)
        {
            Context = context;
        }

        public NotesDbContext Context { get; }

        [Fact]
        public async Task CreateNoteCommandHandler_Success()
        {
            //Arrange
            var handler = new CreateNoteCommandHandler(context);
            var noteName = "note name";
            var noteDetails = "note details";
            //Act
            var noteId = await handler.Handle(
               new CreateNoteCommand
               {
                   Title = noteName,
                   Details = noteDetails,
                   UserId = NotesContextFactory.UserAId

               },
               CancellationToken.None);
            //Assert
            Assert.NotNull(await Context.Notes.
                SingleOrDefaultAsync(note => note.Id == noteId && note.Title == noteName && note.Details == noteDetails));

        }



    }
}
