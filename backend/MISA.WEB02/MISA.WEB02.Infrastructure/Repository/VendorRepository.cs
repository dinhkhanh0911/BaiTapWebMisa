using MISA.WEB02.Core.Entities;
using MISA.WEB02.Core.Interfaces;
using MISA.WEB02.Infrastructure.Helpers;
using Newtonsoft.Json;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB02.Infrastructure.Repository
{
    public class VendorRepository:BaseRespository<Vendor>,IVendorRepository
    {
        #region Methods

        public Object Filter(string filterText, int? vendorType, bool? isOwed,
            bool? isUsed, int currentPage, int pageSize)
        {
            int offset = (currentPage - 1) * pageSize;
            int limit = pageSize;

            //khởi tạo kết nối
            var sqlConnection = new NpgsqlConnection(_sqlConnectionString);

            sqlConnection.Open();
            //lấy dữ liệu

            var vendorTypeValue = vendorType != null ? $"'{vendorType}'" : "null";
            var isOwedValue = isOwed != null ? $"'{isOwed}'" : "null";
            var isUsedValue = isUsed != null ? $"'{isUsed}'" : "null";

            string sqlCommand = $"select public.func_filter_vendor('{filterText}',{vendorTypeValue},{isOwedValue},{isUsedValue},'{offset}','{limit}')";

            //var data = new List<T>();

            NpgsqlCommand cmd = new NpgsqlCommand(sqlCommand, sqlConnection);

            //cmd.CommandType = CommandType.StoredProcedure;
            
            //cmd.Parameters.Add(new NpgsqlParameter("filter_text", filterText));
            //cmd.Parameters.Add(new NpgsqlParameter("vendor_type", vendorTypeValue));
            //cmd.Parameters.Add(new NpgsqlParameter("is_owed", isOwedValue));
            //cmd.Parameters.Add(new NpgsqlParameter("is_used", isUsedValue));
            
            //cmd.Parameters.Add(new NpgsqlParameter("filter_offset", offset.ToString()));
            //cmd.Parameters.Add(new NpgsqlParameter("filter_limit", limit.ToString()));
            NpgsqlDataReader reader = cmd.ExecuteReader();

            ////trả về kết quả
            var data = BindingEntity.BindingData<string>(reader);
            return data.FirstOrDefault();
        }
        #endregion
    }
}
