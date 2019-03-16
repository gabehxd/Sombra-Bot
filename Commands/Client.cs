using Discord.Commands;
using System;
using System.Threading.Tasks;
using Sombra_Bot.Utils;

namespace Sombra_Bot.Commands
{
    public class Client : ModuleBase<SocketCommandContext>
    {
        [Command("Shutdown"), Summary("Shut downs the bot.")]
        [RequireOwner]
        public async Task ShutDown()
        {
            await Context.Channel.SendMessageAsync("Bye bitch.");
            await Context.Client.LogoutAsync();
            Environment.Exit(0);
        }

        [Command("Invite"), Summary("Get an invite.")]
        public async Task GetInvite()
        {
            await Context.Channel.SendMessageAsync("Invite link: https://discordbots.org/bot/516009170353258496\nDiscord server: https://discord.gg/jQ8HuWE\nSource code: https://github.com/SunTheCourier/Sombra-Bot");
        }
    }
}
