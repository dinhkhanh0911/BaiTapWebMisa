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
            using (var sqlConnection = new NpgsqlConnection(_sqlConnectionString))
            {
                sqlConnection.Open();
                //lấy dữ liệu

                var vendorTypeValue = vendorType != null ? $"'{vendorType}'" : "null";
                var isOwedValue = isOwed != null ? $"'{isOwed}'" : "null";
                var isUsedValue = isUsed != null ? $"'{isUsed}'" : "null";

                string sqlCommand = $"select public.func_filter_vendor('{filterText}',{vendorTypeValue},{isOwedValue},{isUsedValue},'{offset}','{limit}')";

                //var data = new List<T>();

                using (NpgsqlCommand cmd = new NpgsqlCommand(sqlCommand, sqlConnection))
                {
                    NpgsqlDataReader reader = cmd.ExecuteReader();

                    ////trả về kết quả
                    var data = BindingEntity.BindingData<string>(reader);
                    return data.FirstOrDefault();
                }

            }
        }
        public bool checkExitsPayment(Guid vendorId)
        {
            //khởi tạo kết nối
            using (var sqlConnection = new NpgsqlConnection(_sqlConnectionString))
            {
                sqlConnection.Open();
                //lấy dữ liệu
                string sqlCommand = $"SELECT * FROM payment p where p.vendor_id = '{vendorId}'";

                //var data = new List<T>();

                using (NpgsqlCommand cmd = new NpgsqlCommand(sqlCommand, sqlConnection))
                {
                    //cmd.Parameters.Add(new NpgsqlParameter("@payment_id", paymentId));
                    NpgsqlDataReader reader = cmd.ExecuteReader();
                    //dữ liệu trả về gồm các propperty của Employee
                    //và thêm các property: DeparmentId,DeparmentCode,PositionCode,PositionId
                    var data = BindingEntity.BindingData<Payment>(reader);
                    if (data.FirstOrDefault() != null) return true;
                    else return false;
                }
            } 
            //trả về kết quả
            return false;
        }
        #endregion
    }
}
