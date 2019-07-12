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
            var dbContext = new DbContextInfo();
            var dbInfo = new DbInfo();

            if (!_fileSystem.Directory.Exists(projectPath))
            {
                throw new DirectoryNotFoundException(nameof(projectPath));
            }

            dbInfo = dbInfo.Parse(dbContext.GetInfo(projectPath));                       
            
            Table.Drop($"Data Source={Path.Combine(projectPath, dbInfo.DataSource)}");            

            _fileSystem.Directory.Delete(Path.Combine(projectPath, "Migrations"), true);
        }              
    }
}
