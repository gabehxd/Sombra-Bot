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
            await Context.Client.SetGameAsync(string.Join(" ", input));
            await Context.Channel.SendMessageAsync("Done!");
        }

        [Command("resetpresence"), Summary("Resets the game presence")]
        [RequireOwner]
        public async Task ResetPresence()
        {
            await Context.Client.SetGameAsync("Hacking the planet");
            await Context.Channel.SendMessageAsync("Done!");
        }
    }
}
