using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB02.Core.Entities
{
    public class VendorGroup
    {
        public Guid VendorGroupId
        {
            get; set;
        }
        public String VendorGroupName { get; set; }
        public String VendorGroupCode { get; set; }
        public String? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public String ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        
    }
}
