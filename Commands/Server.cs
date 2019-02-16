using Discord.Commands;
using System.Threading.Tasks;
using Discord.WebSocket;
using Discord;

namespace Sombra_Bot.Commands
{
    public class Server : ModuleBase<SocketCommandContext>
    {
        [Command("ListServers"), Summary("List all server Sombra Bot is in.")]
        [RequireOwner]
        public async Task Getservers()
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle("Server List");
            builder.WithColor(Color.Purple);
            builder.WithCurrentTimestamp();
            string s = "";
            foreach (SocketGuild guild in Context.Client.Guilds)
            {
                s += $"{guild.Name}\n";
            }
            builder.WithDescription(s);
            await Context.Channel.SendMessageAsync(embed: builder.Build());
        }
    }
}
