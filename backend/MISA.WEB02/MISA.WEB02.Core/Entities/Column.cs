using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB02.Core.Entities
{
    public class Column
    {
        public int ColumnId { get; set; }
        public string? ColumnField { get; set; }
        public string? ColumnName { get; set; }
        public string? ViewColumnName { get; set; }
        public string? DescriptionColumnName { get; set; }
        public string? ColumnClass { get; set; }
        public string? ColumnType { get; set; }
        public string? ColumnWidth { get; set; }
        public string? IsShow { get; set; }
        public string? IsFix { get; set; }
    }
}
