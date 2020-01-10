using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TDG.Behaviors
{
    public class DataGridColumnsMap
    {
        public Type Type { get; set; }
        public List<DataGridColumn> Columns { get;  set; } 
            = new List<DataGridColumn>();
    }
}
