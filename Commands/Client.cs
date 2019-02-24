using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace Sombra_Bot.Commands
{
    public class Client : ModuleBase<SocketCommandContext>
    {
        [Command("Shutdown"), Summary("Shut downs the bot.")]
        [RequireOwner]
        public async Task ShutDown()
        {
            await Context.Channel.SendMessageAsync("Bye bitch.");
            Environment.Exit(0);
        }
    }
}
