using System;

namespace EfReset
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbContext = new DbContext();
            Console.WriteLine(dbContext.GetInfo());
        }
    }
}
