using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EfReset
{
    public class Table
    {
        public static void Drop(string connectionString)
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
    }
}
