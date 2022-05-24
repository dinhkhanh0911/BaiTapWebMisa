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
    public class DistrictService : BaseService<District>, IDistrictService
    {
        private IDistrictRepository _districtRepository;
        public DistrictService(IDistrictRepository districtRepository) : base(districtRepository)
        {
            _districtRepository = districtRepository;
        }

        public IEnumerable<District> GetByProvince(int id)
        {
            var res = _districtRepository.GetByProvince(id);
            return res;
        }
    }
}
