using Discord.Commands;
using Sombra_Bot.Utils;
using System.IO;
using System.Threading.Tasks;

namespace Sombra_Bot.Commands
{
    public class Suggestions : ModuleBase<SocketCommandContext>
    {
        public static readonly FileInfo suggests = new FileInfo(Path.Combine(Program.save.FullName, "Suggestions.obj"));

        [Command("Suggest"), Summary("Suggest a feature")]
        public async Task SaveSuggestion(params string[] suggestion)
        {
            //command should only be enabled when bot is activated by Sun/Dev.
            if (Program.AppInfo.Owner.Id == 130825292292816897)
            {
                string joined = string.Join(" ", suggestion);
                if (string.IsNullOrWhiteSpace(joined))
                {
                    await Error.Send(Context.Channel, Key: "The input text has too few parameters.");
                    return;
                }

                File.AppendAllText(suggests.FullName, $"{Context.User.Id}: {joined}\n");
                await Context.Channel.SendMessageAsync("Thank you for your suggestion!");
            }
            else
            {
                await Error.Send(Context.Channel, Key: "Unknown command.");
            }
        }
    }
}
