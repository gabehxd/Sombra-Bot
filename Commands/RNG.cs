using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sombra_Bot.Utils;

namespace Sombra_Bot.Commands
{
    public class RNG : ModuleBase<SocketCommandContext>
    {
        [Command("Roll"), Summary("Rolls a dice"), Alias("Dice", "DiceRoll")]
        public async Task DiceRoll(int die = 1, int sides = 6)
        {
            if (die <= 0)
            {
                await Error.Send(Context.Channel, Value: "Die can be 0 or below.");
                return;
            }
            if (sides <= 1)
            {
                await Error.Send(Context.Channel, Value: "Sides can be 1 or below.");
                return;
            }
            //Prevent RAM usage hell, thanks simon :)
            if (die > 100 || sides > 100)
            {
                await Error.Send(Context.Channel, Value: "No value can be over 100.");
                return;
            }

            Random rng = new Random();
            List<int> rc = new List<int>();
            int sum = 0;
            for (int i = 0; i < die; i++)
            {
                int num = rng.Next(1, sides + 1);
                sum += num;
                rc.Add(num);
            }
            string msg = $"You rolled: {string.Join(", ", rc)} \uD83C\uDFB2";
            if (rc.Count > 1) msg += $"\nSum: {sum.ToString()}";

            await Context.Channel.SendMessageAsync(msg);
        }

        [Command("Flip"), Summary("Flips a coin.")]
        public async Task CoinFlip()
        {
            Random rng = new Random();
            switch (rng.Next(0, 2))
            {
                case 0:
                    await Context.Channel.SendMessageAsync("Heads!");
                    break;
                case 1:
                    await Context.Channel.SendMessageAsync("Tails!");
                    break;
                //just incase
                default:
                    await Error.Send(Context.Channel, Value: "Command failed: error reported!", e: new Exception("CoinFlip: Internal value is not 0 or 1!"), et: Error.ExceptionType.Fatal);
                    break;
            }
        }
    }
}
