using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using OverwatchAPI;
using Sombra_Bot.Utils;
using System.Linq;

namespace Sombra_Bot.Commands
{
    public class OWStats : ModuleBase<SocketCommandContext>
    {
        [Command("OWStats"), Summary("Gets your Overwatch stats!")]
        public async Task GetstatsOW(string User)
        {
            await Context.Channel.TriggerTypingAsync();
            using (var owClient = new OverwatchClient())
            {
                Player player;
                try
                {
                    player = await owClient.GetPlayerAsync(User);
                }
                catch
                {
                    await Error.Send(Context.Channel, Value: "User does not exist.");
                    return;
                }
                if (player.IsProfilePrivate) await Error.Send(Context.Channel, Value: "Player profile is private");

                EmbedBuilder builder = new EmbedBuilder();
                builder.WithTitle($"{player.Username}'s Stats");
                builder.WithUrl(player.ProfileUrl);
                builder.WithColor(Color.Orange);
                builder.WithCurrentTimestamp();
                builder.WithThumbnailUrl(player.ProfilePortraitUrl);
                builder.AddField("Player Level (Not Inlcuding Prestige(s))", player.PlayerLevel);
                if (player.CompetitiveRank == 0) builder.AddField("Competetive Rank", "Not Placed");
                else builder.AddField($"Competetive Rank: {GetRankName(player.CompetitiveRank)}", $"SR: {player.CompetitiveRank}");
                //builder.AddInlineField("Achievements", $"{player.Achievements.Count}/{player.Achievements.Capacity}"); bugged
                builder.AddField($"Endorsment Level: {player.EndorsementLevel}", $"{player.Endorsements.Keys.ToArray()[0]}: {player.Endorsements.Values.ToArray()[0].ToString().Replace("0.", " ")}%, {player.Endorsements.Keys.ToArray()[1]}: {player.Endorsements.Values.ToArray()[1].ToString().Replace("0.", " ")}%, {player.Endorsements.Keys.ToArray()[2]}: {player.Endorsements.Values.ToArray()[2].ToString().Replace("0.", " ")}%");
                builder.AddField("Platform", player.Platform.ToString().ToUpper());

                await Context.Channel.SendMessageAsync("", embed: builder);
            }
        }

        private string GetRankName(int SR)
        {
            switch (SR)
            {
                case int n when n >= 1 && n <= 1499:
                    return "Bronze";

                case int n when n >= 1500 && n <= 1999:
                    return "Silver";

                case int n when n >= 2000 && n <= 2499:
                    return "Gold";

                case int n when n >= 2500 && n <= 2999:
                    return "Platinum";

                case int n when n >= 3000 && n <= 3499:
                    return "Diamond";

                case int n when n >= 3500 && n <= 3999:
                    return "Master";

                case int n when n >= 4000:
                    return "Grandmaster";
                default:
                    return null;
            }
        }

    }
}
