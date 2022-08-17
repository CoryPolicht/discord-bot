using Discord.Commands;
using Discord.WebSocket;
using discord_bot.Models;
using discord_bot.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace discord_bot
{
    public class Config
    {

        public static IServiceProvider CreateServices()
        {
            return new ServiceCollection()
                .AddSingleton<DiscordSocketClient>()
                .AddSingleton<CommandService>()
                .AddSingleton<CommandHandler>()
                .BuildServiceProvider();
        }

        //this gets config values needed to run the application correctly based on environment variables
        //should throw arguments and not be caught to fail startup
        public static ConfigModel getConfig()
        {
            var apiToken = Environment.GetEnvironmentVariable("apiToken");
            if (apiToken == null) throw new ArgumentNullException("No Api token found");
            return new ConfigModel(apiToken);
        }
    }

}
