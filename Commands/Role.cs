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
            if (userguild.Roles.Last().CompareTo(role) == 1 || Context.Guild.Owner == Context.User)
            {
                if (!role.Members.Contains(user))
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
                    await Context.Channel.SendMessageAsync($"Done!, Gave role: {role} to <@{user.Id}>.");
                    //uncomment for debuging
                    //await Context.Channel.SendMessageAsync($"{userguild.Roles.Last().CompareTo(role).ToString()}");
                }
                else
                {
                    await Error.Send(Context.Channel, Value: $"<@{user.Id}> already has that role.");
                    return;
                }
            }
            else
            {
                await Error.Send(Context.Channel, Value: "You do not have enough permission to give that role.");
                return;
            }
        }

        [Command("RemoveRole"), Summary("Gives this specified role to the specified user.")]
        [RequireUserPermission(ChannelPermission.ManagePermissions)]
        public async Task RemoveRole(SocketGuildUser user, SocketRole role)
        {
            SocketGuildUser userguild = Context.User as SocketGuildUser;
            if (userguild.Roles.Last().CompareTo(role) == 1 || Context.Guild.Owner == Context.User)
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
                await Context.Channel.SendMessageAsync($"Done!, Removed role: {role} to <@{user.Id}>.");
            }
            else
            {
                await Error.Send(Context.Channel, Value: "You do not have enough permission to give that role.");
                return;
            }
        }
    }
}
