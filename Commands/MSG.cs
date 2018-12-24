using System.Threading.Tasks;
using Discord.Commands;

namespace Sombra_Bot.Commands
{
    public class Message : ModuleBase<SocketCommandContext>
    {
        [Command("say"), Summary("Says the message sent.")]
        [RequireOwner]
        public async Task Say(string text)
        {
            try
            {
                await Context.Message.DeleteAsync();
            }
            catch { }
            await Context.Channel.SendMessageAsync(text);
        }
    }
}
