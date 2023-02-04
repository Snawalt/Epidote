using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Epidote.Game
{
    public class LunarFilesCheck
    {
        private static readonly string GameReadyDirectory = Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE"), ".lunarclient", "offline");
        private static readonly string NativesDirectory = Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE"), ".lunarclient", "offline", "multiver", "natives");
        private static readonly string LocalProgramData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Programs\lunarclient";



        private static readonly long MinimumSize = 90000 * 1024; // 90MB in bytes
        public static string errorMessage;

        public static async Task<bool> IsReadyAsync()
        {
            if (!Directory.Exists(GameReadyDirectory))
            {
                errorMessage = "The GameReadyDirectory does not exist.";
                return false;
            }

            if (!Directory.Exists(NativesDirectory))
            {
                errorMessage = "The NativesDirectory does not exist.";
                return false;
            }

            int dllCount = Directory.EnumerateFiles(NativesDirectory, "*.dll", SearchOption.AllDirectories).Count();
            if (dllCount < 15)
            {
                errorMessage = "The number of .dll files in the NativesDirectory is less than 15.";
                return false;
            }

            var offlineSize = await DirSizeAsync(new DirectoryInfo(GameReadyDirectory));
            if (offlineSize < MinimumSize)
            {
                errorMessage = "The size of the GameReadyDirectory is less than 90MB.";
                return false;
            }

            if (!Directory.Exists(LocalProgramData))
            {
                errorMessage = "The LocalProgramData directory does not exist.";
                return false;
            }

            return true;
        }

        private static async Task<long> DirSizeAsync(DirectoryInfo d)
        {
            var size = 0L;
            var fileTasks = d.EnumerateFiles().Select(f => f.Length);
            var dirTasks = d.EnumerateDirectories().Select(DirSizeAsync);

            await Task.WhenAll(dirTasks);
            return size + fileTasks.Sum() + dirTasks.Sum(t => t.Result);
        }

        public static string GetErrorMessage()
        {
            return errorMessage;
        }
    }
}
