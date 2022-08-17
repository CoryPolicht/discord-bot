using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace discord_bot.Util
{
    //SOLID example of open closed principle
    //This is the example provided in the discord docs
    public class RequireRoleAttribute : PreconditionAttribute
    {
        private readonly string name;
        public RequireRoleAttribute(string name) => this.name = name;

        //SOLID example this can be split up
        public override Task<PreconditionResult> CheckPermissionsAsync(ICommandContext context, CommandInfo command, IServiceProvider services)
        {
            if (context.User is SocketGuildUser gUser)
            {
                if (gUser.Roles.Any(x => x.Name == this.name))
                {
                    return Task.FromResult(PreconditionResult.FromSuccess());
                }
                else
                {
                    return Task.FromResult(PreconditionResult.FromError($"You must be in the role {this.name} in order to execute this command"));
                }
            }
            else
            {
                return Task.FromResult(PreconditionResult.FromError("You are unable to run this command under the current context"));
            }
        }
    }
}
