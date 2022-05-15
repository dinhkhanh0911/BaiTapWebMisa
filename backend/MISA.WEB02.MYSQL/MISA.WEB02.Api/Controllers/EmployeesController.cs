using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB02.Core.Entities;
using MISA.WEB02.Core.Exceptions;
using MISA.WEB02.Core.Interfaces;
using MISA.WEB02.Core.Resources;
using MISA.WEB02.Core.Services;

namespace MISA.WEB02.Api.Controllers
{

    /// <summary>
    /// Controller Employee
    /// </summary>
    /// created by Đinh Văn Khánh
    /// Created date (9/4/2022)
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : MisaBaseController<Employee>
    {
        #region Fields
        IEmployeeService _empoyeeService;
        #endregion

        #region Constructor

        public EmployeesController(IEmployeeService employeeService):base(employeeService)
        {
            _empoyeeService = employeeService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy dữ liệu theo điều kiện
        /// </summary>
        /// <param name="q">Điều kiện tìm kiếm</param>
        /// <param name="offset">Trang dữ liệu hiện tại</param>
        /// <param name="limit">Số lượng bản ghi tối đa</param>
        /// <returns>Object{Danh sách nhân viên,trang hiện tại,Tổng số lượng bản ghi}</returns>
        [HttpGet]
        [Route("filter")]
        public IActionResult Filter([FromQuery] string? filterText = "",[FromQuery] int currentPage = 1,[FromQuery] int pageSize = 20)
        {
            try
            {

                var result = _empoyeeService.FilterService(filterText, currentPage, pageSize);
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

        /// <summary>
        /// Xuất dữ liệu Excel
        /// </summary>
        /// <param name="filterText">Điều kiện truyền vào</param>
        /// <param name="currentPage">Trang hiện tại</param>
        /// <param name="pageSize">Số lượng bản ghi</param>
        /// <returns>File excel</returns>
        [HttpGet]
        [Route("export")]
        public IActionResult Export()
        {
            try
            {

                var file= _empoyeeService.ExportService();
                return File(file, "xlsx/xls", "Danh_sach_nhan_vien.xlsx");
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

        /// <summary>
        /// Lấy mã nhân viên mới từ database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("NewEmployeeCode")]
        public IActionResult getNewEmployeeCode()
        {
            try
            {

                var result = _empoyeeService.GetNewCodeService();
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

        [HttpGet]
        [Route("Test")]
        public IActionResult Test()
        {
            try
            {
                var result = this._empoyeeService.test();
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("updatetest/{id}")]
        public virtual IActionResult Put([FromRoute] Guid id, [FromBody] Department department)
        {
            try
            {
                var result = this._empoyeeService.UpdateTest(id, department);
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
