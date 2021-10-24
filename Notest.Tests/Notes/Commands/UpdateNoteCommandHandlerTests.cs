using Microsoft.EntityFrameworkCore;
using Notes.Application.Exceptions;
using Notes.Application.Notes.Commands.UpdateNote;
using Notes.Persistance;
using Notest.Tests.Common;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Notest.Tests.Notes.Commands
{
    public class UpdateNoteCommandHandlerTests : TestCommandBase
    {
        public UpdateNoteCommandHandlerTests(NotesDbContext context) : base(context)
        {
        }
        [Fact]
        public async Task UpdateCommandHandler_Success()
        {
            //Arrange
            var handler = new UpdateNoteCommandHandler(context);
            var updatedTitle = "new Title";

            //Act
            await handler.Handle(new UpdateNoteCommand
            {
                Id = NotesContextFactory.NoteIdForUpdate,
                UserId = NotesContextFactory.UserBId,
                Title = updatedTitle,

            }, CancellationToken.None);
            //Assert
            Assert.NotNull(await context.Notes
                .SingleOrDefaultAsync(
                note => note.Id == NotesContextFactory
                .NoteIdForUpdate && note.Title == updatedTitle));
        }
        [Fact]
        public async Task UpdateCommandHandler_FailOnWrongId()
        {
            //Arrange
            var handler = new UpdateNoteCommandHandler(context);
            //Act

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await handler.Handle(new UpdateNoteCommand
                {
                    Id = Guid.NewGuid(),
                    UserId = NotesContextFactory.UserAId,

                },
                    CancellationToken.None);
            });
        }

        [Fact]
        public async Task UpdateNoteCommand_FailOnWrongUserId()
        {
            //Arrange
            var handler = new UpdateNoteCommandHandler(context);

            //Act

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await handler.Handle(new UpdateNoteCommand
                {

                    Id = Guid.NewGuid(),
                    UserId = NotesContextFactory.UserAId,

                }
                , CancellationToken.None);
            });
        }




    }
}
