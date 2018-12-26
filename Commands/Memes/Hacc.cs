using System;
using System.Collections.Generic;
using Discord.Commands;
using System.Text;
using System.Threading.Tasks;
using Discord;

namespace Sombra_Bot.Commands.Memes
{
    public class Hacc : ModuleBase<SocketCommandContext>
    {
        [Command("Hacc"), Summary("Bans or kicks a user.")]
        public async Task Hac(IUser user)
        {
            await Context.Channel.SendMessageAsync($"<@{user.Id}> hacc'd >:3");
        }
    }
}
