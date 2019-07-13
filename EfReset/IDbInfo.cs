namespace EfReset
{
    public interface IDbInfo
    {
        string ProviderName { get; set; }
        string DatabaseName { get; set; }
        string DataSource { get; set; }
        string Options { get; set; }
        DbInfo Parse(string output);
    }
}
