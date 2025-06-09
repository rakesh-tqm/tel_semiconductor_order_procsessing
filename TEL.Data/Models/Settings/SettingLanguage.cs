using Microsoft.Data.SqlClient;

namespace TelGws.Data.Models
{
    public class SettingLanguage
    {
        public int Id { get; set; }
        public string Variable { get; set; }
        public int? Status { get; set; }
        public string Eng { get; set; }
        public string Jap { get; set; }
        public string EngHelp { get; set; }
        public string JapHelp { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ImportLogId { get; set; }

        public SettingLanguage()
        { }
        public SettingLanguage(SqlDataReader dr)
        {
            Id = int.TryParse(Convert.ToString(dr["Id"]), out int outId) ? outId : default;
            Variable = Convert.ToString(dr["Variable"]);
            Status = int.TryParse(Convert.ToString(dr["Status"]), out int outStatus) ? outStatus : default;
            Eng = Convert.ToString(dr["Eng"]);
            Jap = Convert.ToString(dr["Jap"]);
            EngHelp = Convert.ToString(dr["EngHelp"]);
            JapHelp = Convert.ToString(dr["JapHelp"]);
            CreatedBy = int.TryParse(Convert.ToString(dr["CreatedBy"]), out int outCreatedBy) ? outCreatedBy : default;
            ModifiedBy = int.TryParse(Convert.ToString(dr["ModifiedBy"]), out int outModifiedBy) ? outModifiedBy : default;
            CreatedOn = DateTime.TryParse(Convert.ToString(dr["CreatedOn"]), out DateTime outCreatedOn) ? outCreatedOn : default;
            ModifiedOn = DateTime.TryParse(Convert.ToString(dr["ModifiedOn"]), out DateTime outModifiedOn) ? outModifiedOn : default;
            ImportLogId = int.TryParse(Convert.ToString(dr["ImportLogId"]), out int outImportLogId) ? outImportLogId : default;
        }
    }


}