namespace MISA.WEB02.API.Test.Entities
{
    public class Employee
    {
        public Guid EmployeeID { get; set; }

        public String  EmployeeCode { get; set; }
        public string EmployeeName { get; set; }

        public String? FullName { get; set; }
        public DateTime? DateOBirth { get; set; }

        public int? Gender { get; set; }

        public String? IdentityNumber { get; set; }

    }
}
