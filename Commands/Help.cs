using System.Threading.Tasks;
using Discord;
using System.Collections.Generic;
using Discord.Commands;
using Sombra_Bot.Utils;

namespace Sombra_Bot.Commands
{
    public class Help : ModuleBase<SocketCommandContext>
    {

        static private readonly Dictionary<string, string> commands = new Dictionary<string, string>
        {
            { "Help", "DMs you the help menu, arguments are optional.\n args: <command>." },
            { "ListRoles",  "Lists all roles in the current server." },
            { "AddRole", "Adds a role to the specified user, requires manage roles permission.\nargs: <user> <role>." },
            { "RemoveRole", "Removes a role from the specified user, requires manage roles permission.\nargs: <user> <role>." },
            { "Hack", "Kicks or bans a user, requires ban/kick permissions.\nargs: <level of hack, 1: kick, 2: ban> <user> <reason>." },
            { "Hacc", "Haccs a user >:3.\nargs: <user>"},
            { "GetRelease", "Gets a release from the specificed Github repository.\nargs: <repository owner> <repository name>." },
            { "Roll", "Rolls a max of 100 die with a max of 100 sides\nargs: <die> <sides>." },
            { "Flip", "Flips a coin."},
            { "Avatar", "Gets a user's Avatar.\nargs: <user>." },
            { "OWStats", "Gets Overwatch stats.\nargs: <username>." },
            { "EnableMemes", "Re-enables random memes, requires the manage server permission. Enabled by default." },
            { "DisableMemes", "Disables random memes from being sent into the server, requires the manage server permission."},
            { "Invite", "Gets an invite for Sombra Bot and Sombra Bot's discord server." },
            { "Suggest", $"Suggest a feature for {Program.AppInfo.Owner.Mention} to add. Do not spam this command or you will be banned from using this bot :).\nargs: <suggestion>" }
        };

        [Command("Help"), Summary("Get help.")]
        public async Task Helpmsg(string command = null)
        {
            //Can the help message be automated?
            string msg = "";
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle("Help Menu");
#if !DEBUG
            builder.WithFooter("All commands should start with `s.`");
#else
            builder.WithFooter("Debug build");
#endif
            builder.WithCurrentTimestamp();
            builder.WithColor(Color.Purple);

            if (command == null)
            {
                msg = "Here are my commands:";

                foreach (KeyValuePair<string, string> keyValues in commands)
                {
                    builder.AddField(keyValues.Key, keyValues.Value);
                }
            }
            else
            {
                bool Isfound = false;
                foreach (KeyValuePair<string, string> keyValues in commands)
                {
                    if (keyValues.Key.ToLower() == command.ToLower())
                    {
                        Isfound = true;
                        builder.AddField(keyValues.Key, keyValues.Value);
                        msg = $"Here is the `{keyValues.Key}` command:";
                        break;
                    }
                }

                if (Isfound)
                {
                    await Context.Channel.SendMessageAsync(msg, embed: builder.Build());
                    return;
                }
                else
                {
                    await Error.Send(Context.Channel, Key: "That command does not exist");
                    return;
                }
            }
            await Context.Channel.SendMessageAsync("The help menu has been sent to your DMs!");
            await Context.User.SendMessageAsync(msg, embed: builder.Build());
        }
    }
}
