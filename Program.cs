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
using System.Linq;

namespace Sombra_Bot
{
    class Program
    {
        public static readonly DirectoryInfo save = new DirectoryInfo("save");
        private static string token;
        private DiscordSocketClient client;
        private CommandService Commands;
        public static string presence;
        public static string stream;
        public static bool IsStream;

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
            Console.WriteLine("Started!");
            while (true)
            {
                if (presence == null && !IsStream)
                {
#if !DEBUG
                    await client.SetGameAsync("hacking the planet | s.help");
#else
                    await client.SetGameAsync("hacking the planet | Debug Build");
#endif
                    await Task.Delay(8000);
                    if (client.Guilds.Count > 1)
                    {
#if !DEBUG
                        await client.SetGameAsync($"on {client.Guilds.Count} servers | s.help");
#else
                        await client.SetGameAsync($"on {client.Guilds.Count} servers | Debug Build");
#endif
                    }
                }
                else if (presence != null && !IsStream)
                {
#if !DEBUG
                    await client.SetGameAsync($"{presence} | s.help");
#else
                    await client.SetGameAsync($"{presence} | Debug Build");
#endif
                }
                else if (presence != null && IsStream)
                {
#if !DEBUG
                    await client.SetGameAsync($"{presence} | s.help", stream, StreamType.Twitch);
#else
                    await client.SetGameAsync($"{presence} | Debug Build", stream, StreamType.Twitch);
#endif

                }
                await Task.Delay(8000);
            }
        }

        private async Task MessageReceived(SocketMessage arg)
        {
            SocketUserMessage Message = arg as SocketUserMessage;
            SocketCommandContext Context = new SocketCommandContext(client, Message);

            if (Context.Message == null || Context.Message.Content == "" || Context.User.IsBot) return;
            int ArgPos = 0;

#if !DEBUG
            if (!Message.HasStringPrefix("s.", ref ArgPos))
#else
            if (!Message.HasStringPrefix("d.", ref ArgPos))
#endif
            {
                await ShouldItBeLikeThat(Message);
                return;
            }

            if (BotBan.Banned.Exists)
            {
                if (IsUserBanned(Context.User.Id))
                {
                    await Error.Send(Context.Channel, Value: $"The use of Sombra Bot is currently restricted for <@{Context.User.Id}> by <@130825292292816897>");
                    return;
                }
            }

            await Context.Channel.TriggerTypingAsync();
            IResult Result = await Commands.ExecuteAsync(Context, ArgPos);
            if (!Result.IsSuccess)
            {
                await Error.Send(Message.Channel, Key: Result.ErrorReason);
                //Console.WriteLine($"{DateTime.Now} at Commands] Something went wrong with executing a command. Text: {Context.Message.Content} | Error: {Result.ErrorReason}");
            }
        }

        private async Task ShouldItBeLikeThat(SocketUserMessage context)
        {
            string[] messagearray = context.Content.ToLower().Split(' ');
            Random rng = new Random();
            if (messagearray.Contains("y") || messagearray.Contains("why") && rng.Next(0, 4) == 0)
            {
                await context.Channel.TriggerTypingAsync();
                await Task.Delay(500);
                await context.Channel.SendMessageAsync("Because it :b: like that.");
                return;
            }
            else if (context.Content.ToLower().Contains("is gay") && rng.Next(0, 4) == 0)
            {
                await context.Channel.TriggerTypingAsync();
                await Task.Delay(500);
                await context.Channel.SendMessageAsync("It shall be known!");
                return;
            }
            else return;
        }

        private bool IsUserBanned(ulong id)
        {
            foreach (string user in File.ReadAllLines(BotBan.Banned.FullName))
            {
                if (user == id.ToString()) return true;
            }
            return false;
        }

        private static void LoadConfig()
        {
            Config config = new Config();
            token = config.Token;

            if (token == "xxxx")
            {
                Console.WriteLine("Config has not been found, it has been created configure it with you bot's token.");
                Console.ReadLine();
                Environment.Exit(0);
            }
        }
    }
}
