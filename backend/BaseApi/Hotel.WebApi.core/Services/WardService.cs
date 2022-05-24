using Base.WebApi.core.Interfaces;
using Base.WebApi.Core.Entities;
using MISA.Web02.Core.Interfaces.Base;
using MISA.Web02.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.WebApi.core.Services
{
    public class WardService : BaseService<Ward>, IWardService
    {
        private IWardRepository _wardService;
        public WardService(IWardRepository wardService) : base(wardService)
        {
            _wardService= wardService;
        }

        public IEnumerable<Ward> GetByDistrict(int id)
        {
            var res = _wardService.GetByDistrict(id);
            return res;
        }
    }
}
