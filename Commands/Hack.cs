﻿using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Sombra_Bot.Utils;

namespace Sombra_Bot.Commands
{
    public class Hack : ModuleBase<SocketCommandContext>
    {
        [Command("Hack"), Summary("Bans or kicks a user.")]
        //Is there a way to check if a user has one perm or the other? In the main function possibly?
        [RequireUserPermission(GuildPermission.KickMembers)]
        [RequireUserPermission(GuildPermission.BanMembers)]
        public async Task ManageUser(int level, IUser user, string reason = null)
        {
            if (Context.User == user && level == 1)
            {
                await Context.Channel.SendMessageAsync("You can't kick yourself lmfao.");
                return;
            }

            if (Context.User == user && level == 2)
            {
                await Context.Channel.SendMessageAsync("You can't ban yourself lmfao.");
                return;
            }

            switch (level)
            {
                case 1:
                    //no kick method?
                    await Context.Guild.AddBanAsync(user, reason: reason);
                    await Context.Guild.RemoveBanAsync(user);
                    await Context.Channel.TriggerTypingAsync();
                    await Task.Delay(500);
                    await Context.Channel.SendMessageAsync(Getmessage());
                    await Context.Channel.TriggerTypingAsync();
                    await Task.Delay(500);
                    await Context.Channel.SendMessageAsync($"{user} Hacked!");
                    break;
                case 2:
                    await Context.Guild.AddBanAsync(user, reason: reason);
                    await Context.Channel.TriggerTypingAsync();
                    await Task.Delay(500);
                    await Context.Channel.SendMessageAsync(Getmessage());
                    await Context.Channel.TriggerTypingAsync();
                    await Task.Delay(500);
                    await Context.Channel.SendMessageAsync($"{user} Hacked!");
                    break;
                default:
                    await Error.Send(Context.Channel, Value: "Inputted hack level does not exist");
                    break;
            }
        }
        private string Getmessage()
        {
            Random rng = new Random();
            switch (rng.Next(1, 4))
            {
                case 1:
                    return "Initiating the hack.";
                case 2:
                    return "Iniciando el hackeo.";
                case 3:
                    return "Don't mind me.";
                case 4:
                    return "Let's get started.";
            }
            return null;
        }
    }
}
