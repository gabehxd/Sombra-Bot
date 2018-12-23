using Discord.Commands;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using System.Net.Http;
using Octokit;
using Sombra_Bot.Utils;

namespace Sombra_Bot.Commands
{
    public class Release : ModuleBase<SocketCommandContext>
    {
        public static DirectoryInfo roottemppath = new DirectoryInfo(Path.Combine(Path.GetTempPath(), "Sombra-Bot"));

        [Command("GetRelease"), Summary("Downloads the latest release of a Github repository.")]
        public async Task GetRelease(string user, string repo)
        {
            GitHubClient client = new GitHubClient(new ProductHeaderValue("Github"));
            IReadOnlyList<Octokit.Release> releases;
            try
            {
                releases = await client.Repository.Release.GetAll(user, repo);
            }
            catch
            {
                await Error.Send(Context.Channel, Value: "Invalid repository.");
                return;
            }

            if (releases.Count == 0)
            {
                await Error.Send(Context.Channel, Value: "No releases made.");
                return;
            }

            Octokit.Release latest = releases[0];
            if (latest.Assets.Count == 1)
            {
                await Context.Channel.TriggerTypingAsync();
                await Task.Delay(500);
                await Context.Channel.SendMessageAsync("Grabbing release...");
                await Context.Channel.TriggerTypingAsync();

                if (latest.Assets[0].Size > 8000000)
                {
                    await Context.Channel.SendMessageAsync($"Here ya go!: {latest.Assets[0].BrowserDownloadUrl}");
                    return;
                }

                FileInfo temp = new FileInfo(Path.Combine(Path.GetTempPath(), "Sombra-Bot", repo.ToLower(), latest.Assets[0].Name));
                DirectoryInfo temppath = new DirectoryInfo(Path.Combine(Path.GetTempPath(), "Sombra-Bot", repo.ToLower()));
                temppath.Create();

                if (temp.Exists)
                {
                    if (temp.Length == latest.Assets[0].Size)
                    {
                        await Context.Channel.SendFileAsync(temp.FullName, "Here ya go!:");
                        return;
                    }
                }

                temp.Delete();
                HttpClient dlclient = new HttpClient();
                using (Stream asset = await dlclient.GetStreamAsync(latest.Assets[0].BrowserDownloadUrl))
                using (FileStream dest = temp.Create())
                {
                    asset.CopyTo(dest);
                }
                await Context.Channel.SendFileAsync(temp.FullName, "Here ya go!:");
            }
            else
            {
                await Context.Channel.TriggerTypingAsync();
                await Task.Delay(500);
                await Context.Channel.SendMessageAsync("Grabbing releases...");
                HttpClient dlclient = new HttpClient();
                DirectoryInfo temppath = new DirectoryInfo(Path.Combine(Path.GetTempPath(), "Sombra-Bot", repo.ToLower()));
                temppath.Create();
                foreach (ReleaseAsset asset in latest.Assets)
                {
                    await Context.Channel.TriggerTypingAsync();
                    if (asset.Size <= 8000000)
                    {
                        FileInfo temp = new FileInfo(Path.Combine(Path.GetTempPath(), "Sombra-Bot", repo.ToLower(), asset.Name));
                        if (temp.Exists)
                        {
                            if (temp.Length == asset.Size)
                            {
                                await Context.Channel.SendFileAsync(temp.FullName);
                                continue;
                            }
                        }

                        using (Stream item = await dlclient.GetStreamAsync(asset.BrowserDownloadUrl))
                        using (FileStream dest = temp.Create())
                        {
                            item.CopyTo(dest);
                        }
                        await Context.Channel.SendFileAsync(temp.FullName);
                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync(asset.BrowserDownloadUrl);
                    }
                }
                await Context.Channel.TriggerTypingAsync();
                await Task.Delay(500);
                await Context.Channel.SendMessageAsync("Done!");
            }
        }
    }
}
