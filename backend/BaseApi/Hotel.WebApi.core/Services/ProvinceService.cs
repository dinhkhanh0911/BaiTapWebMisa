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
    public class ProvinceService : BaseService<Province>, IProvinceService
    {
        private IProvinceRepository _provinceRepository;
        public ProvinceService(IProvinceRepository provinceRepository) : base(provinceRepository)
        {
            _provinceRepository= provinceRepository;
        }

        public IEnumerable<Province> GetByCountry(int id)
        {
            var res = _provinceRepository.GetByCountry(id);
            return res;
        }
    }
}
