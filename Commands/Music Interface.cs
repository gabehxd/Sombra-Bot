using Discord;
using Discord.Commands;
using System.Threading.Tasks;
using Discord.Audio;
using System.Diagnostics;

namespace Sombra_Bot.Commands
{
    public class Splatoon : ModuleBase<SocketCommandContext>
    {
        //requries the user to have /spl/ in the working directory
        //implement a stop command

        [Command("PlayMusic", RunMode = RunMode.Async), Summary("Plays music.")]
        [RequireOwner]
        public async Task PlayMusic(IVoiceChannel channel = null)
        {
            var voiceuser = Context.User as IVoiceState;
            channel = voiceuser.VoiceChannel ?? (Context.Message.Author as IGuildUser)?.VoiceChannel;
            if (Context.Channel == null) { await Context.Channel.SendMessageAsync("User must be in a voice channel, or a voice channel must be passed as an argument."); return; }

            //await Music_Manager.SendAsync(client as IAudioClient);
        }

        [Command("Stopmusic", RunMode = RunMode.Async), Summary("Stop music.")]
        [RequireOwner]
        public async Task StopMusic(IVoiceChannel channel = null)
        {
            var client = await channel.ConnectAsync();
            await client.StopAsync();
        }
    }
}
