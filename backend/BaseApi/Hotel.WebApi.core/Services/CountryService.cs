using Base.WebApi.core.Interfaces;
using Base.WebApi.Core.Entities;
using MISA.Web02.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.WebApi.core.Services
{
    public class CountryService : BaseService<Country>, ICountryService
    {
        public CountryService(ICountryRepository countryRepository) : base(countryRepository)
        {
        }
    }
}
