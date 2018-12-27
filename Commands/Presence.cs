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
            await Context.Channel.TriggerTypingAsync();

            string joined = string.Join(" ", input);
            if (string.IsNullOrWhiteSpace(joined))
            {
                await Error.Send(Context.Channel, Value: "The input text has too few parameters.");
                return;
            }
#if !DEBUG
            await Context.Client.SetGameAsync($"{joined} | s.help");
#else
            await Context.Client.SetGameAsync($"{joined} | Debug Build");
#endif
            await Context.Channel.SendMessageAsync("Done!");
        }

        [Command("SetStream"), Summary("Sets the game presence")]
        [RequireOwner]
        public async Task SetStream(string input, string url)
        {
            await Context.Channel.TriggerTypingAsync();

            await Context.Client.SetGameAsync(input, url, Discord.StreamType.Twitch);
            await Context.Channel.SendMessageAsync("Done!");
        }

        [Command("ResetPresence"), Summary("Resets the game presence")]
        [RequireOwner]
        public async Task ResetPresence()
        {
            await Context.Channel.TriggerTypingAsync();

#if !DEBUG
            await Context.Client.SetGameAsync("Hacking the planet | s.help");
#else
            await Context.Client.SetGameAsync("Hacking the planet | Debug Build");
#endif
            await Context.Channel.SendMessageAsync("Done!");
        }
    }
}
