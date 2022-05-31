using MISA.WEB02.Core.Entities;
using MISA.WEB02.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB02.Core.Interfaces
{
    public interface IVendorService:IBaseService<Vendor>
    {
        /// <summary>
        /// Tìm kiếm nâng cao nhà cung cấp theo điều kiện
        /// </summary>
        /// <param name="filterText">Điều kiện tìm kiếm</param>
        /// <param name="vendorType">Loại nhà cung cấp</param>
        /// <param name="isOwed">Tình trạng ghi nợ</param>
        /// <param name="isUsed">Trạng thái sử dụng</param>
        /// <param name="currentPage">Trang hiện tại</param>
        /// <param name="pageSize">Số lượng bản ghi</param>
        /// <returns>
        /// Danh sách nhà cung cấp 
        /// Tổng số bản ghi
        /// Số lượng trang
        /// </returns>
        public Object FilterService(string filterText, int? vendorType, bool? isOwed,
            bool? isUsed, int currentPage, int pageSize);

        /// <summary>
        /// Xuất dữ liệu
        /// </summary>
        ///<param name="filterText">Điều kiện tìm kiếm</param>
        /// <param name="vendorType">Loại nhà cung cấp</param>
        /// <param name="isOwed">Tình trạng ghi nợ</param>
        /// <param name="isUsed">Trạng thái sử dụng</param>
        /// <param name="currentPage">Trang hiện tại</param>
        /// <param name="pageSize">Số lượng bản ghi</param>
        /// <returns>Dữ liệu dạng byte</returns>
        public byte[] ExportService(string filterText, int? vendorType, Guid? vendorGroupId, bool? isOwed,
            bool? isUsed, int currentPage, int pageSize);
    }
}
