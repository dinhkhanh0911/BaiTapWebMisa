using Base.WebApi.core.Interfaces;
using Base.WebApi.Core.Entities;
using Hotel.WebApi.core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Base.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProvinciesController : CustomBaseController<Province>
    {
        private IProvinceService _provinceService;
        public ProvinciesController(IProvinceService provinceService) : base(provinceService)
        {
            _provinceService = provinceService;
        }

        public override IActionResult Delete(int id)
        {
            return StatusCode(404);
        }

        public override IActionResult Filter(int currentPage, int pageSize, string? filterText)
        {
            return StatusCode(404);
        }

        [HttpGet("ByCountry/{id}")]
        public IActionResult GetByCountry(int id)
        {
            try
            {
                var res = _provinceService.GetByCountry(id);
                return Ok(res);
            }
            catch (CustomException ex)
            {
                var result = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ admin",
                    data = ex.Data,
                };
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                var result = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ admin",
                    data = ex.Data,
                };
                return StatusCode(500, result);
            }
        }

        public override IActionResult MultiDelete([FromBody] List<int> listId)
        {
            return StatusCode(404);
        }

        public override IActionResult Post([FromBody] Province entity)
        {
            return StatusCode(404);
        }

        public override IActionResult Put([FromRoute] int id, [FromBody] Province entity)
        {
            return StatusCode(404);
        }
    }
}
