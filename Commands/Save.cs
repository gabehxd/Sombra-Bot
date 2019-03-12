using Discord.Commands;
using Sombra_Bot.Utils;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System.Net;
using System;

namespace Sombra_Bot.Commands
{
    public class Save : ModuleBase<SocketCommandContext>
    {
        private FileInfo Config => new FileInfo(Path.Combine(Program.roottemppath.FullName, "save.cfg"));

        public Dictionary<ulong, string> Suggestions = new Dictionary<ulong, string>();
        public List<string> BannedUsers = new List<string>();
        public

        [Command("GetSave"), Summary("Gets a combined copy of the save files.")]
        [RequireOwner]
        public async Task GetSave()
        {
            FileInfo[] saveobjs = Program.save.GetFiles("*.obj");
            if (saveobjs.Length != 0)
            {
                if (Config.Exists) Config.Delete();
                List<string> output = new List<string>
                {
                    saveobjs.Length.ToString()
                };
                foreach (FileInfo file in saveobjs)
                {
                    string[] lines = File.ReadAllLines(file.FullName);
                    if (lines.Length != 0)
                    {
                        output.Add(file.Name);
                        output.Add(lines.Length.ToString());
                        output.AddRange(lines);
                    }
                }
                File.WriteAllLines(Config.FullName, output);
                await Context.Channel.SendFileAsync(Config.FullName, "Current save file:");
            }
            else
            {
                await Error.Send(Context.Channel, Value: "No objects in save found.");
            }
        }

        [Command("LoadSave"), Summary("Loads a combined save image.")]
        [RequireOwner]
        public async Task LoadSave(bool ShouldClear = false)
        {
            try
            {
                if (ShouldClear)
                {
                    FileInfo[] saveobjs = Program.save.GetFiles("*.obj");
                    foreach (FileInfo savefile in saveobjs)
                    {
                        savefile.Delete();
                    }
                }
                if (Context.Message.Attachments.Count != 1) await Error.Send(Context.Channel, Value: "There is either no files attached or too many attached.");
                Discord.Attachment attachment = Context.Message.Attachments.ElementAt(0);
                FileInfo file = new FileInfo(Path.Combine(Program.roottemppath.FullName, attachment.Filename));
                WebClient client = new WebClient();
                if (file.Exists) file.Delete();
                client.DownloadFile(attachment.Url, file.FullName);
                List<string> lines = File.ReadAllLines(file.FullName).ToList();

                //file count
                int fcount = int.Parse(lines[0]);
                int seeker = 0;
                for (int i = 0; i < fcount; i++)
                {
                    seeker++;
                    string name = lines[seeker];
                    seeker++;
                    int readcount = int.Parse(lines[seeker]);
                    List<string> content = new List<string>();
                    for (int n = 0; n < readcount; n++)
                    {
                        seeker++;
                        content.Add(lines[seeker]);
                    }

                    FileInfo saveobj = new FileInfo(Path.Combine(Program.save.FullName, name));
                    if (saveobj.Exists) saveobj.Delete();
                    File.WriteAllLines(saveobj.FullName, content);
                }
                await Context.Channel.SendMessageAsync("Loaded!");
            }
            catch (Exception e)
            {
                await Error.Send(Context.Channel, Value: "Save could not be loaded, debug info sent via DMs", e: e, et: Error.ExceptionType.Fatal);
            }
        }
    }
}
