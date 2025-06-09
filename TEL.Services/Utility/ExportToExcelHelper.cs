using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.IO;
using System.Data.OleDb;
using System.ComponentModel;

namespace TEL.Services.Utility
{

    public static class Extensions
    {
        public static void ExportToExcel(this DataTable dataTable, String filePath, bool overwiteFile = true)
        {
            if (File.Exists(filePath) && overwiteFile)
            {
                File.Delete(filePath);
            }
            var conn = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={filePath};Extended Properties='Excel 8.0;HDR=Yes;IMEX=0';";
            using (OleDbConnection connection = new OleDbConnection(conn))
            {

                connection.Open();
                using (OleDbCommand command = new OleDbCommand())
                {
                    command.Connection = connection;
                    List<String> columnNames = new List<string>();
                    foreach (DataColumn dataColumn in dataTable.Columns)
                    {
                        columnNames.Add(dataColumn.ColumnName);
                    }
                    String tableName = !String.IsNullOrWhiteSpace(dataTable.TableName) ? dataTable.TableName : Guid.NewGuid().ToString();
                    command.CommandText = $"CREATE TABLE [{tableName}] ({String.Join(",", columnNames.Select(c => $"[{c}] VARCHAR").ToArray())});";
                    command.ExecuteNonQuery();


                    foreach (DataRow row in dataTable.Rows)
                    {
                        List<String> rowValues = new List<string>();
                        foreach (DataColumn column in dataTable.Columns)
                        {
                            rowValues.Add((row[column] != null && row[column] != DBNull.Value) ? row[column].ToString().Replace("'", "''") : String.Empty);
                        }
                        command.CommandText = $"INSERT INTO [{tableName}]({String.Join(",", columnNames.Select(c => $"[{c}]"))}) VALUES ({String.Join(",", rowValues.Select(r => $"'{r}'").ToArray())});";
                        command.ExecuteNonQuery();
                    }
                }
                connection.Close();
            }
        }

      
        
        public static List<T> ConvertToList<T>(this DataTable dataTable) where T : new()
        {
            var list = new List<T>();

            foreach (DataRow row in dataTable.Rows)
            {
                var item = new T();
                foreach (DataColumn col in dataTable.Columns)
                {
                    var propertyInfo = typeof(T).GetProperty(col.ColumnName);
                    if (propertyInfo != null && row[col] != DBNull.Value)
                    {
                        propertyInfo.SetValue(item, row[col]);
                    }
                }
                list.Add(item);
            }

            return list;
        }


        public static DataTable ConvertToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }
    }
    
}


