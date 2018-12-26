using Discord;
using Discord.Commands;
using System.Threading.Tasks;
using Discord.Audio;
using System.Diagnostics;
using System.IO;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Sombra_Bot.Commands
{

    public class Splatoon : ModuleBase<SocketCommandContext>
    {
        //requries the user to have /spl/ in the working directory
        //implement a stop command
        [Command("playsplat"), Summary("Plays music.")]
        [RequireOwner]
        public async Task PlayMusic(IVoiceChannel channel = null)
        {
            var voiceuser = Context.User as IVoiceState;
            channel = voiceuser.VoiceChannel ?? (Context.Message.Author as IGuildUser)?.VoiceChannel;
            if (Context.Channel == null) { await Context.Channel.SendMessageAsync("User must be in a voice channel, or a voice channel must be passed as an argument."); return; }

            IAudioClient audioclient = await channel.ConnectAsync();
            await SendAsync(audioclient as IAudioClient);
        }

        private Process CreateStream(string path)
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
        private async Task SendAsync(IAudioClient client)
        {
            DirectoryInfo spl = new DirectoryInfo("spl");
            List<FileInfo> files = spl.GetFiles().ToList();
            Random rng = new Random();
            files = files.OrderBy(x => rng.Next()).ToList();
            foreach (FileInfo path in files)
            {
                var ffmpeg = CreateStream(path.FullName);
                var output = ffmpeg.StandardOutput.BaseStream;
                var discord = client.CreatePCMStream(AudioApplication.Music);
                await output.CopyToAsync(discord);
                await discord.FlushAsync();
            }
        }
        
    }
}
