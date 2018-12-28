using System.Threading.Tasks;
using Discord;
using System.Collections.Generic;
using Discord.Commands;
using Sombra_Bot.Utils;


namespace Sombra_Bot.Commands
{
    public class Help : ModuleBase<SocketCommandContext>
    {
        private readonly Dictionary<string, string> commands = new Dictionary<string, string>
            {
                { "AddRole", "Adds a role to the specified user.\nargs: <user> <role>" },
                { "RemoveRole", "Removes a role from the specified user.\nargs: <user> <role>" },
                { "Hack", "Kicks or bans a user.\nargs: <level of hack, 1: kick, 2: ban> <user> <reason>" },
                { "Hacc", "Haccs a user >:3.\nargs: <user>"},
                { "GetRelease", "Gets a release from the specificed Github repository\nargs: <repository owner> <repository name>" },
                { "OWStats", "Gets Overwatch stats\nargs: <Username>" },
                { "Suggest",  "Suggest a feature for <@130825292292816897> to add. Do not spam this command or you will be banned from using this bot :).\nargs: <suggestion>" },
                { "Invite", "Gets an invite for Sombra Bot and Sombra Bot's discord" }
            };

        [Command("Help"), Summary("Get help.")]
        public async Task Helpmsg(string command = null)
        {
            //Can the help message be automated?
            await Context.Channel.TriggerTypingAsync();
            string msg = "";
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle("Help Menu");
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
                    if(keyValues.Key.ToLower() == command.ToLower())
                    {
                        Isfound = true;
                        builder.AddField(keyValues.Key, keyValues.Value);
                        msg = $"Here is the `{keyValues.Key}` command:";
                        break;
                    }
                }

                if (!Isfound)
                {
                    await Error.Send(Context.Channel, Key: "That command does not exist");
                    return;
                }
            }
#if !DEBUG
            builder.WithFooter("All commands should start with `s.`");
#else
            builder.WithFooter("Debug build");
#endif
            builder.WithCurrentTimestamp();
            builder.WithColor(Color.Purple);
            await Task.Delay(500);
            await Context.Channel.SendMessageAsync(msg, embed: builder);
        }
    }
}
