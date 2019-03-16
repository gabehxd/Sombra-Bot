using System.Collections.Generic;
using System.IO;
using Sombra_Bot.Commands;
using Sombra_Bot.Utils;

namespace Sombra_Bot.Utils
{
    public abstract class ISaveFile
    {
        public abstract void Read();
        public abstract void Write();
        protected FileInfo File;
        protected Stream Open()
        {
            return File.Open(FileMode.Open, FileAccess.ReadWrite);
        }
        public ISaveFile(FileInfo file)
        {
            File = file;
        }
    }
}