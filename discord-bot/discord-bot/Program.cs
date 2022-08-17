// See https://aka.ms/new-console-template for more information
using Discord.WebSocket;
using discord_bot;
using discord_bot.Services;
using Microsoft.Extensions.DependencyInjection;

//wrap in using statement to handle cleanup of services and what not
//todo unsure of why it was yelling at me for explicit cast, appeared to be resolving IServiceProvider from system and not DI
//SOLID implementation of interfaces on service provider is example of interface segregation
using (var provider = (ServiceProvider)Config.CreateServices())
{
    //get's the discord client service from registered services
    var client = provider.GetRequiredService<DiscordSocketClient>();

    //get the config model, this should fail the application if values are not found
    var config = Config.getConfig();

    //sets the login information to login to discord and checks login
    await client.LoginAsync(Discord.TokenType.Bot, config.ApiToken);

    //starts the websocket connection to discord
    await client.StartAsync();

    //installs the commands via the command handler
    await provider.GetRequiredService<CommandHandler>()
        .InstallCommandsAsyn();

    //set await to -1 in order to wait indefinitly so application dosen't stop.
    await Task.Delay(-1);
}




