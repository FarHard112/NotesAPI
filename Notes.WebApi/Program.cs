using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Notes.Persistance;
using Serilog;
using Serilog.Events;


namespace Notes.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft",
                LogEventLevel.Information)
                .WriteTo.File("NotesWebAppLog-.txt",
                rollingInterval: RollingInterval.Day)
                .CreateLogger();
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var serviceProdiver = scope.ServiceProvider;
                try
                {
                    var context = serviceProdiver.GetService<NotesDbContext>();
                    DbInitializer.Initalize(context);
                }
                catch (System.Exception exception)
                {

                    Log.Fatal(exception, "An error ocurred while initalization");
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
