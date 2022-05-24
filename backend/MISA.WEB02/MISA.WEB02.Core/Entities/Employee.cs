using MISA.WEB02.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB02.Core.Entities
{
    public class Employee
    {
        public Guid EmployeeId { get; set; }
        /// <summary>
        /// Mã nhân viên: bắt buộc
        /// </summary>
        public string EmployeeCode { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        /// <summary>
        /// Tên nhân viên: bắt buộc
        /// </summary>
        public string EmployeeName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// Giới tính: (1-nam, 0-nữ, 2-khác)
        /// </summary>
        public int? Gender { get; set; }
        public string? GenderName
        {
            get
            {
                switch (Gender)
                {
                    case (int)GenderEnum.Male:
                        return "Nam";
                    case (int)GenderEnum.Female:
                        return "Nữ";
                    default:
                        return "Khác";
                }
            }
        }
        public string? PhoneNumber { get; set; }
        public string? LandlineNumber { get; set; }
        public string? Email { get; set; }
        public string? ContactAddress { get; set; }

        /// <summary>
        /// ID của phòng ban: bắt buộc
        /// </summary>
        public Guid DepartmentId { get; set; }
        public String? DepartmentCode { set; get; }
        public String? DepartmentName { get; set; }
        public String? PositionName { get; set; }
        public string? BankAccount { get; set; }
        public string? BankName { get; set; }
        public string? BankBranch { get; set; }
        public string? Salary { get; set; }
        public DateTime? JoinDate { get; set; }
        public string? IdentityNumber { get; set; }
        public int? WorkStatus { get; set; }

        public string? WorkStatusName
        {
            get
            {
                switch (WorkStatus)
                {
                    case (int) WorkStatusEnum.Working:
                        return "Đang làm việc";
                    case (int)WorkStatusEnum.Stop:
                        return "Đã nghỉ việc";
                    case (int)WorkStatusEnum.Holiday:
                        return "Đang nghỉ phép";
                    default:
                        return "Khác";
                }
            }
        }

        public DateTime? IdentityDate { get; set; }
        public string? IdentityPlace { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }

    }
}
