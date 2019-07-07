using System.Collections.Generic;
using System.Diagnostics;

namespace EfReset
{
    public class DbContext : IDbContext
    {
        public string GetInfo()
        {
            using (Process dotnet = new Process())
            {
                dotnet.StartInfo.FileName = "dotnet.exe";
                dotnet.StartInfo.Arguments = "ef dbcontext info --project C:\\depot\\github\\ef-reset\\Sample.Ef";
                dotnet.StartInfo.UseShellExecute = false;
                dotnet.StartInfo.RedirectStandardOutput = true;
                dotnet.Start();

                return dotnet.StandardOutput.ReadToEnd();
            }
        }
    }
}
