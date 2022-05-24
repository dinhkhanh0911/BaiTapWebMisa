using Base.WebApi.core.Interfaces;
using Base.WebApi.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web02.Core.Interfaces.Base;

namespace Base.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CountriesController : CustomBaseController<Country>
    {
        public CountriesController(ICountryService countryService) : base(countryService)
        {

        }

        public override IActionResult Delete(int id)
        {
            return StatusCode(404);
        }

        public override IActionResult Filter(int currentPage, int pageSize, string? filterText)
        {
            return StatusCode(404);
        }

        public override IActionResult GetById(int id)
        {
            return StatusCode(404);
        }

        public override IActionResult MultiDelete([FromBody] List<int> listId)
        {
            return StatusCode(404);
        }

        public override IActionResult Post([FromBody] Country entity)
        {
            return StatusCode(404);
        }

        public override IActionResult Put([FromRoute] int id, [FromBody] Country entity)
        {
            return StatusCode(404);
        }
    }
}
