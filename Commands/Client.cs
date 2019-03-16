using Discord.Commands;
using Sombra_Bot.Utils;
using System;
using System.Threading.Tasks;

namespace Sombra_Bot.Commands
{
    public class Client : ModuleBase<SocketCommandContext>
    {
        [Command("Shutdown"), Summary("Shut downs the bot.")]
        [RequireOwner]
        public async Task ShutDown()
        {
            await Context.Channel.SendMessageAsync("Bye bitch.");
            await Context.Client.LogoutAsync();
            Environment.Exit(0);
        }

        [Command("Say"), Summary("Says the message sent.")]
        [RequireOwner]
        public async Task Say(params string[] input)
        {
            try
            {
                await Context.Message.DeleteAsync();
            }
            catch { }
            await Context.Channel.SendMessageAsync(string.Join(" ", input));
        }

        [Command("Invite"), Summary("Get an invite.")]
        public async Task GetInvite()
        {
            await Context.Channel.SendMessageAsync("Invite link: https://discordbots.org/bot/516009170353258496\nDiscord server: https://discord.gg/jQ8HuWE\nSource code: https://github.com/SunTheCourier/Sombra-Bot");
        }

        [Command("ClearTemp"), Summary("Clears the Temp Directory for Sombra Bot.")]
        [RequireOwner]
        public async Task Clear()
        {
            try
            {
                Program.roottemppath.Delete(true);
            }
            catch
            {
                await Error.Send(Context.Channel, Value: "Failed to delete temp folder.");
                return;
            }
            await Context.Channel.SendMessageAsync("Done!");
        }
    }
}
