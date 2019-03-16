using Discord.Commands;
using Sombra_Bot.Utils;
using Discord;
using System.Threading.Tasks;

namespace Sombra_Bot.Commands
{
    public class ManageMemes : ModuleBase<SocketCommandContext>
    {
        [Command("DisableMemes"), Summary("Disables Sombra bot's random memes.")]
        [RequireUserPermission(GuildPermission.ManageGuild)]
        public async Task DisableChat()
        {
            if (Save.DisabledMServers.Data.Contains(Context.Guild.Id))
            {
                await Context.Channel.SendMessageAsync("Memes for the server have been disabled.");
                return;
            }

            Save.DisabledMServers.Data.Add(Context.Guild.Id);
            await Context.Channel.SendMessageAsync("Memes for the server have been disabled.");
        }

        [Command("EnableMemes"), Summary("Enables Sombra bot's random memes.")]
        [RequireUserPermission(GuildPermission.ManageGuild)]
        public async Task EnableChat()
        {
            if (Save.DisabledMServers.Data.Count != 0)
            {
                if (Save.DisabledMServers.Data.Remove(Context.Guild.Id))
                {
                    await Context.Channel.SendMessageAsync("Enabled random memes.");
                    return;
                }
            }
            await Error.Send(Context.Channel, Value: "You already have memes enabled.");
        }
    }
}
