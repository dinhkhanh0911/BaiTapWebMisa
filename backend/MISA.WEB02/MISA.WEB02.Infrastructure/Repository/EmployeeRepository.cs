using Dapper;
using MISA.WEB02.Core.Entities;
using MISA.WEB02.Core.Interfaces;
using MySqlConnector;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace MISA.WEB02.Infrastructure.Repository
{
    /// <summary>
    /// Thao tác dữ liệu nhân viên
    /// </summary>
    /// Created by: Đinh Văn Khánh (13/04/2022)
    public class EmployeeRepository : BaseRespository<Employee>, IEmployeeRepository
    {

        #region Methods
        /// <summary>
        /// Lấy dữ liệu nhân viên theo điêu kiện
        /// </summary>
        /// <param name="filterText">Dữ liệu tìm kiếm</param>
        /// <param name="currentPage">Trang hiện tại</param>
        /// <param name="pageSize">Số lượng dữ liệu một trang</param>
        /// <returns>
        /// {
        ///     Danh sách nhân viên,
        ///     Tổng số bản ghi,
        ///     Số lượng trang
        /// }
        /// </returns>

        public override IEnumerable<Employee> Get()
        {

            //Khởi tạo kết nối
            var sqlConnection = new MySqlConnection(_sqlConnectionString);

            var sqlCommand = $"SELECT Employee.*,d.DepartmentCode,d.DepartmentName FROM Employee " +
                "INNER JOIN Department d ON Employee.DepartmentId = d.DepartmentId " +
                "ORDER BY EmployeeCode"; 

            var data = sqlConnection.Query<Employee>(sqlCommand);
            return data;
        }
        public Object Filter(string filterText, int currentPage, int pageSize)
        {
            int offset = (currentPage - 1) * pageSize;
            int limit = pageSize;

            //Khởi tạo kết nối
            var sqlConnection = new MySqlConnection(_sqlConnectionString);

            var sqlCommand = $"SELECT Employee.*,d.DepartmentCode,d.DepartmentName FROM Employee " +
                "INNER JOIN Department d ON Employee.DepartmentId = d.DepartmentId " +
                "WHERE EmployeeCode LIKE '%' @q '%' OR FullName LIKE '%' @q '%' " +
                "ORDER BY Employee.EmployeeCode DESC LIMIT @offset,@limit;" +
                "SELECT COUNT(*) FROM Employee WHERE EmployeeCode LIKE '%' @q '%' OR FullName LIKE '%' @q '%' ";
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@q", filterText);
            dynamicParameters.Add("@offset", offset);
            dynamicParameters.Add("@limit", limit);


            var data = sqlConnection.QueryMultiple(sqlCommand,param:dynamicParameters);
            var employees = data.Read<Employee>();
            var count = data.Read<int>().Single();
            var totalPage = count / limit;
            totalPage =  (count % limit == 0) ? totalPage : totalPage+1;
            return new { employees, count,totalPage };
        }
        public Object test()
        {
            var connString = "Server=127.0.0.1;Port=5432;Database=misa_web02_dvkhanh;User Id=postgres;Password=09112000;";
            var sqlConnection = new NpgsqlConnection(connString);
            sqlConnection.Open();
            string commandText = $"select * from department d ";
            DataTable table = new DataTable();
            
            using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, sqlConnection))
            {
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    Department department = new Department();
                    int i = 0;
                    dynamic obj = new ExpandoObject();
                    
                    foreach (PropertyInfo prop in (new Department()).GetType().GetProperties())
                    {

                        
                            var type = prop.PropertyType;
                            var value = reader[ToSnakeCase(prop.Name)];
                            if(type.Name == "Guid")
                            {
                                value = new Guid(value.ToString());
                            }
                            var a = reader[ToSnakeCase(prop.Name)];
                            prop.GetType();
                            prop.SetValue(department, value, null);
                            //department.GetType().GetProperty(prop.Name) = reader[ToSnakeCase(prop.Name)];
                        
                        i++;

                    }
                    return department;
                }
                   
                
            }
            return null;
        }

        public int UpdateTest(Guid departmentId, Department department)
        {
            var connString = "Server=127.0.0.1;Port=5432;Database=misa_web02_dvkhanh;User Id=postgres;Password=09112000;";
            var sqlConnection = new NpgsqlConnection(connString);
            sqlConnection.Open();
            string commandText = $"select * from department d ";
            using (NpgsqlCommand cmd = new NpgsqlCommand("update_department", sqlConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("@department_id",department.DepartmentId.ToString()));
                cmd.Parameters.Add(new NpgsqlParameter("@department_code",department.DepartmentCode));
                cmd.Parameters.Add(new NpgsqlParameter("@department_name",department.DepartmentName));
                cmd.Parameters.Add(new NpgsqlParameter("@created_date",department.CreatedDate));
                cmd.Parameters.Add(new NpgsqlParameter("@created_by", department.CreatedBy));
                cmd.Parameters.Add(new NpgsqlParameter("@modified_date", department.ModifiedDate.ToString()));
                cmd.Parameters.Add(new NpgsqlParameter("@modified_by", department.ModifiedBy));
                //cmd.Prepare();
                var retuntcnt = cmd.ExecuteScalar();
                return 1;
            }
            return 0;
        }
        public string ToSnakeCase(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }
            if (text.Length < 2)
            {
                return text;
            }
            var sb = new StringBuilder();
            sb.Append(char.ToLowerInvariant(text[0]));
            for (int i = 1; i < text.Length; ++i)
            {
                char c = text[i];
                if (char.IsUpper(c))
                {
                    sb.Append('_');
                    sb.Append(char.ToLowerInvariant(c));
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
        #endregion
    }
}
