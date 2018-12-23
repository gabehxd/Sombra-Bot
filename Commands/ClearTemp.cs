using System.Threading.Tasks;
using Discord.Commands;
using Sombra_Bot.Utils;

namespace Sombra_Bot.Commands
{
    public class ClearTemp : ModuleBase<SocketCommandContext>
    {
        [Command("ClearTemp"), Summary("Clears the Temp Directory for Sombra Bot")]
        public async Task Clear()
        {
            try
            {
                Release.roottemppath.Delete(true);
            }
            catch
            {
                await Error.Send("Failed to Delete Temp Folder", Context.Channel, "¯\\_(ツ)_/¯");
            }
            await Context.Channel.TriggerTypingAsync();
            await Task.Delay(500);
            await Context.Channel.SendMessageAsync("Done!");
        }
    }
}
