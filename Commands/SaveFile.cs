using System.Collections.Generic;
using System.IO;
using Sombra_Bot.Commands;

namespace Sombra_Bot.Commands
{
    public abstract class SaveFile<T>
    {
        public T Data;
        public abstract void Read();
        public abstract void WriteSaveFile();
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
            if (File.Exists)
            {
                using (StreamReader reader = new StreamReader(Open()))
                {
                    string s;
                    while ((s = reader.ReadLine()) != null) Data.Add(ulong.Parse(s));
                }
            }
        }

        public override void WriteSaveFile()
        {
            foreach (ulong value in Data)
            {
                System.IO.File.AppendAllText(File.FullName, value.ToString());
            }
        }

        public UlongSaveFile(FileInfo file) : base(file) { }

    }
    public class UlongStringSaveFile : SaveFile<List<KeyValuePair<ulong, string>>>
    {
        public override void Read()
        {
            if (File.Exists)
            {
                using (StreamReader reader = new StreamReader(Open()))
                {
                    string s;
                    while ((s = reader.ReadLine()) != null)
                    {
                        string[] split = s.Split(": ");
                        Data.Add(new KeyValuePair<ulong, string>(ulong.Parse(split[0]), split[1]));
                    }
                }
            }
        }

        public override void WriteSaveFile()
        {
            foreach (KeyValuePair<ulong, string> pair in Data)
            {
                System.IO.File.AppendAllText(File.FullName, $"{pair.Key}: {pair.Value}\n");
            }
        }
        public UlongStringSaveFile(FileInfo file) : base(file) { }
    }
}