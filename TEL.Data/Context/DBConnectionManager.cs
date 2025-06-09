namespace TelGws.Data.Context
{
    public static class DbConnectionManager
    {
        private static string _connectionString;

        static DbConnectionManager()
        {
            // Get App Settings
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            // Get SQL Connection String from AppSetting
            _connectionString = configuration.GetValue<string>("ConnectionStrings:SQLConnection");
        }

        /// <summary>
        /// Create SQL Connection
        /// </summary>
        /// <returns></returns>
        public static SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
