using Discord.Commands;
using Sombra_Bot.Utils;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Discord;
using System.Linq;

namespace Sombra_Bot.Commands
{
    public class Suggestions : ModuleBase<SocketCommandContext>
    {
        public static FileInfo Suggests => new FileInfo(Path.Combine(Program.save.FullName, "Suggestions.obj"));

        [Command("Suggest"), Summary("Suggest a feature."), Alias("AddSuggestion")]
        public async Task SaveSuggestion(params string[] suggestion)
        {
            string joined = string.Join(" ", suggestion);
            if (string.IsNullOrWhiteSpace(joined))
            {
                await Error.Send(Context.Channel, Key: "The input text has too few parameters.");
                return;
            }

            File.AppendAllText(Suggests.FullName, $"{Context.User.Id}: {joined}\n");
            await Context.Channel.SendMessageAsync("Thank you for your suggestion!");
        }

        [Command("ListSuggestions"), Summary("List saved suggestions."), Alias("Suggestions")]
        [RequireOwner]
        public async Task ListSuggestions()
        {
            if (Suggests.Exists)
            {
                string[] suggestions = File.ReadAllLines(Suggests.FullName);
                if (suggestions.Length != 0)
                {
                    string mod = "";
                    int i = 1;
                    foreach (string line in suggestions)
                    {
                        string[] values = line.Split(": ");
                        mod += $"{i} - <@{values[0]}>: {values[1]}\n";
                        i++;
                    }

                    EmbedBuilder builder = new EmbedBuilder();
                    builder.WithTitle("Suggestions");
                    builder.WithColor(Color.Purple);
                    builder.WithDescription(mod);
                    builder.WithCurrentTimestamp();
                    await Context.Channel.SendMessageAsync(embed: builder.Build());
                    return;
                }
            }
            await Error.Send(Context.Channel, Value: "No suggestions have been made.");
        }

        [Command("RemoveSuggestion"), Summary("Remove a suggestion.")]
        [RequireOwner]
        public async Task ListSuggestions(int element)
        {
            if (Suggests.Exists)
            {
                List<string> suggestions = File.ReadAllLines(Suggests.FullName).ToList();
                if (suggestions.Count != 0)
                {
                    if (element > suggestions.Count || element <= 0)
                    {
                        await Error.Send(Context.Channel, Value: "Out of range of suggestions.");
                        return;
                    }
                    suggestions.RemoveAt(element - 1);
                    File.WriteAllLines(Suggests.FullName, suggestions);
                    await Context.Channel.SendMessageAsync("Suggestion removed!");
                    return;
                }
            }
            await Error.Send(Context.Channel, Value: "No suggestions have been made");
        }
    }
}
