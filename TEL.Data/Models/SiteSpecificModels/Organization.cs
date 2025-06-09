using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelGws.Data.Models
{
    public class Organization
    {
        public int Id { get; set; }
        public int SiteId { get; set; }
        public string Name { get; set; }
        public string AbbreviatedName { get; set; }
        public string PhysicalStreetAddress { get; set; }
        public string PhysicalPostalCode { get; set; }
        public string PhysicalState { get; set; }
        public string PhysicalCity { get; set; }
        public string PhysicalTown { get; set; }
        public string ShippingStreetAddress { get; set; }
        public string ShippingPostalCode { get; set; }
        public string ShippingState { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingTown { get; set; }
        public string TelephoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public sbyte ProjectCodeRequired { get; set; }
        public sbyte Status { get; set; }
        public int ImportLogId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

}