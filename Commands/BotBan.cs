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
        public static readonly FileInfo banned = new FileInfo("BannedUsers.list");

        [Command("AddBotBan", RunMode = RunMode.Async), Summary("Ban a user from this bot.")]
        [RequireOwner]
        public async Task AddBan(ulong ID)
        {
            await Context.Channel.TriggerTypingAsync();
            if (banned.Exists)
            {
                foreach (string user in File.ReadAllLines(banned.FullName))
                {
                    if (ulong.Parse(user) == ID)
                    {
                        await Error.Send(Context.Channel, Value: "User is already banned.");
                        return;
                    }
                }
            }
            File.AppendAllText(banned.FullName, $"{ID.ToString()}\n");
            await Context.Channel.SendMessageAsync("User added to ban list <:delte:527696678476709918>.");
        }

        [Command("RemoveBotBan", RunMode = RunMode.Async), Summary("Removes banned user from this bot.")]
        [RequireOwner]
        public async Task RemoveBan(ulong ID)
        {
            await Context.Channel.TriggerTypingAsync();
            if (banned.Exists)
            {
                List<string> bannedusers = File.ReadAllLines(banned.FullName).ToList();
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
                        File.WriteAllLines(banned.FullName, bannedusers.ToArray());
                        await Context.Channel.SendMessageAsync("User removed from ban list!");
                    }
                    else await Error.Send(Context.Channel, Value: "No user with that ID is banned.");
                }
                else await Error.Send(Context.Channel, Value: "No users are banned.");
            }
            else
            {
                await Error.Send(Context.Channel, Value: "No users are banned.");
            }
        }
    }
}
