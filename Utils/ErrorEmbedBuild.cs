using Discord;
using System;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace Sombra_Bot.Utils
{
    public class ErrorEmbedBuild
    {
        public static async Task SendError(string Reason, SocketUserMessage sendloc)
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle("Error");
            builder.AddField(Reason, "View the s.help for help");
            builder.WithColor(Color.Red);
            builder.WithTimestamp(DateTimeOffset.Now);
            await sendloc.Channel.SendMessageAsync("", embed: builder);
        }
    }
}
