using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace discord_bot.Services
{
    public class CommandHandler
    {
        private readonly CommandService _commands;
        private readonly DiscordSocketClient _client;
        private readonly IServiceProvider _service;

        //SOLID example of Dependency inversion
        public CommandHandler(IServiceProvider services)
        {
            //sets up the services required from the injected provider
            _commands = services.GetRequiredService<CommandService>();
            _client = services.GetRequiredService<DiscordSocketClient>();
            _service = services;

            //adds the function to handle commands registered in the event bus
            _client.MessageReceived += HandleCommandAsync;
        }

        //SOLID example of only doing one thing
        public async Task InstallCommandsAsyn()
        {
            //scans the current assembly to retrieve all command modules
            await _commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: _service);
        }

        // SOLID example of method doing too much
        private async Task HandleCommandAsync(SocketMessage messageParam)
        {
            var message = messageParam as SocketUserMessage;

            //short circuits the method
            if (message == null) return;

            int argPos = 0;

            //conditional to check the message meets the required criteria in order for a command to execute
            //todo parameterize the prefix to be modified
            if (!(message.HasCharPrefix('!', ref argPos) || 
                message.HasMentionPrefix(_client.CurrentUser, ref argPos) || 
                message.Author.IsBot))
                return;

            // create the context in which to execute the command
            var context = new SocketCommandContext(_client, message);

            //executes the command
            var result = await _commands.ExecuteAsync(context: context, argPos: argPos, services: _service);

            //if success short circuit
            if (!result.IsSuccess) return;

            //if error send error back
            //todo not sure if something other than error may result in non success, i.e. warning?
            await context.Channel.SendMessageAsync(result.ErrorReason);

        }
    }
}
