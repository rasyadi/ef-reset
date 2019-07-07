namespace EfReset.Tests.TestingHelpers
{
    public class FakeDbContext : IDbContext
    {
        private string _info;

        public FakeDbContext(string info)
        {
            _info = info;
        }

        public string GetInfo()
        {
            return _info;
        }
    }
}
