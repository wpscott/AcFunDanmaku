using System.Threading.Tasks;

namespace AcFunDanmuSongRequest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await DGJ.Initialize();
            await DGJ.AddSong("是心动啊");
            var song = await DGJ.NextSong();
        }
    }
}
