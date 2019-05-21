using Discord.Commands;
using System.Threading.Tasks;
using Discord.WebSocket;
using Discord;
using Sombra_Bot.Utils;

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
            string desc = "";

            switch (Context.Client.Guilds.Count)
            {
                case 1:
                    desc += "I am currently only in this server.";
                    break;
                default:
                    desc += $"```{Context.Client.Guilds.Count} total servers:\n";
                    foreach (SocketGuild guild in Context.Client.Guilds)
                    {
                        desc += $"{guild.Name}\n";
                    }
                    desc += "```";
                    break;
            }

            //i dont have enough servers so this might not work lol
            if (desc.Length > 2000)
            {
                string[] msgs = desc.ConvertToDiscordSendable();
                builder.WithDescription(msgs[0]);
                await Context.Channel.SendMessageAsync(embed: builder.Build());

                for (int i = 1; i < msgs.Length; i++)
                {
                    string msg = (string)msgs[i];

                    EmbedBuilder exnt = new EmbedBuilder();
                    exnt.WithColor(Color.Purple);
                    if (i == msgs.Length - 1) builder.WithCurrentTimestamp();
                    exnt.WithDescription(msg);
                    await Context.Channel.SendMessageAsync(embed: exnt.Build());
                }
                return;
            }
            builder.WithDescription(desc);
            builder.WithCurrentTimestamp();
            await Context.Channel.SendMessageAsync(embed: builder.Build());
        }
    }
}

