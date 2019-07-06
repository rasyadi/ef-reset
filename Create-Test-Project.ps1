dotnet new console -o Sample.Ef | Out-Null

Set-Location .\Sample.Ef

dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design

$personClass = @"
namespace Sample.Ef.Models
{
    public class Person 
    {
        public int Id { get; set; }
    }
}
"@


New-Item -Path . -Name "Models" -ItemType "directory"
New-Item -Path  "Models" -ItemType "file" -Name "Person.cs" -Value $personClass

$dbContexClass = @"
using Microsoft.EntityFrameworkCore;

namespace Sample.Ef.Models
{
    public class PeopleContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=people.db");
        }
    }
}
"@

New-Item -Path  "Models" -ItemType "file" -Name "PeopleContext.cs" -Value $dbContexClass

dotnet ef migrations add InitialCreate | Out-Null
dotnet ef database update | Out-Null

$contactClass = @"
namespace Sample.Ef.Models
{
    public class Contact 
    {
        public int Id { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}
"@

New-Item -Path  "Models" -ItemType "file" -Name "Contact.cs" -Value $contactClass

$dbContexClass = @"
using Microsoft.EntityFrameworkCore;

namespace Sample.Ef.Models
{
    public class PeopleContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=people.db");
        }
    }
}
"@

New-Item -Force -Path  "Models" -ItemType "file" -Name "PeopleContext.cs" -Value $dbContexClass

dotnet ef migrations add AddContactEntity | Out-Null
dotnet ef database update | Out-Null