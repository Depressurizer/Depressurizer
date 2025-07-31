using System.Diagnostics;

namespace Depressurizer.Core.Helpers
{
    public static class Utils
    {
        public static Process? RunProcess(string filename, string args = "")
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(filename, args)
            {
                UseShellExecute = true
            };
            return Process.Start(startInfo);
        }
    }
}
