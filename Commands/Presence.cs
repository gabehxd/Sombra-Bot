using System.Threading.Tasks;
using Discord.Commands;

namespace Sombra_Bot.Commands
{
    public class Presence : ModuleBase<SocketCommandContext>
    {
        [Command("SetPresence"), Summary("Sets the game presence")]
        [RequireOwner]
        public async Task SetPresence(params string[] input)
        {
#if !DEBUG
            await Context.Client.SetGameAsync($"{string.Join(" ", input)} | s.help");
#else
            await Context.Client.SetGameAsync($"{string.Join(" ", input)} | Debug Build");
#endif
            await Context.Channel.SendMessageAsync("Done!");
        }

        [Command("SetStream"), Summary("Sets the game presence")]
        [RequireOwner]
        public async Task SetStream(string input, string url)
        {
            await Context.Client.SetGameAsync(input, url, Discord.StreamType.Twitch);
            await Context.Channel.SendMessageAsync("Done!");
        }

        [Command("ResetPresence"), Summary("Resets the game presence")]
        [RequireOwner]
        public async Task ResetPresence()
        {
#if !DEBUG
            await Context.Client.SetGameAsync("Hacking the planet | s.help");
#else
            await Context.Client.SetGameAsync("Hacking the planet | Debug Build");
#endif
            await Context.Channel.SendMessageAsync("Done!");
        }
    }
}
