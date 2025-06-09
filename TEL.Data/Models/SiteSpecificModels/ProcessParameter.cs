using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelGws.Data.Models
{
    public class ProcessParameter
    {
        public int Id { get; set; }
        public int ProcessAreaId { get; set; }
        public int MeasurementTypeId { get; set; }
        public string ParameterName { get; set; }
        public bool IncludeInRecipe { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public string Comments { get; set; }
        public int ImportLogId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

}