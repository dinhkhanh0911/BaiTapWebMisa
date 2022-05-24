using Base.WebApi.core.Interfaces;
using Base.WebApi.Core.Entities;
using Hotel.WebApi.core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web02.Core.Interfaces.Base;

namespace Base.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class WardsController : CustomBaseController<Ward>
    {
        private IWardService _wardService;
        public WardsController(IWardService wardService) : base(wardService)
        {
            _wardService = wardService;
        }

        [HttpGet("ByDistrict/{id}")]
        public IActionResult GetByDistrict(int id)
        {
            try
            {
                var res = _wardService.GetByDistrict(id);
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

        public override IActionResult Delete(int id)
        {
            return StatusCode(404);
        }

        public override IActionResult Filter(int currentPage, int pageSize, string? filterText)
        {
            return base.Filter(currentPage, pageSize, filterText);
        }

        public override IActionResult MultiDelete([FromBody] List<int> listId)
        {
            return StatusCode(404);
        }

        public override IActionResult Post([FromBody] Ward entity)
        {
            return StatusCode(404);
        }

        public override IActionResult Put([FromRoute] int id, [FromBody] Ward entity)
        {
            return StatusCode(404);
        }
    }
}
