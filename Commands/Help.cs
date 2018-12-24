using System.Threading.Tasks;
using Discord;
using Discord.Commands;


namespace Sombra_Bot.Commands
{
    public class Help : ModuleBase<SocketCommandContext>
    {
        [Command("Help"), Summary("Get help.")]
        public async Task Helpmsg()
        {
            await Context.Channel.TriggerTypingAsync();
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle("Help Menu");
            builder.AddField("AddRole", "Adds a role to the specified user.\nargs: <user> <role>");
            builder.AddField("RemoveRole", "Removes a role to the specified user.\nargs: <user> <role>");
            builder.AddField("OWStats", "Gets Overwatch stats\nargs: <Username>");
            builder.AddField("GetRelease", "Gets a release from the specificed Github repository\nargs: <repository owner> <repository name>");
            builder.AddField("Invite", "Gets an invite for Sombra Bot and Sombra Bot's discord");
            builder.AddField("Hack", "Kicks or bans a user\nargs: <level of hack, 1: kick, 2: ban> <user> <reason>");
#if !DEBUG
            builder.WithFooter("All commands should start with `s.`");
#else
            builder.WithFooter("All commands should start with `d.`");
#endif
            builder.WithCurrentTimestamp();
            builder.WithColor(Color.Purple);
            await Task.Delay(500);
            await Context.Channel.SendMessageAsync("Here are my commands:", embed: builder);
        }
    }
}
