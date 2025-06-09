using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEL.Services.Utility
{
    public static class AppSettingsHepler
    {
        private static string _baseRootPath;

        public static void SetBaseRootPath(string baseRootPath)
        {
            _baseRootPath = baseRootPath;
        }

        public static string GetFilePath(string relativePath)
        {
            if (string.IsNullOrEmpty(_baseRootPath))
            {
                throw new InvalidOperationException("Base root path is not set.");
            }            
            return Path.Combine(_baseRootPath, relativePath);
        }
    }
}
