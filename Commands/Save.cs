using Sombra_Bot.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using Discord.Commands;

namespace Sombra_Bot.Commands
{
    public class Save : ModuleBase<SocketCommandContext>
    {
        [Command("GetSave"), Summary("Gets a combined copy of the save files.")]
        [RequireOwner]
        public async Task GetSave()
        {
            List<string> Save = new List<string>();
            bool anyexists = false;

            if (BotBan.Banned.Exists || Suggestions.suggests.Exists || DisableSpeak.DisabledMServers.Exists) anyexists = true;
            else
            {
                await Error.Send(Context.Channel, Value: "No save data exists.");
                return;
            }

            if (BotBan.Banned.Exists)
            {
                string[] ban = File.ReadAllLines(BotBan.Banned.FullName);
                Save.Add("-Banned-");
                //Add length to know how long to look how many lines we should read for loading a save.
                Save.Add(ban.Length.ToString());
                foreach (string line in ban)
                {
                    Save.Add(line);
                }
            }

            if (Suggestions.suggests.Exists)
            {
                string[] suggest = File.ReadAllLines(Suggestions.suggests.FullName);
                Save.Add("-Suggestions-");
                Save.Add(suggest.Length.ToString());
                foreach (string line in suggest)
                {
                    Save.Add(line);
                }
            }

            if (DisableSpeak.DisabledMServers.Exists)
            {
                string[] disabled = File.ReadAllLines(DisableSpeak.DisabledMServers.FullName);
                Save.Add("-Disabled-");
                Save.Add(disabled.Length.ToString());
                foreach (string line in disabled)
                {
                    Save.Add(line);
                }
            }

            if (anyexists)
            {
                FileInfo temp = new FileInfo(Path.Combine(Path.GetTempPath(), "Save.cfg"));
                File.WriteAllLines(temp.FullName, Save);

                await Context.Channel.SendFileAsync(temp.FullName, "Done!");
            }
        }
    }
}
