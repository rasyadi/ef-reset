using System.IO;
using System.IO.Abstractions;

namespace EfReset
{
    public class Migration
    {
        private readonly IFileSystem _fileSystem;
        private readonly IDbContextInfo _dbContextInfo;
        private readonly ITable _table;
        private IDbInfo _dbInfo;

        public Migration(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
            _dbContextInfo = new DbContextInfo();
            _dbInfo = new DbInfo();
            _table = new Table();
        }

        public Migration() : this(new FileSystem())
        {
        }

        public void Remove(string projectPath, string migrationsPath)
        {            
            if (!_fileSystem.Directory.Exists(projectPath))
            {
                throw new DirectoryNotFoundException(nameof(projectPath));
            }

            _dbInfo = _dbInfo.Parse(_dbContextInfo.GetInfo(projectPath));

            _table.Drop($"Data Source={Path.Combine(projectPath, _dbInfo.DataSource)}");

            if (!_fileSystem.Directory.Exists(migrationsPath))
            {
                throw new DirectoryNotFoundException(nameof(migrationsPath));
            }

            _fileSystem.Directory.Delete(migrationsPath, true);
        }              
    }
}
