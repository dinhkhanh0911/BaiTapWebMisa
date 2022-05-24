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
using System.Text.RegularExpressions;
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
            _sqlConnectionString = "Server=127.0.0.1;Port=5432;Database=MISA_WEB02_AMIS;User Id=postgres;Password=09112000";
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
            //table name snake
            var tableName = BindingEntity.ToSnakeCase(typeof(T).Name);
            //khởi tạo kết nối
            var sqlConnection = new NpgsqlConnection(_sqlConnectionString);

            sqlConnection.Open();

            //sql query
            var commandText = $"SELECT e.{tableName}_code FROM {tableName} as e order by e.{tableName}_code DESC limit 1";

            NpgsqlCommand cmd = new NpgsqlCommand(commandText, sqlConnection);
            NpgsqlDataReader reader = cmd.ExecuteReader();

            //trả về danh sách các code với các key và value
            var data = BindingEntity.BindingData<string>(reader).FirstOrDefault();
            var str = data.Split('-');
            var newCode = Int32.Parse(str[1])+1;
            //var listEntityCode = data.Result;
            ////danh sách code khi tách 2 chữ đầu
            //var listCode = new List<int>();
            ////xóa hết các kí tự là chữ trong code
            //string pattern = @"\D";
            //for (int i = 0; i < listEntityCode.Count(); i++)
            //{
            //    var code = listEntityCode.ElementAt(i);
            //    listCode.Add(int.Parse(Regex.Replace(code.ToString(), pattern, "")));
            //}
            //var newCode = $"1";
            //if (listCode.Count > 0)
            //{
            //    newCode = $"{listCode.Max() + 1}";
            //}
            return $"{str[0]}-{newCode.ToString()}";
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
            string sqlCommand = $"Func_Create_{entityName}";


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
                        if(prop.Name == "paymentDetails")
                        {

                        }
                        if (type.Equals("Guid") || (newVal != null && newVal.GetType().Name.Equals("Guid")))
                        {
                            cmd.Parameters.Add(new NpgsqlParameter($"@{BindingEntity.ToSnakeCase(prop.Name)}", newVal.ToString()));
                        }
                        else if (newVal != null) cmd.Parameters.Add(new NpgsqlParameter($"@{BindingEntity.ToSnakeCase(prop.Name)}", newVal));


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
            string sqlCommand = $"Func_Update_{entityName}";
            
            
            using (NpgsqlCommand cmd = new NpgsqlCommand(sqlCommand, sqlConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                //lấy danh sách property của entity
                var listProperties = typeof(T).GetProperties();
                var test = entity.GetType().GetProperty(listProperties[4].Name).GetValue(entity);
                foreach (var prop in listProperties)
                {
                    //xem prop có cho phép map vào database không
                    var isNotMap = prop.IsDefined(typeof(NotMap), false);
                    if (!isNotMap)
                    {
                        //giá trị mới được truyền vào
                        var type = entity.GetType().GetProperty(prop.Name).PropertyType.Name;
                        var newVal = entity.GetType().GetProperty(prop.Name).GetValue(entity);
                        if (type.Equals("Guid") || (newVal != null && newVal.GetType().Name.Equals("Guid")))
                        {
                            cmd.Parameters.Add(new NpgsqlParameter($"@{BindingEntity.ToSnakeCase(prop.Name)}", newVal.ToString()));
                        }
                        else if(newVal != null) cmd.Parameters.Add(new NpgsqlParameter($"@{BindingEntity.ToSnakeCase(prop.Name)}", newVal));


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
            var sqlConnection = new NpgsqlConnection(_sqlConnectionString);

            sqlConnection.Open();
            //ds nhân viên cần xóa
            var listParam = "";
            //lấy dữ liệu
            string sqlCommand = $"DELETE FROM {entityName} WHERE {entityName}_Id IN ({listParam})";
            NpgsqlCommand cmd = new NpgsqlCommand(sqlCommand, sqlConnection);
            //index
            var i = 0;
            foreach (var id in listId)
            {
                cmd.Parameters.Add(new NpgsqlParameter($"@id{i}", id.ToString()));
                //dynamicParam.Add($"@id{i}", id);
                listParam += $"@Id{i}" + ",";
                i++;
            }
            listParam = listParam.Substring(0, listParam.Length - 1).Trim();
            sqlCommand = $"DELETE FROM {entityName} WHERE {entityName}_Id IN ({listParam})";
            cmd.CommandText = sqlCommand;


            
            


            NpgsqlDataReader reader = cmd.ExecuteReader();

            //trả về kết quả
            return 1;
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

        public Object Filter(string filterText, int currentPage, int pageSize)
        {
            int offset = (currentPage - 1) * pageSize;
            int limit = pageSize;

            var entityName = typeof(T).Name;

            //khởi tạo kết nối
            var sqlConnection = new NpgsqlConnection(_sqlConnectionString);

            sqlConnection.Open();
            //lấy dữ liệu
            string sqlCommand = $"func_filter_{entityName}";

            //var data = new List<T>();

            NpgsqlCommand cmd = new NpgsqlCommand(sqlCommand, sqlConnection);

            cmd.CommandType = CommandType.StoredProcedure;
            if(filterText == null)
            {
                cmd.Parameters.Add(new NpgsqlParameter("filter_text", ""));
            }
            else cmd.Parameters.Add(new NpgsqlParameter("filter_text", filterText));
            cmd.Parameters.Add(new NpgsqlParameter("filter_offset", offset.ToString()));
            cmd.Parameters.Add(new NpgsqlParameter("filter_limit", limit.ToString()));
            NpgsqlDataReader reader = cmd.ExecuteReader();

            ////trả về kết quả
            var data = BindingEntity.BindingData<string>(reader);
            return data.FirstOrDefault();
        }
        #endregion
    }
}
