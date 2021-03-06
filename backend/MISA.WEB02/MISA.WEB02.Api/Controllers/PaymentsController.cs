using Microsoft.AspNetCore.Mvc;
using MISA.WEB02.Api.Helpers;
using MISA.WEB02.Core.Entities;
using MISA.WEB02.Core.Exceptions;
using MISA.WEB02.Core.Interfaces;
using MISA.WEB02.Core.Resources;
using Newtonsoft.Json.Linq;

namespace MISA.WEB02.Api.Controllers
{
    public class PaymentsController : MisaBaseController<Payment>
    {
        #region Fields
        IPaymentService _paymentService;
        #endregion

        #region Constructor
        public PaymentsController(IPaymentService paymentService) : base(paymentService)
        {
            this._paymentService = paymentService;
        }
        #endregion
        #region Methods

        [HttpGet]
        [Route("filterAdvanced")]
        public IActionResult FilterAdvanced([FromQuery] string? filterText = "", [FromQuery] int? paymentType = null,
            [FromQuery] bool? isRecord = null, [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null, [FromQuery] int currentPage = 1, [FromQuery] int pageSize = 20)
        {
            try
            {

                var result = _paymentService.FilterService(filterText, paymentType, isRecord, startDate, endDate, currentPage, pageSize);
                var jObj = JObject.Parse(result.ToString());
                jObj.Capitalize();
                dynamic resultObject = JObject.Parse(jObj.ToString());
                var Count = resultObject.Count.Value;
                return Ok(resultObject);
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
        [HttpGet("PaymentDetails/getByPaymentId/{paymentId}")]
        public IActionResult GetPaymentDetailByPaymentIdService(Guid paymentId)
        {
            try
            {

                var result = _paymentService.GetPaymentDetailByPaymentIdService(paymentId);
                
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
        [HttpPost]
        [Route("PaymentDetails")]
        public IActionResult InsertPaymentDetail([FromBody] PaymentDetail paymentDetail)
        {
            try
            {

                var result = _paymentService.InsertPaymentDetailService(paymentDetail);

                return StatusCode(201, result);
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
        public IActionResult Export([FromQuery] string? filterText = "", [FromQuery] int? paymentType = null,
            [FromQuery] bool? isRecord = null, [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null, [FromQuery] int currentPage = 1, [FromQuery] int pageSize = 20)
        {
            try
            {

                var file = _paymentService.ExportService(filterText, paymentType, isRecord, startDate, endDate, currentPage, pageSize);


                return File(file, "xlsx/xls", "Danh_sach_phieu_chi.xlsx");
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
