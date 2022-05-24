using MISA.WEB02.Core.Entities;
using MISA.WEB02.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB02.Core.Interfaces
{
    public interface IPaymentRepository:IBaseRepository<Payment>
    {
        /// <summary>
        /// Tìm kiếm nâng cao nhà cung cấp theo điều kiện
        /// </summary>
        /// <param name="filterText">Điều kiện tìm kiếm</param>
        /// <param name="paymentType">Loại phiếu</param>
        /// <param name="isRecord">Trạng thái ghi sổ</param>
        /// <param name="startDate,">Ngày bắt đầu</param>
        /// <param name="filterText">Ngày kết thúc</param>
        /// <param name="currentPage">Trang hiện tại</param>
        /// <param name="pageSize">Số lượng bản ghi</param>
        /// <returns>
        /// Danh sách nhà cung cấp 
        /// Tổng số bản ghi
        /// Số lượng trang
        /// </returns>
        public Object Filter(string filterText, int? paymentType, bool? isRecord,
           DateTime? startDate, DateTime? endDate, int currentPage, int pageSize);

        /// <summary>
        /// Lấy thu chi chi tiết
        /// </summary>
        /// <param name="paymentId">Id payment master</param>
        /// <returns>
        /// Thu chi chi tiết
        /// </returns>
        public IEnumerable<PaymentDetail> GetPaymentDetailByPaymentId(Guid paymentId);

        /// <summary>
        /// thêm mới phiếu chi chi tiết
        /// </summary>
        /// <param name="paymentDetail"> Phiếu chi chi tiết </param>
        /// <returns></returns>
        /// Author: Đinh Văn Khánh (22/04/2022)
        public int InsertPaymentDetail(PaymentDetail paymentDetail);
    }
}
