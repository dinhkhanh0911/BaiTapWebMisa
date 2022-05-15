using MISA.WEB02.Core.Entities;
using MISA.WEB02.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB02.Core.Services
{
    public class DepartmentService:BaseService<Department>,IDepartmentService
    {
        #region Fields
        IDepartmentRepository _departmentRepository;
        #endregion


        #region Constructor
        public DepartmentService(IDepartmentRepository departmentRepository):base(departmentRepository)
        {
            this._departmentRepository = departmentRepository;
        }
        #endregion
    }
}
