using Microsoft.EntityFrameworkCore;
using System;

namespace EfReset
{
    class Program
    {
        static void Main(string[] args)
        {          
            var migration = new Migration();
            migration.Remove(@"C:\\depot\\github\\ef-reset\\Sample.Ef", @"C:\\depot\\github\\ef-reset\\Sample.Ef\Migrations");                       
            
            Console.WriteLine("The End");
        }

    }
}
