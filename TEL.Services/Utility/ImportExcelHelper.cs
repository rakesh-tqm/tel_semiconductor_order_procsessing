using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEL.Services.Utility
{
    public class ImportExcelHelper
    {
        public static DataTable ImportExcel(string filePath,string sheatName)
        {
            DataTable dt=new DataTable();
            string connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0; Data Source={filePath}; Extended Properties=Excel 8.0;";
            using (OleDbConnection Connection = new OleDbConnection(connectionString))
            {
                Connection.Open();
                using (OleDbCommand command = new OleDbCommand())
                {
                    command.Connection = Connection;
                    command.CommandText = $"SELECT * FROM {sheatName}";
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                    {
                        adapter.SelectCommand = command;
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }
    }
}
