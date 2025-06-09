using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelGws.Data.Database
{
    public static class SiteHelper
    {
        private static string _connectionString;

        static SiteHelper()
        {

            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            _connectionString = configuration.GetValue<string>("ConnectionStrings:SQLConnection");
        }
        public static TelDbContext CreateDbContext()
        {

            var optionsBuilder = new DbContextOptionsBuilder<TelDbContext>();
            optionsBuilder.UseSqlServer(_connectionString);

            return new TelDbContext(optionsBuilder.Options);
        }
    }
}
