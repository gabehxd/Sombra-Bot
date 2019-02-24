using Discord.Commands;
using System.Threading.Tasks;
using Discord.WebSocket;
using Discord;

namespace Sombra_Bot.Commands
{
    public class Servers : ModuleBase<SocketCommandContext>
    {
        [Command("ListServers"), Alias("Servers"), Summary("List all server Sombra Bot is in.")]
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

            switch (Context.Client.Guilds.Count)
            {
                case 1:
                    builder.WithDescription("I am currently only in this server.");
                    break;
                default:
                    builder.WithDescription($"```{Context.Client.Guilds.Count} total servers:\n{s}```");
                    break;
            }

            await Context.Channel.SendMessageAsync(embed: builder.Build());
        }
    }
}
