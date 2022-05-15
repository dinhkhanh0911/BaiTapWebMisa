using Microsoft.AspNetCore.Mvc;
using MISA.WEB02.Core.Entities;
using MISA.WEB02.Core.Exceptions;
using MISA.WEB02.Core.Interfaces;
using MISA.WEB02.Core.Resources;

namespace MISA.WEB02.Api.Controllers
{
    public class VendorGroupsController : MisaBaseController<VendorGroup>
    {
        #region Fields
        IVendorGroupService _venderGroupService;
        #endregion

        #region Constructor
        public VendorGroupsController(IVendorGroupService venderGroupService) : base(venderGroupService)
        {
            this._venderGroupService = venderGroupService;
        }
        #endregion

        #region Methods

        [HttpGet]
        [Route("search")]
        public IActionResult Search([FromQuery] string? searchValue)
        {
            try
            {

                var result = _venderGroupService.searchService(searchValue);
                return Ok(result);
            }
            catch (MISAExceptions ex)
            {
                var result = new MISAServiceResult()
                {
                    devMsg = ex.Message,
                    userMsg = Resource.VN_MisaExceptionMsg,
                    data = ex.Data,
                };
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                var result = new
                {
                    devMsg = ex.Message,
                    userMsg = Resource.VN_MisaExceptionMsg,
                    data = ex.Data,
                };
                return StatusCode(500, result);
            }
        }
        #endregion
    }
}
