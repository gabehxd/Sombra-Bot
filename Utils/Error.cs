using Discord;
using System;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace Sombra_Bot.Utils
{
    public class Error
    {
        public static async Task Send(string Reason, SocketUserMessage SendLocation, string Action = "View the s.help for help")
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle("Error");
            builder.AddField(Reason, Action);
            builder.WithColor(Color.Red);
            builder.WithTimestamp(DateTimeOffset.Now);
            await SendLocation.Channel.SendMessageAsync("", embed: builder);
        }
    }
}
