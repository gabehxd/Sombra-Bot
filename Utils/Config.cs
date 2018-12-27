/*
 * Copyright (c) 2018 Random
 *
 * This program is free software; you can redistribute it and/or modify it
 * under the terms and conditions of the GNU General Public License,
 * version 2, as published by the Free Software Foundation.
 *
 * This program is distributed in the hope it will be useful, but WITHOUT
 * ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
 * FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for
 * more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
using System.IO;

namespace Configsys
{
    public class Config
    {
        private static readonly FileInfo CONFIG_PATH = new FileInfo("config.txt");

        public string Token { get; set; } = null;

        public Config()
        {
            if (File.Exists(CONFIG_PATH.FullName))
            {
                string[] lines = File.ReadAllLines(CONFIG_PATH.FullName);

                foreach (string line in lines)
                {
                    string[] parts = line.Replace("\t", "").Split('=');

                    if (parts.Length == 2)
                    {
                        try
                        {
                            parts[1] = parts[1].TrimStart(' ');
                            switch (parts[0].TrimEnd(' '))
                            {
                                case "token":
                                    Token = parts[1];
                                    break;
                            }
                        }
                        catch { }
                    }
                }
            }
        }
    }
}