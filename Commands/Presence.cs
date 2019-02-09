using System.Threading.Tasks;
using Discord.Commands;
using Sombra_Bot.Utils;

namespace Sombra_Bot.Commands
{
    public class Presence : ModuleBase<SocketCommandContext>
    {
        [Command("SetPresence"), Summary("Sets the game presence")]
        [RequireOwner]
        public async Task SetPresence(params string[] input)
        {
            string joined = string.Join(" ", input);
            if (string.IsNullOrWhiteSpace(joined))
            {
                await Error.Send(Context.Channel, Value: "The input text has too few parameters.");
                return;
            }

            Program.presence = joined;
            Program.stream = null;
            Program.IsStream = false;
            await Context.Channel.SendMessageAsync("Queued!");
        }

        [Command("SetStream"), Summary("Sets the stream presence")]
        [RequireOwner]
        public async Task SetStream(string url, params string[] input)
        {
            string joined = string.Join(" ", input);
            if (string.IsNullOrWhiteSpace(joined))
            {
                await Error.Send(Context.Channel, Value: "The input text has too few parameters.");
                return;
            }

            Program.presence = joined;
            Program.stream = url;
            Program.IsStream = true;
            await Context.Channel.SendMessageAsync("Queued!");
        }

        [Command("ResetPresence"), Summary("Resets the game presence")]
        [RequireOwner]
        public async Task ResetPresence()
        {
            Program.presence = null;
            Program.stream = null;
            Program.IsStream = false;
            await Context.Channel.SendMessageAsync("Queued!");
        }
    }
}
