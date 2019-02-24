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

        [Command("ListRoles"), Alias("Roles"), Summary("Lists all roles in the current guild.")]
        public async Task ListRoles()
        {
            EmbedBuilder builder = new EmbedBuilder();
            string s = "";
            foreach (SocketRole role in Context.Guild.Roles)
            {
                s += $"{role.Name}\n";
            }

            switch (Context.Guild.Roles.Count)
            {
                case 1:
                    builder.WithAuthor("There is only one role: '@everone'");
                    break;
                default:
                    builder.WithDescription($"```{Context.Guild.Roles.Count} total roles:\n{s}```");
                    break;
            }

            await Context.Channel.SendMessageAsync(embed: builder.Build());
        }
    }
}
