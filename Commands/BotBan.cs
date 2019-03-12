using Discord.Commands;
using System.Threading.Tasks;
using Sombra_Bot.Utils;

namespace Sombra_Bot.Commands
{
    public class BotBan : ModuleBase<SocketCommandContext>
    {
        //public static FileInfo Banned => new FileInfo(Path.Combine(Save.save.FullName, "BannedUsers.obj"));

        [Command("AddBotBan"), Summary("Ban a user from this bot.")]
        [RequireOwner]
        public async Task AddBan(ulong ID)
        {
            //better search function?
            foreach (string user in Save.BannedUsers)
            {
                if (ulong.Parse(user) == ID)
                {
                    await Error.Send(Context.Channel, Value: "User is already banned.");
                    return;
                }
            }
            Save.BannedUsers.Add(ID.ToString());
            await Context.Channel.SendMessageAsync("User added to ban list.");
        }

        [Command("RemoveBotBan"), Summary("Removes banned user from this bot.")]
        [RequireOwner]
        public async Task RemoveBan(ulong ID)
        {
            if (Save.BannedUsers.Count != 0)
            {
                if (Save.BannedUsers.Remove(ID.ToString()))
                {
                    await Context.Channel.SendMessageAsync("User removed from ban list.");
                    return;
                }
                await Error.Send(Context.Channel, Value: "No user with that ID is banned.");
            }
            await Error.Send(Context.Channel, Value: "No users are banned.");
        }
    }
}
