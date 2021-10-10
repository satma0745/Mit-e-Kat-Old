namespace Mitekat.Persistence.Configuration
{
    internal class DatabaseConfiguration
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public string Database { get; set; }
        public string User { get; set; }
        public string Password { get; set; }

        public string ConnectionString =>
            $"Server={Server};Port={Port};Database={Database};User Id={User};Password={Password}";
    }
}
