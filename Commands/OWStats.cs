using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using OverwatchAPI;
using Sombra_Bot.Utils;
using System.Collections.Generic;

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
                    await Error.Send("User does not exist.", Context.Channel);
                    return;
                }
                if (player.IsProfilePrivate) await Error.Send("Player profile is private", Context.Channel, "Change your profile to public");

                EmbedBuilder builder = new EmbedBuilder();
                builder.WithTitle($"{player.Username}'s Stats");
                builder.WithUrl(player.ProfileUrl);
                builder.WithColor(Color.Orange);
                builder.WithCurrentTimestamp();
                builder.WithThumbnailUrl(player.ProfilePortraitUrl);
                builder.AddField("Player Level (Not Inlcuding Prestige(s))", player.PlayerLevel);
                if (player.CompetitiveRank == 0) builder.AddField("Competetive Rank", "Not Placed");
                else builder.AddField("Competetive Rank", $"SR: {player.CompetitiveRank}: {GetRankName(player.CompetitiveRank)}");
                //builder.AddInlineField("Achievements", $"{player.Achievements.Count}/{player.Achievements.Capacity}"); bugged
                string endorvalue = "";
                foreach (KeyValuePair<Endorsement, decimal> s in player.Endorsements)
                {
                    //maybe multiply by 100?
                    string endor = $"{s.Key}: {s.Value.ToString().Replace("0.", " ")}%, ";
                    endorvalue += endor;
                }
                builder.AddField($"Endorsment Level: {player.EndorsementLevel}", endorvalue);
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
