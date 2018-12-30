using System.Threading.Tasks;
using Discord.Commands;
using Discord;

namespace Sombra_Bot.Commands
{
    public class Invite : ModuleBase<SocketCommandContext>
    {
        [Command("Invite"), Summary("Get an invite.")]
        public async Task GetInvite()
        {
            await Context.Channel.TriggerTypingAsync();
            await Task.Delay(500);
            await Context.Channel.SendMessageAsync("Invite me using this link: https://discordapp.com/oauth2/authorize?client_id=516009170353258496&scope=bot&permissions=8\nMy Discord Server: https://discord.gg/jQ8HuWE");
        }
    }
}
