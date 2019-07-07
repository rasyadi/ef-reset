using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;

namespace TestRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Process dotnet = new Process())
            {
                var providerName = "";
                var databaseName = "";
                var dataSource = "";
                var options = "";
                dotnet.StartInfo.FileName = "dotnet.exe";
                dotnet.StartInfo.Arguments = "ef dbcontext info --project C:\\depot\\github\\ef-reset\\Sample.Ef";
                dotnet.StartInfo.UseShellExecute = false;
                dotnet.StartInfo.RedirectStandardOutput = true;
                dotnet.Start();

                var dbInfoPairs = new Dictionary<string, string>();
                string textLine = "";
                while ((textLine = dotnet.StandardOutput.ReadLine()) != null) {
                    if (textLine.StartsWith("Provider name"))
                    {
                        providerName = textLine.Split(":")[1];
                    }
                    else if (textLine.StartsWith("Database name"))
                    {
                        databaseName = textLine.Split(":")[1];
                    }
                    else if (textLine.StartsWith("Data source"))
                    {
                        dataSource = textLine.Split(":")[1];
                    }
                    else if (textLine.StartsWith("Options"))
                    {
                        options = textLine.Split(":")[1];
                    }

                }

                Console.WriteLine(providerName);
                Console.WriteLine(databaseName);
                Console.WriteLine(dataSource);
                Console.WriteLine(options);

                dotnet.WaitForExit();
            }
        }
    }
}
