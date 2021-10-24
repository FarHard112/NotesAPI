using Notes.Persistance;
using System;

namespace Notest.Tests.Common
{
    public abstract class TestCommandBase : IDisposable
    {
        protected readonly NotesDbContext context;

        protected TestCommandBase(NotesDbContext context)
        {
            this.context = NotesContextFactory.Create();
        }
        public void Dispose()
        {
            NotesContextFactory.Destroy(context);
        }
    }
}
