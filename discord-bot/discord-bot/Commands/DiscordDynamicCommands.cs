using Discord.Commands;
using discord_bot.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace discord_bot.Commands
{
    //No idea how I'm going to make this dynamic but this is the class that I'm attempting it in
    [Group("Dynamic-Commands")]
    public class DiscordDynamicCommands: ModuleBase<CommandContext>
    {
        public DiscordDynamicCommands()
        {

        }

        //try using !! for dynamic command execution
        [Command("!")]
        [RequireRole("Server Supporters")]
        public async Task DynamicCommand(params string[] args)
        {
            string command = args[0];

        }

    }
}
