using Discord.Commands;
using Sombra_Bot.Utils;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace Sombra_Bot.Commands
{
    public class Save : ModuleBase<SocketCommandContext>
    {
        public static readonly DirectoryInfo save = new DirectoryInfo("save");

        public static Dictionary<string, ISaveFile> Saves = new Dictionary<string, ISaveFile>();
#if !PUBLIC
        private static readonly string[] PreDefinedSaves = { "BannedUsers.Ulong", "DisabledMServers.Ulong" };
#else
        private static readonly string[] PreDefinedSaves = { "BannedUsers.Ulong", "DisabledMServers.Ulong", "Suggestions.UlongString" };
        public static UlongStringSaveFile Suggestions => Saves["Suggestions"] as UlongStringSaveFile;
#endif
        public static UlongSaveFile BannedUsers => Saves["BannedUsers"] as UlongSaveFile;
        public static UlongSaveFile DisabledMServers => Saves["DisabledMServers"] as UlongSaveFile;

        private static ISaveFile OpenSaveFile(FileInfo file)
        {
            switch (file.Extension.ToLower())
            {
                case ".ulong":
                    return new UlongSaveFile(file);
                case ".ulongstring":
                    return new UlongStringSaveFile(file);
                default:
                    throw new Exception("File not a save!");
            }
        }

        static Save()
        {
            save.Create();
            foreach (string str in PreDefinedSaves)
            {
                FileInfo info = save.GetFile(str);
                if (!info.Exists)
                    info.Create().Close();
            }

            foreach (FileInfo file in save.EnumerateFiles())
                Saves[Path.GetFileNameWithoutExtension(file.FullName)] = OpenSaveFile(file);

            foreach (ISaveFile file in Saves.Values)
                file.Read();
        }

        public static void WriteAll()
        {
            foreach (ISaveFile save in Saves.Values)
                save.Write();
        }

        [Command("ForceSave"), Summary("Forces writing saves to files.")]
        [RequireOwner]
        public async Task ForceSave()
        {
            WriteAll();
            await Context.Channel.SendMessageAsync("Saving Forced!");
        }

        [Command("GetSave")]
        [RequireOwner]
        public async Task GetSave(string savename)
        {
            WriteAll();
            if (save.GetSize() == 0)
            {
                await Error.Send(Context.Channel, Value: "Saves is empty!");
                return;
            }
            
            if (savename == "*" || savename.ToLower() == "all")
            {
                MemoryStream stream = new MemoryStream();
                ZipArchive zipfile = new ZipArchive(stream, ZipArchiveMode.Create);
                
                foreach (FileInfo savefile in save.GetFiles())
                {
                    if (savefile.Length != 0)
                    {
                        using (var entry = zipfile.CreateEntry(savefile.Name, CompressionLevel.Optimal).Open())
                        {
                            entry.Write(File.ReadAllBytes(savefile.FullName));
                        }
                    }
                }
                
                stream.Position = 0;
                await Context.Channel.SendFileAsync(stream, "Saves.zip");
                stream.Close();
                return;
            }

            foreach (FileInfo savefile in save.GetFiles())
            {
                if (Path.GetFileNameWithoutExtension(savefile.FullName).ToLower().Contains(savename.ToLower()))
                {
                    if (savefile.Length != 0)
                    {
                        if (savefile.Length <= 8000000)
                        {
                            await Context.Channel.SendFileAsync(savefile.FullName);
                            return;
                        }
                        else
                        {
                            await Error.Send(Context.Channel, Value: "Save file is too large to send! (>8MB)");
                            return;
                        }
                    }
                    else
                    {
                        await Error.Send(Context.Channel, Value: "Save is empty!");
                        return;
                    }
                }
            }
            await Error.Send(Context.Channel, Value: "Save file not found!");
        }

        //STUBBED
        /* 
        [Command("LoadSave"), Summary("Loads a combined save image.")]
        [RequireOwner]
        public async Task LoadSave(bool ShouldClear = false)
        {

        }
        */
    }
}
