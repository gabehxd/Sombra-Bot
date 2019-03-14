using Discord.Commands;
using Sombra_Bot.Utils;
using System.Collections.Generic;
using System.IO;

namespace Sombra_Bot.Commands
{
    public class Save : ModuleBase<SocketCommandContext>
    {
        public static readonly DirectoryInfo save = new DirectoryInfo("save");
        private FileInfo TempSaveImage => Program.roottemppath.GetFile("save.cfg");

        //To be used at a later date
        public static Dictionary<string, ISaveFile> Saves;
        public static UlongSaveFile BannedUsers => Saves["BannedUsers"] as UlongSaveFile;
        public static UlongSaveFile DisabledMServers => Saves["DisabledMServers"] as UlongSaveFile;
        public static UlongStringSaveFile Suggestions => Saves["Suggestions"] as UlongStringSaveFile;

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
