using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Interactivity;
using DSharpPlus.CommandsNext;
using Newtonsoft.Json;

namespace KBeerDiscordBot {
    class Program {

        public static DiscordClient discord;
        public static CommandsNextModule commands;
        //public static InteractivityModule interactivity;
        public static ConfigJson jConfig;


        static void Main(string[] args) {
            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        static async Task MainAsync(String[] args) {

            //Load config
            string json = "";
            using (var fs = System.IO.File.OpenRead("config.json"))
            using (var sr = new System.IO.StreamReader(fs, new System.Text.UTF8Encoding(false)))
                json = await sr.ReadToEndAsync();

            jConfig = JsonConvert.DeserializeObject<ConfigJson>(json);

            DiscordConfiguration dConfig;

            // Set up the client connection

            dConfig = new DiscordConfiguration {
                Token = jConfig.DiscordToken,
                TokenType = TokenType.Bot,
                UseInternalLogHandler = true,
                LogLevel = LogLevel.Debug
            };
            discord = new DiscordClient(dConfig);

            commands = discord.UseCommandsNext(new CommandsNextConfiguration {
                StringPrefix = jConfig.CmdPrefix + "."
            });
            commands.RegisterCommands<CommandManager>();

            await discord.ConnectAsync();
            await Task.Delay(-1);
        }

        public static void SaveConfig() {
            JsonSerializer ser = new JsonSerializer();
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter("config.json")) {
                using (JsonWriter writer = new JsonTextWriter(sw)) {
                    ser.Serialize(writer, jConfig);
                }
            }
        }

        public struct ConfigJson {

            [JsonProperty("UntappdClientID")]
            public string ClientID { get; set; }

            [JsonProperty("UntappdClientSecret")]
            public string ClientSecret { get; set; }

            [JsonProperty("DiscordToken")]
            public string DiscordToken { get; set; }

            [JsonProperty("CmdPrefix")]
            public string CmdPrefix { get; set; }
        }
    }
}
