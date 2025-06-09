using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelGws.Data.Models
{

    public class ProcessFlow
    {
        public int Id { get; set; }
        public int SiteId { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public string Image { get; set; }
        public string Info { get; set; }
        public sbyte Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

}