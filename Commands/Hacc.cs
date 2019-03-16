using Discord.Commands;
using System.Threading.Tasks;
using Discord;

namespace Sombra_Bot.Commands.Memes
{
    public class Hacc : ModuleBase<SocketCommandContext>
    {
        [Command("Hacc"), Summary("Bans or kicks a user.")]
        public async Task Hac(IUser user)
        {
            EmbedBuilder msg = new EmbedBuilder();
            msg.WithColor(Color.Purple);
            msg.WithCurrentTimestamp();
            msg.WithFooter($"Hacc'd by {Context.User.Username}");
            msg.AddField("Wam", $"{user.Mention} hacc'd >:3");
            await Context.Channel.SendMessageAsync(embed: msg.Build());
        }
    }
}
