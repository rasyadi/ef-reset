using System;
using System.Linq;

namespace EfReset
{
    public class DbInfo
    {
        public string ProviderName { get; set; }
        public string DatabaseName { get; set; }
        public string DataSource { get; set; }
        public string Options { get; set; }

        public DbInfo Parse(string output)
        {
            if (string.IsNullOrEmpty(output))
            {
                throw new ArgumentNullException(nameof(output));
            }

            if (!output.Contains(":"))
            {
                throw new ArgumentException("Invalid argument");
            }

            var outputLines = output.Split("\r\n").Where(text => !string.IsNullOrEmpty(text));

            foreach (var line in outputLines)
            {
                if (line.StartsWith("Provider name"))
                {
                    ProviderName = line.Split(":")[1].Trim();
                }
                else if (line.StartsWith("Database name"))
                {
                    DatabaseName = line.Split(":")[1].Trim();
                }
                else if (line.StartsWith("Data source"))
                {
                    DataSource = line.Split(":")[1].Trim();
                }
                else if (line.StartsWith("options"))
                {
                    Options = line.Split(":")[1].Trim();
                }
            }

            return this;
        }
    }
}
