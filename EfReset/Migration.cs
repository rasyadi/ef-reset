using System.IO;
using System.IO.Abstractions;

namespace EfReset
{
    public class Migration
    {
        private readonly IFileSystem _fileSystem;

        public Migration(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public Migration() : this(new FileSystem())
        {
        }

        public void Remove(string projectPath)
        {
            if (!_fileSystem.Directory.Exists(projectPath))
            {
                throw new DirectoryNotFoundException(nameof(projectPath));
            }
        }
    }
}
