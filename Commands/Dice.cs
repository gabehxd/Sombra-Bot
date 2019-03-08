using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sombra_Bot.Utils;

namespace Sombra_Bot.Commands
{
    public class Dice : ModuleBase<SocketCommandContext>
    {
        [Command("Roll"), Summary("Rolls a dice")]
        public async Task DiceRoll(int sides, int die)
        {
            if (die <= 0 || sides <= 0)
            {
                await Error.Send(Context.Channel, Value: "No value can be 0 or below.");
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
                //if the sides is 4 i only get values >= 3
                int num = rng.Next(1, sides + 1);
                sum += num;
                rc.Add(num);
            }
            await Context.Channel.SendMessageAsync($"You rolled: {string.Join(", ", rc)} \uD83C\uDFB2\nSum: {sum.ToString()}");
        }
    }
}
