using MISA.WEB02.Core.Entities;
using MISA.WEB02.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB02.Core.Interfaces
{
    public interface IVendorGroupRepository : IBaseRepository<VendorGroup>
    {
        public IEnumerable<VendorGroup> search(string searchValue);
    }
}
