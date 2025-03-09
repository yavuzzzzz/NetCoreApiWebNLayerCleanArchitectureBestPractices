namespace App.Repositories
{
    public class ConnectionStringOption // This class is used to map the ConnectionStrings section from appsettings.json
    { 
        public const string Key = "ConnectionStrings";
        public string SqlServer { get; set; } = default!;
    }
}
