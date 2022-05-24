using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB02.Core.Entities
{
    public class Payment
    {
        /// <summary>
        /// id payment
        /// </summary>
        public Guid? PaymentId { get; set; }
        /// <summary>
        /// code payment
        /// </summary>
        public string? PaymentCode { get; set; }
        /// <summary>
        /// khóa ngoại bảng AccountObject
        /// </summary>
        public Guid? VendorId { get; set; }
        /// <summary>
        /// tên của AccountObject
        /// </summary>
        [NotMap]
        public string? VendorName { get; set; }
        /// <summary>
        /// tên Người nhận
        /// </summary>
        public string? ReceiverName { get; set; }
        /// <summary>
        /// địa chỉ
        /// </summary>
        public string? Address { get; set; }
        /// <summary>
        /// ngày hạch toán
        /// </summary>
        public DateTime? AccountingDate { get; set; }
        /// <summary>
        /// Ngày Thanh toán
        /// </summary>
        public DateTime? PaymentDate { get; set; }
        /// <summary>
        /// diễn giải cho phiếu chi(lí do chi)
        /// </summary>
        public string? DescriptionPayment { get; set; }
        /// <summary>
        /// Khóa ngoại bảng nhân viên
        /// </summary>
        public Guid? EmployeeId { get; set; }
        /// <summary>
        /// tệp đính kèm chứng từ gốc kèm theo
        /// </summary>
        public int? AttachDocumentAmount { get; set; }
        /// <summary>
        /// loại Tiền tệ
        /// </summary>
        public int? CurrencyId { get; set; }
        

        [NotMap]
        public IEnumerable<PaymentDetail>? PaymentDetails { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        
        public bool? IsRecord { get; set; }
        public int? PaymentType { get; set; }
    }
}
