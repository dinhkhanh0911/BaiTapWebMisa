using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB02.Core.Entities;
using MISA.WEB02.Core.Interfaces;

namespace MISA.WEB02.Api.Controllers
{
    /// <summary>
    /// Controller Department
    /// </summary>
    /// CreatedBy: Đinh Văn Khánh (15/4/2022)
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : MisaBaseController<Department>
    {
        #region Fields
        IDepartmentService _departmentService;
        #endregion

        #region Constructor
        public DepartmentsController(IDepartmentService departmentService):base(departmentService)
        {
            this._departmentService = departmentService;
        }
        #endregion
    }
}
