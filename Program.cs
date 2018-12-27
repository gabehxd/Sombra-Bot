using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Configsys;
using System.Reflection;
using Sombra_Bot.Utils;
using System.IO;
using Sombra_Bot.Commands;

namespace Sombra_Bot
{
    class Program
    {
        public static readonly DirectoryInfo save = new DirectoryInfo("save");
        private static string token;
        private DiscordSocketClient client;
        private CommandService Commands;

        static void Main()
        {
            save.Create();
            LoadConfig();
            Program program = new Program();
            program.MainAsync().GetAwaiter().GetResult();
        }

        private async Task MainAsync()
        {
            client = new DiscordSocketClient(new DiscordSocketConfig());

            Commands = new CommandService(new CommandServiceConfig
            {
                CaseSensitiveCommands = false,
                DefaultRunMode = RunMode.Async,
            });
            client.MessageReceived += MessageReceived;
            client.Ready += Client_Ready;
            await Commands.AddModulesAsync(Assembly.GetEntryAssembly());
            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();

            await Task.Delay(-1);
        }

        private async Task Client_Ready()
        {
#if !DEBUG
            await client.SetGameAsync("Hacking the planet | s.help");
#else
            await client.SetGameAsync("Hacking the planet | Debug Build");
#endif
            Console.WriteLine("Started!");
        }

        private async Task MessageReceived(SocketMessage arg)
        {
            SocketUserMessage Message = arg as SocketUserMessage;
            SocketCommandContext Context = new SocketCommandContext(client, Message);

            if (Context.Message == null || Context.Message.Content == "" || Context.User.IsBot) return;
            if (BotBan.banned.Exists)
            {
                if (IsUserBanned(Context.User.Id))
                {
                    await Error.Send(Context.Channel, Value: $"The use of Sombra Bot is currently restricted for <@{Context.User.Id}> by <@130825292292816897> ");
                    return;
                }
            }
            int ArgPos = 0;
#if !DEBUG
            if (!Message.HasStringPrefix("s.", ref ArgPos))
#else
            if (!Message.HasStringPrefix("d.", ref ArgPos))
#endif
            {
                Random rng = new Random();
                if (rng.Next(0, 4) == 0 && ShouldItBeLikeThat(Message.Content))
                {
                    await Message.Channel.TriggerTypingAsync();
                    await Task.Delay(500);
                    await Message.Channel.SendMessageAsync("Because it :b: like that.");
                }
                return;
            }

            IResult Result = await Commands.ExecuteAsync(Context, ArgPos);
            if (!Result.IsSuccess)
            {
                await Error.Send(Message.Channel, Value: Result.ErrorReason);

                //Console.WriteLine($"{DateTime.Now} at Commands] Something went wrong with executing a command. Text: {Context.Message.Content} | Error: {Result.ErrorReason}");
            }
        }

        private bool ShouldItBeLikeThat(string content)
        {
            string[] messagearray = content.Split(' ');
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

        private bool IsUserBanned(ulong id)
        {
            foreach (string user in File.ReadAllLines(BotBan.banned.FullName))
            {
                if (user == id.ToString())
                {
                    return true;
                }
            }
            return false;
        }

        private static void LoadConfig()
        {
            Config config = new Config();
            token = config.Token;
        }
    }
}
