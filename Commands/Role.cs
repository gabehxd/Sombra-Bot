using Discord.Commands;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using System.Linq;
using Sombra_Bot.Utils;

namespace Sombra_Bot.Commands
{
    public class Role : ModuleBase<SocketCommandContext>
    {
        [Command("GiveRole"), Summary("Gives this specified role to the specified user.")]
        [RequireUserPermission(ChannelPermission.ManagePermissions)]
        public async Task GetRelease(IGuildUser user, string role)
        {
            SocketRole addrole;
            try
            {
                addrole = Context.Guild.Roles.FirstOrDefault(x => x.Name == role);
            }
            catch
            {
                await Error.Send("Role not found.", Context.Channel, "¯\\_(ツ)_/¯");
                return;
            }
            try
            {
                await user.AddRoleAsync(addrole);
            }
            catch
            {
                await Error.Send("Role could not be added.", Context.Channel, "¯\\_(ツ)_/¯");
                return;
            }
            await Context.Channel.SendMessageAsync("Done!");
            
        }
    }
}
