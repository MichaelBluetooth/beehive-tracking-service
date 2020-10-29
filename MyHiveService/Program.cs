using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyHiveService.Models;
using MyHiveService.TestData;

namespace MyHiveService
{
    public class Program
    {
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>();


        public static void Main(string[] args)
        {
            //1. Get the IWebHost which will host this application.
            var host = CreateWebHostBuilder(args)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            //2. Find the service layer within our scope.
            using (var scope = host.Services.CreateScope())
            {
                //3. Get the instance of DbContext in our services layer
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<MyHiveDbContext>();

                //bool isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
                bool isDevelopment = true;
                if (isDevelopment)
                {
                    //4. Call the DataGenerator to create sample data
                    TestDataBuilder.fill(context);
                }
            }

            //Continue to run the application
            host.Run();
        }
    }
}
