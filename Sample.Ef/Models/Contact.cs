namespace Sample.Ef.Models
{
    public class Contact 
    {
        public int Id { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}