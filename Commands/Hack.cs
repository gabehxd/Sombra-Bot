using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Sombra_Bot.Utils;

namespace Sombra_Bot.Commands
{
    public class Hack : ModuleBase<SocketCommandContext>
    {
        [Command("Hack"), Summary("Bans or kicks a user.")]
        public async Task ManageUser(int level, IGuildUser user, string reason = null)
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
                    if (user.GuildPermissions.KickMembers)
                    {
                        try
                        {
                            await user.KickAsync(reason);
                            await Context.Channel.SendMessageAsync(Getmessage());
                            await Context.Channel.TriggerTypingAsync();
                            await Context.Channel.SendMessageAsync($"{user} Hacked!");
                        }
                        catch
                        {
                            await Error.Send(Context.Channel, Value: "User could not be kicked, insufficent permissions.");
                        }
                    }
                    else await Error.Send(Context.Channel, Value: "User requires guild permission KickMembers");

                    break;
                case 2:
                    if (user.GuildPermissions.BanMembers)
                    {
                        try
                        {
                            await Context.Guild.AddBanAsync(user, reason: reason);
                            await Context.Channel.SendMessageAsync(Getmessage());
                            await Context.Channel.TriggerTypingAsync();
                            await Context.Channel.SendMessageAsync($"{user} Hacked!");
                        }
                        catch
                        {
                            await Error.Send(Context.Channel, Value: "User could not be banned, insufficent permissions.");
                        }
                    }
                    else await Error.Send(Context.Channel, Value: "User requires guild permission BanMembers");
                    break;
                default:
                    await Error.Send(Context.Channel, Key: "Inputted hack level does not exist");
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
