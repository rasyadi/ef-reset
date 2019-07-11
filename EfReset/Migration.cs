using System.IO;
using System.IO.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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
            
            DropTables($"Data Source={Path.Combine(projectPath, dbInfo.DataSource)}");
            Directory.Delete(Path.Combine(projectPath, "Migrations"), true);
        }

        private void DropTables(string connectionString)
        {           
            using (var context = new EfResetDbContext(connectionString))
            {
                var tables = new List<string>();
                using (var connection = context.Database.GetDbConnection())
                {
                    connection.Open();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT tbl_name FROM sqlite_master where type = 'table'";
                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            tables.Add(reader["tbl_name"].ToString());
                        }                       
                    }

                    using (var command = connection.CreateCommand())
                    {
                        foreach (var table in tables.Where(t => !t.Equals("sqlite_sequence")))
                        {
                            command.CommandText = $"DROP TABLE {table}";
                            command.ExecuteNonQuery();
                        }
                    }

                    connection.Close();
                }
            }
        }

        public class EfResetDbContext : DbContext
        {
            private readonly string _connectionString;

            public EfResetDbContext(string connectionString)
            {
                _connectionString = connectionString;
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {                
                optionsBuilder.UseSqlite(_connectionString);
            }
        }
    }
}
