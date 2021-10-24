using Microsoft.EntityFrameworkCore;
using Notes.Domain;
using Notes.Persistance;
using System;


namespace Notest.Tests.Common
{
    public static class NotesContextFactory
    {
        public static Guid UserAId = new Guid();
        public static Guid UserBId = new Guid();

        public static Guid NoteIdForDelete = Guid.NewGuid();
        public static Guid NoteIdForUpdate = Guid.NewGuid();


        public static NotesDbContext Create()
        {
            var options = new DbContextOptionsBuilder<NotesDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new NotesDbContext(options);
            context.Database.EnsureCreated();
            context.Notes.AddRange(
                new Note
                {
                    CreationDate = DateTime.Today,
                    Details = "Details1",
                    EditDate = null,
                    Id = Guid.Parse("{002909C6-B8DD-49E1-94A1-1AFB61F4202D}"),
                    Title = "Title1",
                    UserId = UserAId
                },
                new Note
                {
                    CreationDate = DateTime.Today,
                    Details = "Details2",
                    EditDate = null,
                    Id = Guid.Parse("{C9F527FA-66C6-4145-9399-33DC1D64F03A}"),
                    Title = "Title2",
                    UserId = UserBId
                },
                  new Note
                  {
                      CreationDate = DateTime.Today,
                      Details = "Details3",
                      EditDate = null,
                      Id = NoteIdForDelete,
                      Title = "Title3",
                      UserId = UserAId
                  },
                  new Note
                  {
                      CreationDate = DateTime.Today,
                      Details = "Details4",
                      EditDate = null,
                      Id = NoteIdForUpdate,
                      Title = "Title4",
                      UserId = UserBId
                  }

                );
            context.SaveChanges();
            return context;

        }

        public static void Destroy(NotesDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }

    }
}
