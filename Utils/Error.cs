using Discord;
using System;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace Sombra_Bot.Utils
{
    public class Error
    {
        public static async Task Send(ISocketMessageChannel SendLocation, string Key = "An error has occured.", string Value = "View the help menu for help.")
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle("Error");
            builder.AddField(Key, Value);
            builder.WithColor(Color.Red);
            builder.WithCurrentTimestamp();
            await SendLocation.SendMessageAsync("", embed: builder);
        }
    }
}
