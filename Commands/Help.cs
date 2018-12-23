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
            builder.AddField("GetRelease", "Gets a release from the specificed Github repository\n args: <repository owner> <repository name>");
            builder.AddField("OWStats", "Gets Overwatch stats\n args: <Battle.net tag>");
            builder.AddField("Hack", "Kicks or Bans a user\n args: <level of hack, 1: kick, 2: ban> <user> <reason>");
            builder.WithFooter("All commands should start with `s.`");
            builder.WithColor(Color.Purple);

            Task.Delay(500);
            await Context.Channel.SendMessageAsync("Here are my commands:", embed: builder);
        }
    }
}
