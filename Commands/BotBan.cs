using Discord.Commands;
using System.Threading.Tasks;
using Sombra_Bot.Utils;
using Discord;

namespace Sombra_Bot.Commands
{
    public class BotBan : ModuleBase<SocketCommandContext>
    {
        [Command("AddBotBan"), Summary("Ban a user from this bot.")]
        [RequireOwner]
        public async Task AddBan(ulong ID)
        {
            //better search function?
            foreach (ulong user in Save.BannedUsers.Data)
            {
                if (user == ID)
                {
                    await Error.Send(Context.Channel, Value: "User is already banned.");
                    return;
                }
            }
            Save.BannedUsers.Data.Add(ID);
            await Context.Channel.SendMessageAsync("User added to ban list.");
        }

        [Command("RemoveBotBan"), Summary("Removes banned user from this bot.")]
        [RequireOwner]
        public async Task RemoveBan(ulong ID)
        {
            if (Save.BannedUsers.Data.Count != 0)
            {
                if (Save.BannedUsers.Data.Remove(ID))
                {
                    await Context.Channel.SendMessageAsync("User removed from ban list.");
                    return;
                }
                await Error.Send(Context.Channel, Value: "No user with that ID is banned.");
            }
            await Error.Send(Context.Channel, Value: "No users are banned.");
        }

        [Command("ListBotBans"), Summary("Removes banned user from this bot."), Alias("Bans")]
        [RequireOwner]
        public async Task ListBans()
        {
            if (Save.BannedUsers.Data.Count != 0)
            {
                EmbedBuilder builder = new EmbedBuilder();
                string s = "";
                foreach (ulong ID in Save.BannedUsers.Data)
                {
                    s += $"<@{ID}>\n";
                }
                builder.WithColor(Color.Purple);
                builder.WithTitle("Banned Users");
                builder.WithDescription(s);
                builder.WithCurrentTimestamp();
                await Context.Channel.SendMessageAsync(embed: builder.Build());
                return;
            }
            await Error.Send(Context.Channel, Value: "No users have been banned.");
        }
    }
}
