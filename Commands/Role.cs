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
        [Command("AddRole"), Summary("Gives this specified role to the specified user."), Alias("GiveRole")]
        [RequireUserPermission(ChannelPermission.ManageRoles), RequireBotPermission(ChannelPermission.ManageRoles)]
        public async Task GiveRole([RequireHierarchy]SocketGuildUser user, SocketRole role)
        {
            SocketGuildUser userguild = Context.User as SocketGuildUser;

            if (!role.Members.Contains(user))
            {

                await user.AddRoleAsync(role);

                EmbedBuilder msg = new EmbedBuilder();
                msg.WithColor(Color.Purple);
                msg.WithCurrentTimestamp();
                msg.AddField("Success!", $"Gave role {role.Mention} to {user.Mention}.");
                await Context.Channel.SendMessageAsync(embed: msg.Build());
            }
            else
            {
                await Error.Send(Context.Channel, Value: $"{user.Mention} already has that role.");
                return;
            }
        }

        [Command("RemoveRole"), Summary("Gives this specified role to the specified user.")]
        [RequireUserPermission(ChannelPermission.ManageRoles), RequireBotPermission(ChannelPermission.ManageRoles)]
        public async Task RemoveRole([RequireHierarchy]SocketGuildUser user, SocketRole role)
        {
            SocketGuildUser userguild = Context.User as SocketGuildUser;

            if (role.Members.Contains(user))
            {
                await user.RemoveRoleAsync(role);

                EmbedBuilder msg = new EmbedBuilder();
                msg.WithColor(Color.Purple);
                msg.WithCurrentTimestamp();
                msg.AddField("Success!", $"Removed role {role.Mention} from {user.Mention}.");
                await Context.Channel.SendMessageAsync(embed: msg.Build());
            }
            else
            {
                await Error.Send(Context.Channel, Value: $"{user.Mention} does not have that role.");
                return;
            }
        }

    }
}
