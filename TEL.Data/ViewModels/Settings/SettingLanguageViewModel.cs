using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelGws.Data.ViewModels.Settings
{
    public class VariableViewModel
    {
        public List<string> Variables{ get; set; }
    }
    public class SettingLanguageViewModel
    {
        public int Id { get; set; }
        public string Variable { get; set; }
        public int? Status { get; set; }
        public string Eng { get; set; }
        public string Jap { get; set; }
        public string EngHelp { get; set; }
        public string JapHelp { get; set; }        
    }
}
