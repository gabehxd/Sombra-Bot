using Discord.Commands;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Sombra_Bot.Utils;

namespace Sombra_Bot.Commands
{
    public class User : ModuleBase<SocketCommandContext>
    {
        [Command("Avatar"), Alias("Ava", "pfp"), Summary("Grabs a User Avatar")]
        public async Task GetAvatar(IUser user = null)
        {
            if (user == null) user = Context.User;
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithColor(Color.Purple);
            builder.WithTitle($"{user.Username}'s Avatar");
            //Is 2048 always the best size?
            builder.WithImageUrl(user.GetAvatarUrl(ImageFormat.Auto, 2048));
            await Context.Channel.SendMessageAsync(embed: builder.Build());
        }

        [Command("Nick"), Summary("Nicknames a user"), RequireBotPermission(GuildPermission.ManageNicknames)]
        public async Task SetNick(string nick, SocketGuildUser user = null)
        {
            IGuildUser contextuser = Context.User as IGuildUser;
            if (contextuser == null)
            {
                if (contextuser.GuildPermissions.ChangeNickname) user = Context.User as SocketGuildUser;
                else 
                {
                    await Error.Send(Context.Channel, Value: "User requires guild permission ChangeNickname.");
                    return;
                }
            }
            else if (user != Context.User as SocketGuildUser && !contextuser.GuildPermissions.ManageNicknames)
            {
                await Error.Send(Context.Channel, Value: "User requires guild permission ManageNicknames.");
                return;
            }
            else if (user == Context.User as SocketGuildUser && contextuser.GuildPermissions.ChangeNickname)
            {
                await Error.Send(Context.Channel, Value: "User requires guild permission ChangeNickname.");
                    return;
            }


            try
            {
                await user.ModifyAsync(x => { x.Nickname = nick; });
            }
            catch
            {
                await Error.Send(Context.Channel, Value: "Not enough permissions");
                return;
            }
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithColor(Color.Green);
            builder.WithTitle("Success");
            builder.WithDescription($"{user.Mention}'s Nickname has been set!");
            await Context.Channel.SendMessageAsync(embed: builder.Build());
        }
    }
}
