using Discord;
using Discord.Audio;
using Discord.Commands;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sombra_Bot.Commands
{
    public class Music_Manager
    {
        private readonly ConcurrentDictionary<ulong, IAudioClient> ConnectedChannels = new ConcurrentDictionary<ulong, IAudioClient>();
        
        public async Task JoinAudio(IGuild guild, IVoiceChannel target, SocketCommandContext context)
        {
            IAudioClient client;
            if (ConnectedChannels.TryGetValue(guild.Id, out client))
            {
                await context.Channel.SendMessageAsync("I'm already in a channel in this server");
                return;
            }

            if (target.GuildId != guild.Id)
            {
                return;
            }


        }

        private static Process CreateStream(string path)
        {
            var ffmpeg = new ProcessStartInfo
            {
                FileName = "ffmpeg.exe",
                Arguments = $"-i \"{path}\" -ac 2 -f s16le -ar 48000 pipe:1",
                UseShellExecute = false,
                RedirectStandardOutput = true,
            };
            return Process.Start(ffmpeg);
        }
    }
}
