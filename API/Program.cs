using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Program
    // Where we start our application is a file place to seed data, we're going to do this inside the main method
    // What goes on inside here happens before our application is actually started
    // File Program sẽ chạy trước khi application bắt đầu
    {
        public static async Task Main(string[] args) // Instead of Returning void to async and Task
        {
            var host = CreateHostBuilder(args).Build(); // Assign this method to a variable

            // We need get our service, our data context service so that we can pass it to our seed method
            using var scope = host.Services.CreateScope();

            var services = scope.ServiceProvider;  // when we're in the main methods inside the program class, we're
                                                   // Outside of our middleware, we spent a bunch of time setting up a global exception handler 
                                                   // We don't have access to it in this method
                                                   // We write a try catch block so that we can catch any exceptions that happen during the seeding of our data

            try
            {
                var context = services.GetRequiredService<DataContext>();
                await context.Database.MigrateAsync(); // This Asynchronously applies any pending migrations for the context
                // to the database. Will create the database if it does not already exist.

                await Seed.SeedUsers(context);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>(); // We pass in the program class as its type
                logger.LogError(ex, "An error occurred during migration");
            }

            await host.RunAsync(); // We removed Run command from CreateHostBuilder then we need to make sure call it after we finished doing 
                                   // For more appropriately we can say: await host.RunAsync();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
