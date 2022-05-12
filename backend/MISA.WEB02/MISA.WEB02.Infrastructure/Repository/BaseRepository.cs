using Dapper;
using MISA.WEB02.Core.Entities;
using MISA.WEB02.Core.Interfaces.Base;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB02.Infrastructure.Repository
{
    /// <summary>
    /// Base thao tác với dữ liệu
    /// </summary>
    /// Created by: Đinh Văn Khánh (13/04/2022)
    public class BaseRespository<T> : IBaseRepository<T>
    {
        #region Fields
        public string _sqlConnectionString;
        #endregion
        #region Constructor
        public BaseRespository()
        {
            //_sqlConnectionString = "Host=3.0.89.182;Port=3306;Database=MISA.WEB02.DVKHANH;User Id=dev;Password=12345678";
            _sqlConnectionString = "Host=Localhost;Port=3306;Database=MISA.WEB02.DVKHANH;User Id=root;Password=09112000";
        }
        #endregion
        #region Methods
        /// <summary>
        /// Lấy toàn bộ dữ liệu trong bảng
        /// </summary>
        /// <returns>
        /// Danh sách dữ liệu
        /// </returns>
        /// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        public virtual IEnumerable<T> Get()
        {
            var entityName = typeof(T).Name;
            //khởi tạo kết nối
            var sqlConnection = new MySqlConnection(_sqlConnectionString);
            //lấy dữ liệu
            string sqlCommand = $"SELECT * FROM {entityName}";
            //dữ liệu trả về gồm các propperty của Employee
            //và thêm các property: DeparmentId,DeparmentCode,PositionCode,PositionId
            var data = sqlConnection.Query<T>(sql: sqlCommand);
            //trả về kết quả
            return data;
        }
        /// <summary>
        /// Lấy mã đối tượng mới nhất trong hệ thống
        /// </summary>
        /// <returns>Mã đối tượng mới nhất trong hệ thống</returns>
        /// Author: Đinh Văn Khánh - MF1112 (13/04/2022)
        public virtual string GetNewCode()
        {
            //khởi tạo kết nối
            var sqlConnection = new MySqlConnection(_sqlConnectionString);
            //lấy entityName
            var entityName = typeof(T).Name;
            //sql query sắp xếp theo mã code
            var sqlCommand = $"SELECT e.{entityName}Code FROM {entityName} as e ORDER BY e.{entityName}Code";
            //Lấy danh sách code của entity:string
            var listEntityCode = sqlConnection.Query<string>(sql: sqlCommand);
            //danh sách code khi tách 2 chữ đầu
            var value = 0;
            //nếu kết quả trả về có phần tử thì lấy mã của phần tử cuối cùng cộng thêm một
            if(listEntityCode.Count() > 0)
            {
                var code = listEntityCode.ElementAt(listEntityCode.Count() - 1);
                value = int.Parse(code.Substring(3));
            }
            var newCode = "NV-"+$"{value + 1}".PadLeft(6, '0');
            return newCode;
        }
        /// <summary>
        /// Thêm mới 1 nhân viên
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>
        /// <returns>
        /// trả về số hàng bị ảnh hưởng
        /// </returns>
        /// Author: Đinh Văn Khánh - MF1112 (13/04/2022)
        public virtual int Insert(T entity)
        {
            //lấy tên của entity
            var entityName = typeof(T).Name;
            //lấy id mới nhất
            //lấy property Id của entity
            var propId = typeof(T).GetProperty($"{typeof(T).Name}Id");
            //set giá trị cho prop id
            propId.SetValue(entity, Guid.NewGuid());
            //khởi tạo kết nối
            var sqlConnection = new MySqlConnection(_sqlConnectionString);
            //lấy dữ liệu chèn tên bảng vào
            var sqlCommand = $"Proc_Insert{entityName}";
            //truyền tham số cho Procedure
            var dynamicParam = new DynamicParameters();
            //lấy danh sách property của entity
            var listProperties = typeof(T).GetProperties();
            foreach (var prop in listProperties)
            {
                //xem prop có cho phép map vào database không
                var isNotMap = prop.IsDefined(typeof(NotMap), false);
                if (!isNotMap)
                {
                    dynamicParam.Add($"{prop.Name}", entity.GetType().GetProperty(prop.Name).GetValue(entity));
                }
            }
            var res = sqlConnection.Execute(sql: sqlCommand, param: dynamicParam, commandType: System.Data.CommandType.StoredProcedure);
            //trả về kết quả
            return res;
        }
        /// <summary>
        /// cập nhật thông tin nhân viên
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="employeeId"></param>
        /// <returns>số hàng bị ảnh hưởng</returns>
        /// Author: Đinh Văn Khánh - MF1112 (13/04/2022)
        public virtual int Update(T entity)
        {
            var entityName = typeof(T).Name;
            //khởi tạo kết nối
            var sqlConnection = new MySqlConnection(_sqlConnectionString);
            //sử dụng proc update
            string sqlCommand = $"Proc_Update{entityName}";
            //truyền tham số cho Procedure
            var dynamicParam = new DynamicParameters();
            //lấy danh sách property của entity
            var listProperties = typeof(T).GetProperties();
            foreach (var prop in listProperties)
            {
                //xem prop có cho phép map vào database không
                var isNotMap = prop.IsDefined(typeof(NotMap), false);
                if (!isNotMap)
                {
                    //giá trị mới được truyền vào
                    var newVal = entity.GetType().GetProperty(prop.Name).GetValue(entity);
                    dynamicParam.Add($"{prop.Name}", newVal);
                }
            }
            //update dữ liệu
            var res = sqlConnection.Execute(sql: sqlCommand, param: dynamicParam, commandType: System.Data.CommandType.StoredProcedure);
            //trả về kết quả
            return res;
        }
        /// <summary>
        /// Lấy nhân viên theo mã id
        /// </summary>
        /// <param name="id">mã id nhân viên</param>
        /// <returns>
        /// nhân viên tìm được/null
        /// </returns>
        /// Author: Đinh Văn Khánh - MF1112 (13/04/2022)
        public virtual T GetById(Guid id)
        {
            var entityName = typeof(T).Name;
            //khởi tạo kết nối
            var sqlConnection = new MySqlConnection(_sqlConnectionString);
            //lấy dữ liệu
            string sqlCommand = $"SELECT * FROM {entityName} as e WHERE {entityName}Id = @{entityName}Id";
            //khởi tạo tham số
            var dynamicParam = new DynamicParameters();
            dynamicParam.Add($"@{entityName}Id", id);
            //dữ liệu trả về gồm các propperty của T
            var data = sqlConnection.Query<T>(sql: sqlCommand, param: dynamicParam).FirstOrDefault();
            //trả về kết quả
            return data;
        }

        /// <summary>
        /// Xóa 1 bản ghi với id truyền vào
        /// </summary>
        /// <param name="id">mã id nhân viên</param>
        /// <returns>
        /// số hàng bị ảnh hưởng
        /// </returns>
        /// Author: Đinh Văn Khánh - MF1112 (13/04/2022)
        public virtual int Delete(Guid id)
        {
            var entityName = typeof(T).Name;
            //khởi tạo kết nối
            var sqlConnection = new MySqlConnection(_sqlConnectionString);
            //lấy dữ liệu
            string sqlCommand = $"DELETE FROM {entityName} WHERE {entityName}Id = @{entityName}Id";
            //khởi tạo tham số
            var dynamicParam = new DynamicParameters();
            dynamicParam.Add($"@{entityName}Id", id);
            //dữ liệu trả về gồm các propperty của Employee
            var data = sqlConnection.Execute(sql: sqlCommand, param: dynamicParam);
            //trả về kết quả
            return data;
        }
        /// <summary>
        /// xóa nhiều bản ghi trong database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Author: Đinh Văn Khánh - MF1112 (13/04/2022)
        public virtual int MultiDelete(List<Guid> listId)
        {
            var entityName = typeof(T).Name;
            //khởi tạo kết nối
            var sqlConnection = new MySqlConnection(_sqlConnectionString);
            //ds nhân viên cần xóa
            var listParam = "";
            //khởi tạo tham số, sinh chuỗi tham số
            var dynamicParam = new DynamicParameters();
            //index
            var i = 0;
            foreach (var id in listId)
            {
                dynamicParam.Add($"@id{i}", id);
                listParam += $"@Id{i}" + ",";
                i++;
            }
            listParam = listParam.Substring(0, listParam.Length - 1).Trim();
            //lấy dữ liệu
            string sqlCommand = $"DELETE FROM {entityName} WHERE {entityName}Id IN ({listParam})";

            //dữ liệu trả về gồm các propperty của Employee
            var data = sqlConnection.Execute(sql: sqlCommand, param: dynamicParam);
            //trả về kết quả
            return data;
        }
        /// <summary>
        /// tìm bản ghi có code trùng vói code truyền vào
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public virtual T FindByCode(string code)
        {
            var entityName = typeof(T).Name;
            //khởi tạo kết nối
            var sqlConnection = new MySqlConnection(_sqlConnectionString);
            //lấy dữ liệu
            string sqlCommand = $"SELECT * FROM {entityName} WHERE {entityName}Code = @{entityName}Code";
            //khởi tạo tham số
            var dynamicParam = new DynamicParameters();
            dynamicParam.Add($"@{entityName}Code", code);
            //dữ liệu trả về thông tin của đối tượng
            var data = sqlConnection.Query<T>(sql: sqlCommand, param: dynamicParam).FirstOrDefault();
            //trả về kết quả
            return data;
        }
        #endregion
    }
}
