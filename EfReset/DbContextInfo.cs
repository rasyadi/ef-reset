using System.Diagnostics;
using System.IO;
using System.IO.Abstractions;

namespace EfReset
{
    public class DbContextInfo : IDbContextInfo
    {
        private readonly IFileSystem _fileSystem;

        public DbContextInfo(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public DbContextInfo(): this(new FileSystem())
        {
        }

        public string GetInfo(string projectPath)
        {            
            if (!_fileSystem.Directory.Exists(projectPath))
            {
                throw new DirectoryNotFoundException(nameof(projectPath));
            }

            using (Process dotnet = new Process())
            {
                dotnet.StartInfo.FileName = "dotnet.exe";
                dotnet.StartInfo.Arguments = $"ef dbcontext info --project {projectPath}";
                dotnet.StartInfo.UseShellExecute = false;
                dotnet.StartInfo.RedirectStandardOutput = true;
                dotnet.Start();

                return dotnet.StandardOutput.ReadToEnd();
            }
        }
    }
}
