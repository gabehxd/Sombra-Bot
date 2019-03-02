using Discord.Commands;
using System.Threading.Tasks;
using Discord;

namespace Sombra_Bot.Commands
{
    public class User : ModuleBase<SocketCommandContext>
    {
        [Command("Avatar"), Alias("Ava", "pfp"), Summary("Grabs a User Avatar")]
        public async Task GetAvatar(IUser user)
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithColor(Color.Purple);
            builder.WithTitle($"{user.Username}'s Avatar");
            //Is 2048 always the best size?
            builder.WithImageUrl(user.GetAvatarUrl(ImageFormat.Auto, 2048));
            await Context.Channel.SendMessageAsync(embed: builder.Build());
        }
    }
}
