using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB02.Core.Entities
{
    public class Vendor
    {
        /// <summary>
        /// Id Bảng nhà cung cấp
        /// </summary>
        [MISARequired]
        public Guid VendorId { get; set; }

        /// <summary>
        /// Mã Nhà cung cấp
        /// </summary>
        [MISARequired]
        public string? VendorCode { get; set; }

        /// <summary>
        /// địa chỉ nhà cung cấp
        /// </summary>
        
        public string? Address { get; set; }

        /// <summary>
        /// Tên Nhà cung cấp
        /// </summary>
        [MISARequired]
        [MISADisplay(Name = "Tên nhà cung cấp")]
        public string? VendorName { get; set; }

        public string? VendorGroup { get; set; }
        /// <summary>
        /// Tên tiền tố(xưng hô)
        /// </summary>
        public int? PrefixNameId { get; set; }


        /// <summary>
        /// mã số thuế
        /// </summary>
        public string? TaxCode { get; set; }

        /// <summary>
        /// số điện thoại
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// số điện thoại bàn
        /// </summary>
        public string? LandLineNumber { get; set; }
        /// <summary>
        /// link trang web
        /// </summary>
        public string? Website { get; set; }

        /// <summary>
        /// Loại nhà cung cấp: 0 - tổ chức; 1 - cá nhân
        /// </summary>
        public int? VendorType { get; set; }

        /// <summary>
        /// là khách hàng hay không hay không
        /// </summary>
        public bool? IsCustomer { get; set; }
        public bool? IsOwed { get; set; }

        public bool? IsUsed { get; set; }

        /// <summary>
        /// tên người liên hệ
        /// </summary>
        
        public string? ContactName { get; set; }

        /// <summary>
        /// tên đại diện pháp luật
        /// </summary>
        public string? ContactLegalRep { get; set; }

        /// <summary>
        /// email liên hệ
        /// </summary>
        public string? ContactEmail { get; set; }

        /// <summary>
        /// số điện thoại liên hệ
        /// </summary>
        public string? ContactPhoneNumber { get; set; }

        #region Điều khoản

        public Guid? EmployeeId { get; set; }

        [NotMap]
        public string? EmployeeName { get; set; }
        /// <summary>
        /// điều khoản được định nghĩa trong enum
        /// </summary>
        public int? ContractId { get; set; }

        /// <summary>
        /// tên điều khoản
        /// </summary>
        [NotMap]
        public string? ContractName { get; set; }

        /// <summary>
        /// Số ngày nợ tối đa
        /// </summary>
        public int? MaxDebitDate { get; set; }

        /// <summary>
        /// Số nợ tối đa
        /// </summary>
        public double? MaxDebitAmount { get; set; }

        /// <summary>
        /// Tài khoản công nợ nhận
        /// </summary>
        public int? DebitReceiptAccountId { get; set; }

        /// <summary>
        /// Tài khoản công nợ trả
        /// </summary>
        public int? DebitPaymentAccountId { get; set; }

        #endregion

        #region Tài khoản ngân hàng
        public string? Bank { get; set; }
        #endregion

        #region Địa chỉ khác

        /// <summary>
        /// tên quốc gia
        /// </summary>
        public int? CountryId { get; set; }

        /// <summary>
        /// tên tỉnh
        /// </summary>
        public int? ProvinceId { get; set; }

        /// <summary>
        /// tên huyện
        /// </summary>
        public int? DistrictId { get; set; }

        /// <summary>
        /// tên xã
        /// </summary>
        public int? WardId { get; set; }

        /// <summary>
        /// danh sách các địac hỉ giao hàng là chuỗi json
        /// </summary>
        public string? DeliveryAddress { get; set; }
        #endregion

        #region Ghi chú

        /// <summary>
        /// ghi chú
        /// </summary>
        public string? TextNote { get; set; }
        #endregion

        #region Cá nhân (thêm)
        /// <summary>
        /// ngày cấp chứng minh nhân dân
        /// </summary>
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// số cmnd
        /// </summary>
        public string? IdentityNumber { get; set; }
        /// <summary>
        /// nơi cấp chứng minh nhân dân
        /// </summary>
        public string? IdentityPlace { get; set; }

        

        #endregion
    }
}
