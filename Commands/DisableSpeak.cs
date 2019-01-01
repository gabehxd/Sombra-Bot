using Discord.Commands;
using Sombra_Bot.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Discord;
using System.Threading.Tasks;
using System.Linq;

namespace Sombra_Bot.Commands
{
    public class DisableSpeak : ModuleBase<SocketCommandContext>
    {
        public static FileInfo DisabledMServers => new FileInfo(Path.Combine("save", "BannedUsers.list"));

        [Command("DisableMemes"), Summary("Disables Sombra bot's random memes.")]
        [RequireUserPermission(GuildPermission.ManageGuild)]
        public async Task DisableChat()
        {
            if (DisabledMServers.Exists)
            {
                foreach (string id in File.ReadAllLines(DisabledMServers.FullName))
                {
                    if (ulong.Parse(id) == Context.Guild.Id)
                    {
                        await Error.Send(Context.Channel, Value: "Server already has memes disabled.");
                        return;
                    }
                }
            }
            File.AppendAllText(DisabledMServers.FullName, $"{Context.Guild.Id.ToString()}\n");
            await Context.Channel.SendMessageAsync("Memes for the server have been disabled.");
        }

        [Command("EnableMemes"), Summary("Enables Sombra bot's random memes.")]
        [RequireUserPermission(GuildPermission.ManageGuild)]
        public async Task EnableChat()
        {
            if (DisabledMServers.Exists)
            {
                List<string> disabledservers = File.ReadAllLines(DisabledMServers.FullName).ToList();
                if (disabledservers.Count != 0)
                {
                    bool Isfound = false;

                    for (int i = disabledservers.Count - 1; i >= 0; i--)
                    {
                        if (disabledservers[i] == Context.Guild.Id.ToString())
                        {
                            disabledservers.RemoveAt(i);
                            Isfound = true;
                        }
                    }
                    if (Isfound)
                    {
                        File.WriteAllLines(DisabledMServers.FullName, disabledservers.ToArray());
                        await Context.Channel.SendMessageAsync("Enabled random memes.");
                        return;
                    }
                }
            }
            await Error.Send(Context.Channel, Value: "You already have memes enabled.");
        }
    }
}
