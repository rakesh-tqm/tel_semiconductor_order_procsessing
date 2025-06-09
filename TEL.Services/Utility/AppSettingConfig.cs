using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEL.Services.Utility
{
    public static class AppSettingConfig
    {
        private static IConfiguration _configuration;

        public static void SetConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static string GetValue(string key)
        {
            return _configuration[key];
        }

        public static T GetSection<T>(string key) where T : class
        {
            return _configuration.GetSection(key).Get<T>();
        }

        public static string GetConnectionString(string name)
        {
            return _configuration.GetConnectionString(name);
        }

        public static string GetNestedValue(string sectionName, string key)
        {
            return _configuration.GetSection(sectionName)[key];
        }
    }
}
