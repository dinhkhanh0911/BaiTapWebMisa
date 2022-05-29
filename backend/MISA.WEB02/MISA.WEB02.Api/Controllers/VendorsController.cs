using Microsoft.AspNetCore.Mvc;
using MISA.WEB02.Api.Helpers;
using MISA.WEB02.Core.Entities;
using MISA.WEB02.Core.Exceptions;
using MISA.WEB02.Core.Interfaces;
using MISA.WEB02.Core.Resources;
using Newtonsoft.Json.Linq;

namespace MISA.WEB02.Api.Controllers
{
    public class VendorsController : MisaBaseController<Vendor>
    {
        #region Fields
        IVendorService _vendorService;
        #endregion

        #region Constructor
        public VendorsController(IVendorService vendorService) : base(vendorService)
        {
            this._vendorService = vendorService;
        }
        #endregion

        #region Methods
        [HttpGet]
        [Route("filterAdvanced")]
        public IActionResult FilterAdvanced([FromQuery] string? filterText = "",[FromQuery] int? vendorType = null,[FromQuery] bool? isOwed = null,
            [FromQuery] bool? isUsed=null,[FromQuery] int currentPage = 1,[FromQuery] int pageSize = 20)
        {
            try
            {

                var result = _vendorService.FilterService(filterText, vendorType,isOwed,isUsed,currentPage,pageSize);
                var jObj = JObject.Parse(result.ToString());
                jObj.Capitalize();
                return Ok(JObject.Parse(jObj.ToString()));
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


        [HttpGet]
        [Route("export")]
        public IActionResult Export()
        {
            try
            {

                var file = _vendorService.ExportService("Danh sách nhà cung cấp");


                return File(file, "xlsx/xls", "Danh_sach_nha_cung_cap.xlsx");
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
