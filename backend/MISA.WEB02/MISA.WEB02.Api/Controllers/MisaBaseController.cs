using Microsoft.AspNetCore.Mvc;
using MISA.WEB02.Core.Entities;
using MISA.WEB02.Core.Exceptions;
using MISA.WEB02.Core.Interfaces.Base;
using MISA.WEB02.Core.Resources;
namespace MISA.WEB02.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public abstract class MisaBaseController<T> : ControllerBase
    {
        #region Fields
        private IBaseService<T> _baseService;

        #endregion
        #region Constructor
        public MisaBaseController(IBaseService<T> baseService)
        {
            this._baseService = baseService;
        }
        #endregion
        /// <summary>
        /// Lấy tất cả bản ghi từ database
        /// </summary>
        /// <returns>
        /// trả về danh sách bản ghi lấy được
        /// trả về NoContent nếu k có dữ  liệu nào nhận được
        /// </returns>
        /// Author: Đinh Văn Khánh - MF1112 (13/04/2022)
        [HttpGet]
        public virtual IActionResult Get()
        {
            try
            {
                var result = _baseService.GetService();
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
        /// Lấy bản ghi bằng id của bản ghi
        /// </summary>
        /// <param name="id">id bản ghi được truyền qua router</param>
        /// <returns>
        /// bản ghi tìm được trong database
        /// NoContent nếu k có bản ghi nào
        /// </returns>
        /// Author: Đinh Văn Khánh - MF1112 (13/04/2022)
        [HttpGet("{id}")]
        public virtual IActionResult GetById(Guid id)
        {
            try
            {
                var result = _baseService.GetByIdService(id);
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
        /// Thêm bản ghi
        /// </summary>
        /// <param name="employee">Đối tượng bản ghi được truyền từ form</param>
        /// <returns>
        /// trả về 201 nếu thêm thành công và k có lỗi
        /// trả về badrequest nếu các trường validate không hợp lệ
        /// </returns>
        /// Author: Đinh Văn Khánh - MF1112 (13/04/2022)
        [HttpPost]
        public virtual IActionResult Post([FromBody] T entity)
        {
            try
            {
                var result = _baseService.InsertService(entity);
                return Created("", result);
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
        /// Thay đổi thông tin bản ghi
        /// </summary>
        /// <param name="id">mã id của bản ghi</param>
        /// <param name="employee">đối tượng bản ghi được truyền qua body</param>
        /// <returns>
        /// trả về Ok nếu sửa thành công
        /// trả về badrequest nếu người dùng thay đổi mã bản ghi hoặc không tìm thấy bản ghi để thay đổi
        /// </returns>
        /// Author: Đinh Văn Khánh - MF1112 (13/04/2022)
        [HttpPut("{id}")]
        public virtual IActionResult Put([FromRoute] Guid id, [FromBody] T entity)
        {
            try
            {
                var result = _baseService.UpdateService(entity, id);
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
        /// xóa bản ghi
        /// </summary>
        /// <param name="id">mã id của bản ghi</param>
        /// <returns>
        /// xóa thành công: trả về code 200 và số dòng dc xóa
        /// xóa không thành công trả về lỗi badrequest (thường là do k tìm thấy bản ghi hoặc mã id gửi lên không đúng đinh dạng)
        /// </returns>
        /// Author: Đinh Văn Khánh - MF1112 (13/04/2022)
        [HttpDelete("{id}")]
        public virtual IActionResult Delete(Guid id)
        {
            try
            {
                var result = _baseService.DeleteService(id);
                return Ok(result);

            }
            catch (MISAExceptions ex1)
            {
                var result = new MISAServiceResult()
                {
                    devMsg = ex1.Message,
                    userMsg = Resource.VN_MisaExceptionMsg,
                    data = ex1.Data,
                };
                return StatusCode(500, result);
            }
            catch (Exception ex)
            {
                var result = new MISAServiceResult()
                {
                    devMsg = ex.Message,
                    userMsg = Resource.VN_MisaExceptionMsg,
                    data = ex.Data,
                };
                return StatusCode(500, result);
            }
        }
        /// <summary>
        /// xóa bản ghi
        /// </summary>
        /// <param name="listId">mã id của bản ghi</param>
        /// <returns>
        /// xóa thành công: trả về code 200 và số dòng dc xóa
        /// xóa không thành công trả về lỗi badrequest (thường là do k tìm thấy bản ghi hoặc mã id gửi lên không đúng đinh dạng)
        /// </returns>
        /// Author: Đinh Văn Khánh - MF1112 (13/04/2022)
        [HttpDelete("multiple")]
        public virtual IActionResult MultiDelete([FromBody] List<Guid> listId)
        {
            try
            {
                var result = _baseService.MultiDelete(listId);
                return Ok(result);

            }
            catch (MISAExceptions ex1)
            {
                var result = new MISAServiceResult()
                {
                    devMsg = ex1.Message,
                    userMsg = Resource.VN_MisaExceptionMsg,
                    data = ex1.Data,
                };
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                var result = new MISAServiceResult()
                {
                    devMsg = ex.Message,
                    userMsg = Resource.VN_MisaExceptionMsg,
                    data = ex.Data,
                };
                return StatusCode(500, result);
            }
        }

        [HttpGet]
        [Route("NewCode")]
        public virtual IActionResult GetNewCode()
        {
            try
            {
                var result = _baseService.GetNewCodeService();
                return Ok(result);

            }
            catch (MISAExceptions ex1)
            {
                var result = new MISAServiceResult()
                {
                    devMsg = ex1.Message,
                    userMsg = Resource.VN_MisaExceptionMsg,
                    data = ex1.Data,
                };
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                var result = new MISAServiceResult()
                {
                    devMsg = ex.Message,
                    userMsg = Resource.VN_MisaExceptionMsg,
                    data = ex.Data,
                };
                return StatusCode(500, result);
            }
        }
    }
}
