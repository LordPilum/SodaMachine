using SodaMachine.Models;
using SodaMachine.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace SodaMachine
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Starting application.");
            List<Soda>? inventory;
            using (StreamReader r = new StreamReader("Configuration/inventory.json"))
            {
                string json = r.ReadToEnd();
                inventory = JsonSerializer.Deserialize<List<Soda>>(json);
            }

            if(inventory == null) {
                Console.WriteLine("Unable to load inventory.");
                return;
            }
            Console.WriteLine("Loaded {0} products.", inventory.Count);

            await Host.CreateDefaultBuilder()
                .ConfigureLogging(builder =>
                {
                    builder.ClearProviders();
                    builder.AddConsole()
                        .SetMinimumLevel(LogLevel.Error);
                })
                .ConfigureServices(services =>
                {
                    services.AddSingleton<List<Soda>>(inventory);
                    services.AddSingleton<SodaMachineState>();
                    services.AddHostedService<SodaMachineService>(serviceProvider => 
                        new SodaMachineService(
                            serviceProvider.GetService<ILogger<SodaMachineService>>(), 
                            serviceProvider.GetService<SodaMachineState>()
                        )
                    );
                })
                .RunConsoleAsync();
        }
    }
}
