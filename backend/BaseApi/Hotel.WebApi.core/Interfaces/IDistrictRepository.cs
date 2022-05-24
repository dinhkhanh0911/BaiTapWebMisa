using Base.WebApi.Core.Entities;
using MISA.Web02.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.WebApi.core.Interfaces
{
    public interface IDistrictRepository:IBaseRepository<District>
    {
        public IEnumerable<District> GetByProvince(int id);
    }
}
