using System.Collections.Generic;
using System.IO;
using Sombra_Bot.Commands;
using Sombra_Bot.Utils;

namespace Sombra_Bot.Utils
{

    public abstract class SaveFile<T> : ISaveFile
    {
        public T Data;
        public SaveFile(FileInfo file) : base(file) { }
    }

    public class UlongSaveFile : SaveFile<List<ulong>>
    {

        public override void Read()
        {
            if (!File.Exists) File.Create();

            using (StreamReader reader = new StreamReader(Open()))
            {
                string s;
                while ((s = reader.ReadLine()) != null) Data.Add(ulong.Parse(s));
            }

        }

        public override void Write()
        {
            using (StreamWriter writer = new StreamWriter(Open()))
                foreach (ulong value in Data)
                    writer.WriteLine($"{value}\n");
        }

        public UlongSaveFile(FileInfo file) : base(file) { }

    }
    public class UlongStringSaveFile : SaveFile<List<KeyValuePair<ulong, string>>>
    {
        public override void Read()
        {
            if (!File.Exists) File.Create();

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

        public override void Write()
        {
            using (StreamWriter writer = new StreamWriter(Open()))
                foreach (KeyValuePair<ulong, string> pair in Data)
                    writer.WriteLine($"{pair.Key}: {pair.Value}\n");
        }
        public UlongStringSaveFile(FileInfo file) : base(file) { }
    }
}