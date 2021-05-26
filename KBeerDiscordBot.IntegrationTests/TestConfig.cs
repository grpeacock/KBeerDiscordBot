using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace KBeerDiscordBot.IntegrationTests
{
    public static class TestConfig
    {
        static TestConfig()
        {
            _config = new ConfigurationBuilder()
                    .AddJsonFile("config.json")
                    .Build();
        }

        private static IConfigurationRoot _config;

        public static string UntappdClientId => _config["untappd:client:id"];
        public static string UntappdClientSecret => _config["untappd:client:secret"];

    }
}
