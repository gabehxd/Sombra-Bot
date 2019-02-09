using System.Threading.Tasks;
using Discord.Commands;

namespace Sombra_Bot.Commands
{
    public class Invite : ModuleBase<SocketCommandContext>
    {
        [Command("Invite"), Summary("Get an invite.")]
        public async Task GetInvite()
        {
            await Context.Channel.SendMessageAsync("Invite link: <https://discordapp.com/oauth2/authorize?client_id=516009170353258496&scope=bot&permissions=8>\nDiscord server: https://discord.gg/jQ8HuWE\nSource code: https://github.com/SunTheCourier/Sombra-Bot");
        }
    }
}
