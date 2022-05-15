using MISA.WEB02.Core.Entities;
using MISA.WEB02.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB02.Core.Interfaces
{
    /// <summary>
    /// Cũng cấp các interface xử lý các nghiệp vụ cho các dich vụ
    /// </summary>
    public interface IEmployeeService : IBaseService<Employee>
    {
        /// <summary>
        /// Tìm kiếm nhân viên theo điều kiện
        /// </summary>
        /// <param name="filterText">Điều kiện tìm kiếm</param>
        /// <param name="currentPage">Trang hiện tại</param>
        /// <param name="pageSize">Số lượng bản ghi</param>
        /// <returns>
        /// Danh sách nhân viên 
        /// Tổng số bản ghi
        /// Số lượng trang
        /// </returns>
        public Object FilterService(string filterText, int currentPage, int pageSize);

        /// <summary>
        /// Xuất file nhân viên theo điều kiện
        /// </summary>
        /// <returns>
        /// File nhân viên
        /// </returns>

        public byte[] ExportService();
        public Object test();

        public int UpdateTest(Guid departmentId, Department department);
    }
}
