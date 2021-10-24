namespace Notes.Identitiy.Data
{
    public static class DbInitalizer
    {
        public static void Initalize(AuthDbContext context)
        {
            context.Database.EnsureCreated();
        }

    }
}
