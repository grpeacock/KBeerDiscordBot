using DSharpPlus;
using DSharpPlus.CommandsNext;
using KBeerDiscordBot.Abstractions;
using KBeerDiscordBot.Discord;
using KBeerDiscordBot.Untappd;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace KBeerDiscordBot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Load Configuration
            var configuration = new ConfigurationBuilder()
                    .AddJsonFile("config.json")
                    .Build();

            // Define services
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IConfigurationRoot>(configuration);
            serviceCollection.AddSingleton<UntappdApiCredentials>(sp =>
            {
                var config = sp.GetRequiredService<IConfigurationRoot>();
                return new UntappdApiCredentials
                {
                    ClientId = config["untappd:client:id"],
                    ClientSecret = config["untappd:client:secret"]
                };
            });
            serviceCollection.AddSingleton<IBeerService, UntappdBeerService>();
            var serviceProvider = serviceCollection.BuildServiceProvider();

            // Set up the client connection
            var discordConfig = new DiscordConfiguration
            {
                Token = configuration["discord:token"],
                TokenType = TokenType.Bot
            };
            var discordClient = new DiscordClient(discordConfig);

            var commandsNextModule = discordClient.UseCommandsNext(new CommandsNextConfiguration
            {
                StringPrefixes = new[] { configuration["discord:commandPrefix"] + ".", "." },
                Services = serviceProvider
            });
            commandsNextModule.RegisterCommands<SearchCommandModule>();

            await discordClient.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}
