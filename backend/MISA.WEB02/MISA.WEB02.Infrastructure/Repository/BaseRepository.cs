using Dapper;
using MISA.WEB02.Core.Entities;
using MISA.WEB02.Core.Interfaces.Base;
using MISA.WEB02.Infrastructure.Helpers;
using MySqlConnector;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
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
            _sqlConnectionString = "Server=127.0.0.1;Port=5432;Database=misa_web02_dvkhanh;User Id=postgres;Password=09112000";
        }
        #endregion
        #region Methods
        /// <summary>
        /// Lấy toàn bộ dữ liệu trong bảng
        /// </summary>
        /// <returns>
        /// Danh sách dữ liệu
        /// </returns>
        /// Author: Đinh Văn Khánh-MF1112  (13/04/2022)
        public virtual IEnumerable<T> Get()
        {
            var entityName = typeof(T).Name;
            //khởi tạo kết nối
            var sqlConnection = new NpgsqlConnection(_sqlConnectionString);

            sqlConnection.Open();
            //lấy dữ liệu
            string sqlCommand = $"SELECT * FROM {BindingEntity.ToSnakeCase(entityName)}";

            //var data = new List<T>();

            using (NpgsqlCommand cmd = new NpgsqlCommand(sqlCommand, sqlConnection))
            {
                NpgsqlDataReader reader = cmd.ExecuteReader();
                
                //dữ liệu trả về gồm các propperty của Employee
                //và thêm các property: DeparmentId,DeparmentCode,PositionCode,PositionId
                var data = BindingEntity.BindingData<T>(reader);
                return data;
            }
            //trả về kết quả
            return null;
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
            var entityName = typeof(T).Name;
            //khởi tạo kết nối
            var sqlConnection = new NpgsqlConnection(_sqlConnectionString);
            // Mở kết nối
            sqlConnection.Open();
            //sử dụng proc update
            string sqlCommand = $"Func_Create{entityName}";


            using (NpgsqlCommand cmd = new NpgsqlCommand(sqlCommand, sqlConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                //lấy danh sách property của entity
                var listProperties = typeof(T).GetProperties();
                foreach (var prop in listProperties)
                {
                    //xem prop có cho phép map vào database không
                    var isNotMap = prop.IsDefined(typeof(NotMap), false);
                    if (!isNotMap)
                    {
                        //giá trị mới được truyền vào
                        var type = entity.GetType().GetProperty(prop.Name).PropertyType.Name;

                        if (type.Equals("Guid"))
                        {
                            var guidId = Guid.NewGuid();
                            cmd.Parameters.Add(new NpgsqlParameter($"@{BindingEntity.ToSnakeCase(prop.Name)}", guidId.ToString()));
                        }
                        else
                        {
                            var newVal = entity.GetType().GetProperty(prop.Name).GetValue(entity);
                            cmd.Parameters.Add(new NpgsqlParameter($"@{BindingEntity.ToSnakeCase(prop.Name)}", newVal));
                        }


                    }
                }
                cmd.Prepare();
                var retuntcnt = cmd.ExecuteScalar();
                return 1;
            }
            return 0;
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
            var sqlConnection = new NpgsqlConnection(_sqlConnectionString);
            // Mở kết nối
            sqlConnection.Open();
            //sử dụng proc update
            string sqlCommand = $"Func_Update{entityName}";
            
            
            using (NpgsqlCommand cmd = new NpgsqlCommand(sqlCommand, sqlConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                //lấy danh sách property của entity
                var listProperties = typeof(T).GetProperties();
                foreach (var prop in listProperties)
                {
                    //xem prop có cho phép map vào database không
                    var isNotMap = prop.IsDefined(typeof(NotMap), false);
                    if (!isNotMap)
                    {
                        //giá trị mới được truyền vào
                        var type = entity.GetType().GetProperty(prop.Name).PropertyType.Name;
                        var newVal = entity.GetType().GetProperty(prop.Name).GetValue(entity);
                        if (type.Equals("Guid"))
                        {
                            cmd.Parameters.Add(new NpgsqlParameter($"@{BindingEntity.ToSnakeCase(prop.Name)}", newVal.ToString()));
                        }
                        else cmd.Parameters.Add(new NpgsqlParameter($"@{BindingEntity.ToSnakeCase(prop.Name)}", newVal));


                    }
                }
                cmd.Prepare();
                var retuntcnt = cmd.ExecuteScalar();
                return 1;
            }
            return 0;
            
            ////update dữ liệu
            //var res = sqlConnection.Execute(sql: sqlCommand, param: dynamicParam, commandType: System.Data.CommandType.StoredProcedure);
            ////trả về kết quả
            //return res;
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
            var sqlConnection = new NpgsqlConnection(_sqlConnectionString);

            sqlConnection.Open();
            //lấy dữ liệu
            string sqlCommand = $"SELECT * FROM {entityName} as e WHERE {entityName}_id = @{entityName}_id";

            //var data = new List<T>();

            NpgsqlCommand cmd = new NpgsqlCommand(sqlCommand, sqlConnection);

            var entityId = $"@{entityName}_Id";


            cmd.Parameters.Add(new NpgsqlParameter(entityId, id.ToString()));
            NpgsqlDataReader reader = cmd.ExecuteReader();

            //dữ liệu trả về gồm các propperty của Employee
            //và thêm các property: DeparmentId,DeparmentCode,PositionCode,PositionId
            var data = BindingEntity.BindingData<T>(reader);
            return data.FirstOrDefault<T>();
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
            var sqlConnection = new NpgsqlConnection(_sqlConnectionString);

            sqlConnection.Open();
            //lấy dữ liệu
            string sqlCommand = $"DELETE FROM {entityName} as e WHERE {entityName}_id = @{entityName}_id";

            //var data = new List<T>();

            NpgsqlCommand cmd = new NpgsqlCommand(sqlCommand, sqlConnection);

            var entityId = $"@{entityName}_Id";


            cmd.Parameters.Add(new NpgsqlParameter(entityId, id.ToString()));
            NpgsqlDataReader reader = cmd.ExecuteReader();

            return 1;
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
            var sqlConnection = new NpgsqlConnection(_sqlConnectionString);

            sqlConnection.Open();
            //lấy dữ liệu
            string sqlCommand = $"SELECT * FROM {entityName} as e WHERE {entityName}_code = @{entityName}_code";

            //var data = new List<T>();

            NpgsqlCommand cmd = new NpgsqlCommand(sqlCommand, sqlConnection);

            var entityId = $"@{entityName}_code";


            cmd.Parameters.Add(new NpgsqlParameter(entityId, code));
            NpgsqlDataReader reader = cmd.ExecuteReader();

            //dữ liệu trả về gồm các propperty của Employee
            //và thêm các property: DeparmentId,DeparmentCode,PositionCode,PositionId
            var data = BindingEntity.BindingData<T>(reader);
            return data.FirstOrDefault<T>();
        }
        #endregion
    }
}
