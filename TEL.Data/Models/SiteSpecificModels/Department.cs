using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelGws.Data.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AbbreviatedName { get; set; }
        public int OrganizationId { get; set; }
        public string ApproverEmailId { get; set; }
        public string ApproverUserId { get; set; }
        public string SalesPersonEmail { get; set; }
        public string SalesPersonId { get; set; }
        public string ShipPersonEmail { get; set; }
        public string PurchasePersonEmail { get; set; }
        public string PurchasePersonId { get; set; }
        public string ShipToAddress { get; set; }
        public string ShipFromAddress { get; set; }
        public sbyte Status { get; set; }
        public int LastOrderCount { get; set; }
        public string SuccessorFlag { get; set; }
        public string CountChangeNote { get; set; }
        public int DeptMergeUnmergeId { get; set; }
        public string UpdateNote { get; set; }
        public int ShowHistory { get; set; }
        public string HistoryNote { get; set; }
        public int ImportLogId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

}