using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelGws.Data.Models
{

    public class ProcessFlowDetail
    {
        public int Id { get; set; }
        public int ProcessFlowId { get; set; }
        public int recipeId { get; set; }
        public int toolId { get; set; }
        public decimal toolCost { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public sbyte Status { get; set; }
    }

}