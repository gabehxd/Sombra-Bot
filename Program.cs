using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Configsys;

namespace Sunny_Bot
{
    class Program
    {
        private static string token;
        private DiscordSocketClient client;
        private CommandService Commands;

        static void Main()
        {
            LoadConfig();
            Program program = new Program();
            program.MainAsync().GetAwaiter().GetResult();
        }

        private async Task MainAsync()
        {
            client = new DiscordSocketClient(new DiscordSocketConfig());

            Commands = new CommandService(new CommandServiceConfig
            {
                CaseSensitiveCommands = true,
                DefaultRunMode = RunMode.Async,
            });
            client.MessageReceived += MessageReceived;
            client.Ready += Client_Ready;
            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();

            await Task.Delay(-1);
        }

        private async Task Client_Ready()
        {
            Console.WriteLine("Started!");
            await client.SetGameAsync("<3", "https://github.com/SunTheCourier", StreamType.NotStreaming);
        }

        private async Task MessageReceived(SocketMessage arg)
        {
            var Message = arg as SocketUserMessage;
            var Context = new SocketCommandContext(client, Message);

            if (Context.Message.Content == "") return;
            if (Context.User.IsBot) return;

            Random rng = new Random();
            if (Shoulditbelikethat(Message) && rng.Next(0, 4) == 0)
            {
                await Message.Channel.TriggerTypingAsync();
                await Task.Delay(1000);
                await Message.Channel.SendMessageAsync("Because it :b: like that.");
            }
            /*
            int ArgPos = 0;
            if (!(Message.HasStringPrefix("s!", ref ArgPos) || Message.HasMentionPrefix(client.CurrentUser, ref ArgPos))) return;

            var Result = await Commands.ExecuteAsync(Context, ArgPos);
            if (!Result.IsSuccess)
                Console.WriteLine($"{DateTime.Now} at Commands] Something went wrong with executing a command. Text: {Context.Message.Content} | Error: {Result.ErrorReason}");
                */
        }

        private static void LoadConfig()
        {
            Config config = new Config();
            token = config.Token;
        }
        private static bool Shoulditbelikethat(SocketUserMessage message)
        {
            string[] messagearray = message.Content.Split(' ');
            foreach (string why in messagearray)
            {
                switch (why)
                {
                    case "why":
                    case "y":
                        return true;
                }
            }
            return false;
        }
    }
}
