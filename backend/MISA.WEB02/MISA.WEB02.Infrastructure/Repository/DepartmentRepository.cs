using Dapper;
using MISA.WEB02.Core.Entities;
using MISA.WEB02.Core.Interfaces;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB02.Infrastructure.Repository
{
    /// <summary>
    /// Repository lấy dữ liệu của đơn vị
    /// </summary>
    /// CreatedBy: Đinh Văn Khánh (15/4/2022)
    public class DepartmentRepository : BaseRespository<Department>, IDepartmentRepository
    {

    }
}
