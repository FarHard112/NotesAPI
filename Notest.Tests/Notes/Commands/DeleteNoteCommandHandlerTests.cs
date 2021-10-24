using Microsoft.EntityFrameworkCore;
using Notes.Application.Exceptions;
using Notes.Application.Notes.Commands.CreateNote;
using Notes.Application.Notes.Commands.DeleteNote;
using Notes.Persistance;
using Notest.Tests.Common;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Notest.Tests.Notes.Commands
{
    public class DeleteNoteCommandHandlerTests : TestCommandBase
    {
        public DeleteNoteCommandHandlerTests(NotesDbContext context) : base(context)
        {
        }

        [Fact]
        public async Task DeleteNoteCommandHandler_Success()
        {
            //Arrange
            var handler = new DeleteNoteCommandHandler(context);
            //Act
            await handler.Handle(new DeleteNoteCommand
            {
                Id = NotesContextFactory.NoteIdForDelete,
                UserId = NotesContextFactory.UserAId,

            }, CancellationToken.None);
            //Assert
            Assert.Null(context.Notes.
                SingleAsync(note => note.Id == NotesContextFactory.NoteIdForDelete));
        }
        [Fact]
        public async Task DeleteNoteCommandHandler_FailOrWrongId()
        {
            //Arrange
            var handler = new DeleteNoteCommandHandler(context);
            //Act

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>

            await handler.Handle(new DeleteNoteCommand
            {
                Id = Guid.NewGuid(),
                UserId = NotesContextFactory.UserAId
            },
                CancellationToken.None)
            );


        }
        [Fact]
        public async Task DeleteNoteCommandHandler_FailOrWrongUserId()
        {
            //Arrange
            var deleteHandler = new DeleteNoteCommandHandler(context);
            var createHandler = new CreateNoteCommandHandler(context);
            var noteId = await createHandler.Handle(
                new CreateNoteCommand { Title = "NoteTitle", UserId = NotesContextFactory.UserAId }, CancellationToken.None);

            //Act
            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await deleteHandler.Handle(new DeleteNoteCommand
                { Id = noteId, UserId = NotesContextFactory.UserBId }
                , CancellationToken.None);
            });
        }
    }
}
