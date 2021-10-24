namespace Notes.Persistance
{
    public class DbInitializer
    {
        public static void Initalize(NotesDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
