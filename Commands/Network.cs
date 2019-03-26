using Discord.Commands;
using Sombra_Bot.Utils;
using Discord;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

namespace Sombra_Bot.Commands
{
    public class Network : ModuleBase<SocketCommandContext>
    {
        [Command("Ping"), Summary("Pings an IP.")]
        public async Task Ping(string ip)
        {
            Ping pinger = new Ping();
            try
            {
                PingReply reply = pinger.Send(ip);
                if (reply.Status == IPStatus.Success)
                {
                    EmbedBuilder builder = new EmbedBuilder();
                    builder.WithColor(Color.Green);
                    builder.WithCurrentTimestamp();
                    builder.AddField(reply.Address.ToString(), $"{reply.RoundtripTime}ms");
                    await Context.Channel.SendMessageAsync(embed: builder.Build());
                }
            }
            catch
            {
                await Error.Send(Context.Channel, Value: "Ping failed!");
                return;
            }
        }
    }
}