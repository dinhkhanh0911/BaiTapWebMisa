using Base.WebApi.core.Interfaces;
using Base.WebApi.Core.Entities;
using Hotel.WebApi.core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Base.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DistrictsController : CustomBaseController<District>
    {
        private IDistrictService _districtService;
        public DistrictsController(IDistrictService districtService) : base(districtService)
        {
            _districtService = districtService;
        }

        public override IActionResult Delete(int id)
        {
            return StatusCode(404);
        }

        public override IActionResult Filter(int currentPage, int pageSize, string? filterText)
        {
            return StatusCode(404);
        }

        [HttpGet("ByProvince/{id}")]
        public IActionResult GetByProvince(int id)
        {
            try
            {
                var res = _districtService.GetByProvince(id);
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

        public override IActionResult Post([FromBody] District entity)
        {
            return StatusCode(404);
        }

        public override IActionResult Put([FromRoute] int id, [FromBody] District entity)
        {
            return StatusCode(404);
        }
    }
}
