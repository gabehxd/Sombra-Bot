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
        public async Task DiceRoll(int die = 2, int sides = 6)
        {
            if (die <= 0)
            {
                await Error.Send(Context.Channel, Value: "Die cannot be 0 or below.");
                return;
            }
            if (sides <= 1)
            {
                await Error.Send(Context.Channel, Value: "Sides cannot be 1 or below.");
                return;
            }

            Random rng = new Random();

            if (die > 400 || sides > 400)
            {
                int sum;
                try
                {
                    sum = rng.Next(die, die * sides + 1);
                }
                catch
                {
                    await Error.Send(Context.Channel, Value: "Inputted values were too large!");
                    return;
                }
                
                await Context.Channel.SendMessageAsync("Value over 400, calculating sum only!");
                await Context.Channel.SendMessageAsync($"You rolled a sum of {sum} \uD83C\uDFB2");


            }
            else
            {
                int sum = 0;
                List<int> rc = new List<int>();
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
        }

        [Command("Flip"), Summary("Flips a coin.")]
        public async Task CoinFlip()
        {
            Random rng = new Random();
            switch (rng.Next(0, 2))
            {
                case 0:
                    await Context.Channel.SendMessageAsync("Heads!");
                    return;
                case 1:
                    await Context.Channel.SendMessageAsync("Tails!");
                    return;
                //just incase
                default:
                    await Error.Send(Context.Channel, Value: "Command failed: error reported!", e: new Exception("CoinFlip: Internal value is not 0 or 1!"), et: Error.ExceptionType.Fatal);
                    return;
            }
        }
    }
}
