using MISA.WEB02.Core.Entities;
using MISA.WEB02.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB02.Core.Services
{
    public class VendorGroupService : BaseService<VendorGroup>, IVendorGroupService
    {
        #region Fields
        IVendorGroupRepository _venderGroupRepository;
        #endregion

        #region Constructor
        public VendorGroupService(IVendorGroupRepository venderGroupRepository) : base(venderGroupRepository)
        {
            _venderGroupRepository = venderGroupRepository;
        }

        public IEnumerable<VendorGroup> searchService(string searchValue)
        {
            return _venderGroupRepository.search(searchValue);
        }
        #endregion
    }
}
