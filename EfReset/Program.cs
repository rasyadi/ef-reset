using McMaster.Extensions.CommandLineUtils;
using System;
using System.IO;

namespace EfReset
{
    class Program
    {
        [Option(Description = "Directory of the Entity Framework Core project")]
        public string ProjectPath { get; set; }

        public static void Main(String[] args)
            => CommandLineApplication.Execute<Program>(args);

        private void OnExecute()
        {
            var migrationFolder = Path.Join(ProjectPath, "Migrations");
            var migration = new Migration();
            migration.Remove(ProjectPath, migrationFolder);

            Console.WriteLine("The End");
        }
    }
}
