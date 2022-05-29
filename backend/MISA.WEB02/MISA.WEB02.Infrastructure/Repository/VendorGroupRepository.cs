using MISA.WEB02.Core.Entities;
using MISA.WEB02.Core.Interfaces;
using MISA.WEB02.Core.Interfaces.Base;
using MISA.WEB02.Infrastructure.Helpers;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB02.Infrastructure.Repository
{
    public class VendorGroupRepository : BaseRespository<VendorGroup>,IVendorGroupRepository
    {
        public IEnumerable<VendorGroup> search(string searchValue)
        {
            //khởi tạo kết nối
            using (var sqlConnection = new NpgsqlConnection(_sqlConnectionString))
            {
                sqlConnection.Open();
                //lấy dữ liệu
                string sqlCommand = $"func_search_vendor_group";

                //var data = new List<T>();

                using (NpgsqlCommand cmd = new NpgsqlCommand(sqlCommand, sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new NpgsqlParameter("search_value", searchValue.ToString()));
                    NpgsqlDataReader reader = cmd.ExecuteReader();
                    //dữ liệu trả về gồm các propperty của Employee
                    //và thêm các property: DeparmentId,DeparmentCode,PositionCode,PositionId
                    var data = BindingEntity.BindingData<VendorGroup>(reader);
                    //trả về kết quả
                    return data;
                }
            }
        }
    }
}
