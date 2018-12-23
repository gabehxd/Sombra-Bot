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
        [Command("AddRole"), Summary("Gives this specified role to the specified user.")]
        [RequireUserPermission(ChannelPermission.ManagePermissions)]
        public async Task GiveRole(SocketGuildUser user, SocketRole role)
        {
            SocketGuildUser userguild = Context.User as SocketGuildUser;

            if (userguild.Roles.Last().CompareTo(role) >= 0)
            {
                try
                {
                    await user.AddRoleAsync(role);
                }
                catch
                {
                    await Error.Send(Context.Channel, Value: "Role could not be added.");
                    return;
                }
                await Context.Channel.SendMessageAsync("Done!");
            }
        }

        [Command("RemoveRole"), Summary("Gives this specified role to the specified user.")]
        [RequireUserPermission(ChannelPermission.ManagePermissions)]
        public async Task RemoveRole(SocketGuildUser user, SocketRole role)
        {
            try
            {
                await user.RemoveRoleAsync(role);
            }
            catch
            {
                await Error.Send(Context.Channel, Value: "Role could not be removed.");
                return;
            }
            await Context.Channel.SendMessageAsync("Done!");
        }
    }
}
