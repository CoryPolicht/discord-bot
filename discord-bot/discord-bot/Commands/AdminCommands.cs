using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace discord_bot.Commands
{
    [RequireOwner]
    [Group("Admin")]
    public class AdminCommands
    {
        [Command("exit")]
        public async Task ExitAsync()
        {
            Environment.Exit(0);
        }
    }
}
