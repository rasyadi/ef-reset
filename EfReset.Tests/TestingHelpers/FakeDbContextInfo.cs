namespace EfReset.Tests.TestingHelpers
{
    public class FakeDbContextInfo : IDbContextInfo
    {
        private string _info;

        public FakeDbContextInfo(string info)
        {
            _info = info;
        }

        public string GetInfo(string projectPath)
        {
            return _info;
        }
    }
}
