using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEL.Services.Utility
{
    public enum ErrorMessageHelper
    {

        [Description("Ok")]
        Ok = 100,
        [Description("Created successfully.")]
        Created = 101,
        [Description("Updated successfully")]
        Updated = 102,
        [Description("Deleted successfully")]
        Deleted = 103,
        [Description("Already exist")]
        Duplicate = 104,
        [Description("No data found")]
        NoDataFound = 105



    }
}
