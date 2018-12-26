using System.Threading.Tasks;
using Discord.Commands;

namespace Sombra_Bot.Commands
{
    public class Presence : ModuleBase<SocketCommandContext>
    {
        [Command("setpresence"), Summary("Sets the game presence")]
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

        [Command("resetpresence"), Summary("Resets the game presence")]
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
