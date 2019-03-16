using System.IO;

namespace Sombra_Bot.Utils
{
    public static class FS
    {
        public static FileInfo GetFile(this DirectoryInfo obj, string filename)
        {
            return new FileInfo($"{obj.FullName}{Path.DirectorySeparatorChar}{filename}");
        }
    }
}