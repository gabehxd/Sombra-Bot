using System.Collections.Generic;
using System.IO;
using Sombra_Bot.Commands;
using Sombra_Bot.Utils;

namespace Sombra_Bot.Utils
{
    public class Misc
    {
        public static string ConvertToReadableSize(double size)
        {
            string[] units = { "B", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
            int unit = 0;

            while (size >= 1024)
            {
                size /= 1024;
                ++unit;
            }

            return string.Format("{0:0.#} {1}", size, units[unit]);
        }
    }
}