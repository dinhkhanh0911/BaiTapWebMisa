using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB02.Core.Entities
{
    [AttributeUsage(AttributeTargets.Property)]
    public class NotMap : Attribute
    {

    }
    [AttributeUsage(AttributeTargets.Property)]
    public class MISADisplay : Attribute
    {
        public string? Name { get; set; }
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class MISARequired : Attribute
    {
    }
}
