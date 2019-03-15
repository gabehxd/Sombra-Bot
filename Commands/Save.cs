using Discord.Commands;
using Sombra_Bot.Utils;
using System.Collections.Generic;
using System.IO;
using System;

namespace Sombra_Bot.Commands
{
    public class Save : ModuleBase<SocketCommandContext>
    {
        public static readonly DirectoryInfo save = new DirectoryInfo("save");

        public static Dictionary<string, ISaveFile> Saves = new Dictionary<string, ISaveFile>();
        private static string[] PreDefinedSaves = { "BannedUsers.Ulong", "DisabledMServers.Ulong", "Suggestions.UlongString" };
        public static UlongSaveFile BannedUsers => Saves["BannedUsers"] as UlongSaveFile;
        public static UlongSaveFile DisabledMServers => Saves["DisabledMServers"] as UlongSaveFile;
        public static UlongStringSaveFile Suggestions => Saves["Suggestions"] as UlongStringSaveFile;

        private static ISaveFile OpenSaveFile(FileInfo file)
        {
            switch (file.Extension.ToLower())
            {
                case ".ulong":
                    return new UlongSaveFile(file);
                case ".ulongstring":
                    return new UlongSaveFile(file);
                default:
                    throw new Exception("File not a save!");
            }
        }

        static Save()
        {
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


        //STUBBED
        //private FileInfo TempSaveImage => Program.roottemppath.GetFile("save.cfg");
        //STUBBED
        /* 
        [Command("GetSave"), Summary("Gets a combined copy of the save files.")]
        [RequireOwner]
        public async Task GetSave()
        {
            
        }
        */

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
