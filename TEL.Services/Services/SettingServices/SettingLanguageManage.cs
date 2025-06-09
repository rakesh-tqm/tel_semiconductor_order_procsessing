using System.Data;
using System.Transactions;
using TEL.Services.Utility;

namespace TEL.Services
{

    public class SettingLanguageManage
    {
        /// <summary>
        /// Get Setting Language By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static (SettingLanguage, string) Get(int id)
        {

            using (var connection = DbConnectionManager.CreateConnection())
            {

                SqlCommand cmd = new SqlCommand("SettingLanguageGet", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Id", id));
                connection.Open();
                var dr = cmd.ExecuteReader();

                SettingLanguage obj = null;
                if (dr.Read())
                    obj = new SettingLanguage(dr);

                connection.Close();
                cmd.Dispose();

                return (obj, obj == null ? "No record found" : string.Empty);

            }
        }

        /// <summary>
        /// Get All Recipes
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static (List<SettingLanguage>, string) Get(string[] variables, int id = 0)
        {
            if (variables != null)
            {
                string allVariableNames = string.Join(",", variables);

                using (var connection = DbConnectionManager.CreateConnection())
                {

                    SqlCommand cmd = new SqlCommand("SettingLanguageGetByVariable", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@VariableNames", allVariableNames));
                    connection.Open();
                    var dr = cmd.ExecuteReader();

                    List<SettingLanguage> settingLanguageList = new List<SettingLanguage>();
                    while (dr.Read())
                        settingLanguageList.Add(new SettingLanguage(dr));
                    connection.Close();
                    cmd.Dispose();

                    connection.Close();
                    cmd.Dispose();

                    return (settingLanguageList, settingLanguageList == null ? "No record found" : string.Empty);
                }

            }
            return (null, string.Empty);
        }

        /// <summary>
        /// Download Recipe File
        /// </summary>
        /// <returns></returns>
        public static (string filePath, string fileName) Download()
        {
            using (var connection = DbConnectionManager.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("SettingLanguageGetAll", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                connection.Open();
                var dr = cmd.ExecuteReader();
                List<SettingLanguage> settingLanguageList = new List<SettingLanguage>();
                var dt = new DataTable();
                dt.Load(dr);
                cmd.Dispose();

                var folderPath = AppSettingsHepler.GetFilePath(AppSettingConfig.GetValue("FileExportPath"));

                var fileName = "LanguageSettings";
                var uniqueFileName = $"{fileName.GetUniqueFileName()}.xls";

                dt.ExportToExcel($"{Path.Combine(folderPath, uniqueFileName)}");

                return (folderPath, uniqueFileName);
            }
        }

        /// <summary>
        /// Import Recipe file
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static (bool result, string error) ImportFile(string fileName)
        {
            var folderPath = AppSettingsHepler.GetFilePath(AppSettingConfig.GetValue("FileImportPath"));

            var filePath = Path.Combine(folderPath, $"{fileName}");

            if (File.Exists(Path.Combine(folderPath, $"{fileName}")))
            {
                var dt = ImportExcelHelper.ImportExcel(filePath, "SettingLanguage");
                if (dt.Rows.Count > 0)
                {
                    using (TransactionScope trans = new TransactionScope())
                    {
                        using (var connection = DbConnectionManager.CreateConnection())
                        {


                            SqlCommand cmd = new SqlCommand("SettingLanguageSet", connection);
                            cmd.CommandType = CommandType.StoredProcedure;
                            connection.Open();
                            foreach (DataRow row in dt.Rows)
                            {
                                if (!string.IsNullOrEmpty(Convert.ToString(row["Variable"])))
                                {
                                    cmd.Parameters.Clear();
                                    cmd.Parameters.Add(new SqlParameter("@Id", row["Id"]));
                                    cmd.Parameters.Add(new SqlParameter("@Variable", row["Variable"]));
                                    cmd.Parameters.Add(new SqlParameter("@Status", row["Status"]));
                                    cmd.Parameters.Add(new SqlParameter("@Eng", row["Eng"]));
                                    cmd.Parameters.Add(new SqlParameter("@Jap", row["Jap"]));
                                    cmd.Parameters.Add(new SqlParameter("@EngHelp", row["EngHelp"]));
                                    cmd.Parameters.Add(new SqlParameter("@JapHelp", row["JapHelp"]));
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            cmd.Dispose();
                            connection.Close();
                        }
                        trans.Complete();
                    }
                    return (true, string.Empty);
                }
            }
            return (false, "File not found.");
        }

        /// <summary>
        /// Get All Setting Language
        /// </summary>
        /// <returns></returns>
        public static List<SettingLanguage> GetAll()
        {
            using (var connection = DbConnectionManager.CreateConnection())
            {

                SqlCommand cmd = new SqlCommand("SettingLanguageGetAll", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                connection.Open();
                var dr = cmd.ExecuteReader();

                List<SettingLanguage> settingLanguageList = new List<SettingLanguage>();
                while (dr.Read())
                    settingLanguageList.Add(new SettingLanguage(dr));
                connection.Close();
                cmd.Dispose();

                return settingLanguageList;

            }
        }

        /// <summary>
        /// Create or Edit Setting Language
        /// </summary>
        /// <param name="_SettingLanguage"></param>
        /// <returns></returns>
        public static (bool, string) Set(SettingLanguage _SettingLanguage)
        {
            using (TransactionScope trans = new TransactionScope())
            {
                using (var connection = DbConnectionManager.CreateConnection())
                {

                    SqlCommand cmd = new SqlCommand("SettingLanguageSet", connection);
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.Add(new SqlParameter("@Id", _SettingLanguage.Id));
                    cmd.Parameters.Add(new SqlParameter("@Variable", _SettingLanguage.Variable));
                    cmd.Parameters.Add(new SqlParameter("@Status", _SettingLanguage.Status));
                    cmd.Parameters.Add(new SqlParameter("@Eng", _SettingLanguage.Eng));
                    cmd.Parameters.Add(new SqlParameter("@Jap", _SettingLanguage.Jap));
                    cmd.Parameters.Add(new SqlParameter("@EngHelp", _SettingLanguage.EngHelp));
                    cmd.Parameters.Add(new SqlParameter("@JapHelp", _SettingLanguage.JapHelp));
                    cmd.Parameters.Add(new SqlParameter("@CreatedBy", 1));
                    cmd.Parameters.Add(new SqlParameter("@CreatedOn", DateTime.UtcNow));
                    cmd.Parameters.Add(new SqlParameter("@ModifiedBy", 1));
                    cmd.Parameters.Add(new SqlParameter("@ModifiedOn", DateTime.UtcNow));
                    cmd.Parameters.Add(new SqlParameter("@ErrorMessage", ParameterDirection.Output));
                    connection.Open();

                    cmd.ExecuteNonQuery();

                    cmd.Dispose();
                    connection.Close();
                    trans.Complete();

                    var ErrorMessage = cmd.Parameters["@ErrorMessage"];
                    var ErrorMessageValue = Convert.ToString(ErrorMessage.Value);

                    if (!string.IsNullOrEmpty(ErrorMessageValue))
                        return (false, ErrorMessageValue);
                    else
                        return (true, ErrorMessageValue);
                }
            }
        }


        /// <summary>
        /// Delete Setting Language
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static (bool, string) Delete(int id)
        {
            using (TransactionScope trans = new TransactionScope())
            {
                using (var connection = DbConnectionManager.CreateConnection())
                {

                    SqlCommand cmd = new SqlCommand("SettingLanguageDelete", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    cmd.Parameters.Add(new SqlParameter("@Id", id));
                    cmd.Parameters.Add(new SqlParameter("@ErrorMessage", ParameterDirection.Output));
                    int iResult = cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    connection.Close();
                    trans.Complete();

                    var ErrorMessage = cmd.Parameters["@ErrorMessage"];
                    var ErrorMessageValue = Convert.ToString(ErrorMessage.Value);

                    if (iResult > 0)
                        return (true, ErrorMessageValue);
                    else
                        return (false, ErrorMessageValue);

                }
            }
        }
    }
}