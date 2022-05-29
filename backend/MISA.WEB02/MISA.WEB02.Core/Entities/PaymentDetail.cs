using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB02.Core.Entities
{
    public class PaymentDetail
    {
        /// <summary>
        /// Id payment detail
        /// </summary>
        public Guid PaymentDetailId { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string? DescriptionPayment { get; set; }

        /// <summary>
        /// Id payment master
        /// </summary>
        public Guid PaymentId { get; set; }

        /// <summary>
        /// Tài khoản nợ
        /// </summary>

        public int? DebitAccountId { get; set; }

        /// <summary>
        /// Tài khoản có
        /// </summary>
        public int? CreditAccountId { get; set; }

        /// <summary>
        /// Số tiền
        /// </summary>
        public double? CashAmount { get; set; }

        /// <summary>
        /// Tài khoản nhà cung cấp
        /// </summary>
        public Guid? VendorId { get; set; }

        [NotMap]
        public String? VendorCode { get; set; }
        [NotMap]
        public String? VendorName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
