﻿using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Configsys;
using System.Reflection;

namespace Sombra_Bot
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
            await client.SetGameAsync("<3", "https://github.com/SunTheCourier", StreamType.NotStreaming);
            Console.WriteLine("Started!");
        }

        private async Task MessageReceived(SocketMessage arg)
        {
            SocketUserMessage Message = arg as SocketUserMessage;
            SocketCommandContext Context = new SocketCommandContext(client, Message);

            if (Context.Message == null || Context.Message.Content == "") return;
            if (Context.User.IsBot) return;
            int ArgPos = 0;

            if (!Message.HasStringPrefix("s.", ref ArgPos))
            {
                Random rng = new Random();
                if (rng.Next(0, 4) == 0 && Shoulditbelikethat(Message))
                {
                    await Message.Channel.TriggerTypingAsync();
                    await Task.Delay(800);
                    await Message.Channel.SendMessageAsync("Because it :b: like that.");
                }
                return;
            }

            IResult Result = await Commands.ExecuteAsync(Context, ArgPos);
            if (!Result.IsSuccess)
            {
                EmbedBuilder builder = new EmbedBuilder();
                builder.WithTitle("Error");
                builder.AddField(Result.ErrorReason, "View the help menu for command usage.");
                builder.WithColor(Color.Red);
                builder.WithTimestamp(DateTimeOffset.Now);
                await Message.Channel.SendMessageAsync("", embed: builder);

                //Console.WriteLine($"{DateTime.Now} at Commands] Something went wrong with executing a command. Text: {Context.Message.Content} | Error: {Result.ErrorReason}");
            }
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

        private static void LoadConfig()
        {
            Config config = new Config();
            token = config.Token;
        }
    }
}