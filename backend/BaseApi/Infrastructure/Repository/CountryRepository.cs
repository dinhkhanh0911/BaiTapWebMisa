using Base.WebApi.core.Interfaces;
using Base.WebApi.Core.Entities;
using MISA.Infrastructure.Respository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Respository
{
    public class CountryRepository:BaseRepository<Country>,ICountryRepository
    {
    }
}
