using System.IO;
using Discord.Commands;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Sombra_Bot.Utils;

namespace Sombra_Bot.Commands
{
    public class BotBan : ModuleBase<SocketCommandContext>
    {
        public static FileInfo Banned => new FileInfo(Path.Combine("save", "BannedUsers.list"));

        [Command("AddBotBan"), Summary("Ban a user from this bot.")]
        [RequireOwner]
        public async Task AddBan(ulong ID)
        {
            if (Banned.Exists)
            {
                foreach (string user in File.ReadAllLines(Banned.FullName))
                {
                    if (ulong.Parse(user) == ID)
                    {
                        await Error.Send(Context.Channel, Value: "User is already banned.");
                        return;
                    }
                }
            }
            File.AppendAllText(Banned.FullName, $"{ID.ToString()}\n");
            await Context.Channel.SendMessageAsync("User added to ban list.");
        }

        [Command("RemoveBotBan"), Summary("Removes banned user from this bot.")]
        [RequireOwner]
        public async Task RemoveBan(ulong ID)
        {
            if (Banned.Exists)
            {
                List<string> bannedusers = File.ReadAllLines(Banned.FullName).ToList();
                if (bannedusers.Count != 0)
                {
                    bool Isfound = false;

                    for (int i = bannedusers.Count - 1; i >= 0; i--)
                    {
                        if (bannedusers[i] == ID.ToString())
                        {
                            bannedusers.RemoveAt(i);
                            Isfound = true;
                        }
                    }
                    if (Isfound)
                    {
                        File.WriteAllLines(Banned.FullName, bannedusers.ToArray());
                        await Context.Channel.SendMessageAsync("User removed from ban list.");
                        return;
                    }
                    else await Error.Send(Context.Channel, Value: "No user with that ID is banned.");
                }
            }
            await Error.Send(Context.Channel, Value: "No users are banned.");
        }
    }
}
