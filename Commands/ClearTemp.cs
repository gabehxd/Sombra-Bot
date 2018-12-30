using System.Threading.Tasks;
using Discord.Commands;
using Sombra_Bot.Utils;

namespace Sombra_Bot.Commands
{
    public class ClearTemp : ModuleBase<SocketCommandContext>
    {
        [Command("ClearTemp"), Summary("Clears the Temp Directory for Sombra Bot")]
        [RequireOwner]
        public async Task Clear()
        {
            try
            {
                Release.roottemppath.Delete(true);
            }
            catch
            {
                await Error.Send(Context.Channel, Value: "Failed to delete temp folder.");
                return;
            }
            await Context.Channel.TriggerTypingAsync();
            await Task.Delay(500);
            await Context.Channel.SendMessageAsync("Done!");
        }
    }
}
