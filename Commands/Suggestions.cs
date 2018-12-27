using Discord.Commands;
using Sombra_Bot.Utils;
using System.IO;
using System.Threading.Tasks;

namespace Sombra_Bot.Commands
{
    public class Suggestions : ModuleBase<SocketCommandContext>
    {
        private static readonly FileInfo suggests = new FileInfo("Suggestions.txt");

        [Command("Suggest"), Summary("Suggest a feature")]
        public async Task SaveSuggestion(params string[] suggestion)
        {
            string joined = string.Join(" ", suggestion);
            if (string.IsNullOrWhiteSpace(joined))
            {
                await Error.Send(Context.Channel, Value: "The input text has too few parameters.");
                return;
            }

            File.AppendAllText(suggests.FullName, $"{Context.User.Id}: {joined}\n");
            await Context.Channel.SendMessageAsync("Thank you for your suggestion!");
        }
    }
}
