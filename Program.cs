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
using Discord.Rest;

namespace Sombra_Bot
{
    class Program
    {
        public static DirectoryInfo roottemppath = new DirectoryInfo(Path.Combine(Path.GetTempPath(), "Sombra-Bot"));
        public static readonly DirectoryInfo save = new DirectoryInfo("save");
        private static string token;
        private DiscordSocketClient client;
        private CommandService Commands;
        public static string presence;
        public static string stream;
        public static bool IsStream;
        public static RestApplication AppInfo;


        static void Main()
        {
            roottemppath.Create();
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
            await Commands.AddModulesAsync(Assembly.GetEntryAssembly(), null);
            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();

            await Task.Delay(-1);
        }

        private async Task Client_Ready()
        {
            AppInfo = await client.GetApplicationInfoAsync();
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
                    await Task.Delay(10000);
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
                    await client.SetGameAsync($"{presence} | s.help", stream, ActivityType.Streaming);
#else
                    await client.SetGameAsync($"{presence} | Debug Build", stream, ActivityType.Streaming);
#endif
                }
                await Task.Delay(10000);
            }
        }

        private async Task MessageReceived(SocketMessage arg)
        {
            SocketUserMessage Message = arg as SocketUserMessage;
            SocketCommandContext Context = new SocketCommandContext(client, Message);

            //If guild is null then we are in DMs and we should not do anything w/ DMs
            if (string.IsNullOrEmpty(Context.Message.Content) || Context.User.IsBot || Context.Guild == null) return;
            int ArgPos = 0;

#if !DEBUG
            if (!Message.HasStringPrefix("s.", ref ArgPos))
#else
            if (!Message.HasStringPrefix("d.", ref ArgPos))
#endif
            {
                if (!AreMemesDisabled(Context.Guild.Id)) await ShouldItBeLikeThat(Message);
                return;
            }

            if (IsUserBanned(Context.User.Id))
            {
                await Error.Send(Context.Channel, Value: $"The use of Sombra Bot is currently restricted for {Context.User.Mention} by {AppInfo.Owner.Mention}");
                return;
            }

            await Context.Channel.TriggerTypingAsync();
            IResult Result = await Commands.ExecuteAsync(Context, ArgPos, null);
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
            if (rng.Next(0, 6) == 0 && (messagearray.Contains("y") || messagearray.Contains("why")))
            {
                await context.Channel.TriggerTypingAsync();
                await Task.Delay(500);
                await context.Channel.SendMessageAsync("Because it :b: like that.");
                return;
            }
            else if (rng.Next(0, 3) == 0 && (context.Content.ToLower().Contains("is gay") || context.Content.ToLower().Contains("are gay")))
            {
                await context.Channel.TriggerTypingAsync();
                await Task.Delay(500);
                await context.Channel.SendMessageAsync("It shall be known!");
                return;
            }
        }

        private bool AreMemesDisabled(ulong id)
        {
            if (DisableSpeak.DisabledMServers.Exists)
            {
                foreach (string server in File.ReadAllLines(DisableSpeak.DisabledMServers.FullName))
                {
                    if (ulong.Parse(server) == id) return true;
                }
            }
            return false;
        }

        private bool IsUserBanned(ulong id)
        {
            if (BotBan.Banned.Exists)
            {
                foreach (string user in File.ReadAllLines(BotBan.Banned.FullName))
                {
                    if (ulong.Parse(user) == id) return true;
                }
            }
            return false;
        }

        private static void LoadConfig()
        {
            Config config = new Config();
            token = config.Token;

            if (token == "xxxx")
            {
                Console.WriteLine("Config has not been found, the file has been created, configure it with you bot's token.");
                Console.ReadLine();
                Environment.Exit(0);
            }
        }
    }
}
