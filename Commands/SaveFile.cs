using System.Collections.Generic;
using System.IO;
using Sombra_Bot.Commands;

namespace Sombra_Bot.Commands
{
    public abstract class SaveFile<T>
    {
        public T Cache;
        public abstract void Read();
        protected FileInfo File;
        public SaveFile(FileInfo file)
        {
            File = file;
        }
        protected Stream Open()
        {
            return File.OpenWrite();
        }
    }

    public class UlongSaveFile : SaveFile<List<ulong>>
    {
        
        public override void Read()
        {
            using (StreamReader reader = new StreamReader(Open()))
            {
                if (File.Exists)
                string s;
                while ((s = reader.ReadLine()) != null)
                    Cache.Add(ulong.Parse(s));
            }
        }

        public UlongSaveFile(FileInfo file) : base(file) { }

    }

}