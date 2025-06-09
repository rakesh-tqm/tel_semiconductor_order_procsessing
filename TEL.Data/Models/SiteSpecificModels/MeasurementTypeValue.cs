using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelGws.Data.Models
{
    public class MeasurementTypeValue
    {
        public int Id { get; set; }
        public int MeasurementTypeId { get; set; }
        public string MeasurementUnit { get; set; }
        public sbyte Status { get; set; }
        public string Comments { get; set; }
        public int ImportLogId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }



}