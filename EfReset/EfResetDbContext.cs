using Microsoft.EntityFrameworkCore;

namespace EfReset
{
    public class EfResetDbContext: DbContext
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
