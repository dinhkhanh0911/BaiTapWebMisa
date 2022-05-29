using Dapper;
using MISA.WEB02.Core.Entities;
using MISA.WEB02.Core.Interfaces;
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
    public class PaymentRepository:BaseRespository<Payment>,IPaymentRepository
    {
        #region Methods

        public Object Filter(string filterText, int? paymentType, bool? isRecord,
            DateTime? startDate,DateTime? endDate, int currentPage, int pageSize)
        {
            int offset = (currentPage - 1) * pageSize;
            int limit = pageSize;

            //khởi tạo kết nối
            var sqlConnection = new NpgsqlConnection(_sqlConnectionString);

            sqlConnection.Open();
            //lấy dữ liệu

            var paymentTypeValue = paymentType != null ? $"'{ paymentType}'" : "null";
            var isRecordValue = isRecord != null ? $"'{isRecord}'" : "null";
            var startDateValue = startDate != null ? $"'{startDate}'" : "null";
            var endDateValue = endDate != null ? $"'{endDate}'" : "null";

            string sqlCommand = $"select public.func_filter_payment('{filterText}',{paymentTypeValue},{isRecordValue},{startDateValue},{endDateValue},'{offset}','{limit}')";

            //var data = new List<T>();

            using (NpgsqlCommand cmd = new NpgsqlCommand(sqlCommand, sqlConnection))
            {
                NpgsqlDataReader reader = cmd.ExecuteReader();

                ////trả về kết quả
                var data = BindingEntity.BindingData<string>(reader);

                return data.FirstOrDefault();
            }
                
        }

        public IEnumerable<PaymentDetail> GetPaymentDetailByPaymentId(Guid paymentId)
        {
            //khởi tạo kết nối
            using (var sqlConnection = new NpgsqlConnection(_sqlConnectionString))
            {
                sqlConnection.Open();
                //lấy dữ liệu
                string sqlCommand = $"SELECT P.*,v.vendor_name,v.vendor_code FROM payment_detail p inner join vendor v on p.vendor_id = v.vendor_id where p.payment_id = '{paymentId}'";

                //var data = new List<T>();

                using (NpgsqlCommand cmd = new NpgsqlCommand(sqlCommand, sqlConnection))
                {
                    //cmd.Parameters.Add(new NpgsqlParameter("@payment_id", paymentId));
                    NpgsqlDataReader reader = cmd.ExecuteReader();
                    //dữ liệu trả về gồm các propperty của Employee
                    //và thêm các property: DeparmentId,DeparmentCode,PositionCode,PositionId
                    var data = BindingEntity.BindingData<PaymentDetail>(reader);
                    return data;
                }
            }

            //trả về kết quả
            return null;
        }
        public override int Insert(Payment payment)
        {
            //khởi tạo kết nối
            using (var sqlConnection = new NpgsqlConnection(_sqlConnectionString))
            {
                // Mở kết nối
                sqlConnection.Open();

                NpgsqlTransaction trans = null;
                //sử dụng proc update
                string sqlCommand = $"Func_Create_payment";
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sqlCommand, sqlConnection))
                    {
                        trans = sqlConnection.BeginTransaction();
                        cmd.CommandType = CommandType.StoredProcedure;
                        for (var i = 0; i < 2; ++i)
                        {
                            if (i == 0)
                            {
                                //lấy danh sách property của entity
                                var listProperties = typeof(Payment).GetProperties();
                                var dynamicParam = new DynamicParameters();
                                foreach (var prop in listProperties)
                                {
                                    //xem prop có cho phép map vào database không
                                    var isNotMap = prop.IsDefined(typeof(NotMap), false);
                                    if (!isNotMap)
                                    {
                                        //giá trị mới được truyền vào
                                        var type = payment.GetType().GetProperty(prop.Name).PropertyType.Name;
                                        var newVal = payment.GetType().GetProperty(prop.Name).GetValue(payment);

                                        if (type.Equals("Guid") || (newVal != null && newVal.GetType().Name.Equals("Guid")))
                                        {

                                            cmd.Parameters.Add(new NpgsqlParameter($"@{BindingEntity.ToSnakeCase(prop.Name)}", newVal.ToString()));
                                        }
                                        else if (newVal != null)
                                            cmd.Parameters.AddWithValue($"@{BindingEntity.ToSnakeCase(prop.Name)}", newVal.ToString());
                                        else if (newVal == null)
                                            cmd.Parameters.AddWithValue($"@{BindingEntity.ToSnakeCase(prop.Name)}", DBNull.Value);

                                    }
                                }
                                cmd.Prepare();

                                var retuntcnt = cmd.ExecuteScalar();
                            }
                            if (i == 1)
                            {
                                cmd.CommandText = $"Func_Create_payment_detail";
                                var paymentDetails = (List<PaymentDetail>)payment.GetType().GetProperty("PaymentDetails").GetValue(payment);
                                if (paymentDetails != null)
                                {
                                    foreach (var paymentDetail in paymentDetails)
                                    {
                                        cmd.Parameters.Clear();
                                        //lấy danh sách property của entity
                                        var listProperties = typeof(PaymentDetail).GetProperties();
                                        foreach (var prop in listProperties)
                                        {
                                            //xem prop có cho phép map vào database không
                                            var isNotMap = prop.IsDefined(typeof(NotMap), false);
                                            if (!isNotMap)
                                            {
                                                //giá trị mới được truyền vào
                                                var type = paymentDetail.GetType().GetProperty(prop.Name).PropertyType.Name;
                                                var newVal = paymentDetail.GetType().GetProperty(prop.Name).GetValue(paymentDetail);

                                                if (type.Equals("Guid") || (newVal != null && newVal.GetType().Name.Equals("Guid")))
                                                {

                                                    cmd.Parameters.Add(new NpgsqlParameter($"@{BindingEntity.ToSnakeCase(prop.Name)}", newVal.ToString()));
                                                }
                                                else if (newVal != null)
                                                    cmd.Parameters.AddWithValue($"@{BindingEntity.ToSnakeCase(prop.Name)}", newVal.ToString());
                                                else if (newVal == null)
                                                    cmd.Parameters.AddWithValue($"@{BindingEntity.ToSnakeCase(prop.Name)}", NpgsqlTypes.NpgsqlDbType.Text, DBNull.Value);

                                            }
                                        }
                                        cmd.Prepare();
                                        var retuntcnt = cmd.ExecuteScalar();
                                    }
                                }

                            }

                        }

                        Payment p = base.FindByCode(payment.PaymentCode);


                        trans.Commit();
                        return 1;
                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return 0;
                }
            }
                
            
        }

        /// <summary>
        /// thêm mới phiếu chi chi tiết
        /// </summary>
        /// <param name="paymentDetail"> Phiếu chi chi tiết </param>
        /// <returns></returns>
        /// Author: Đinh Văn Khánh (22/04/2022)
        public int InsertPaymentDetail(PaymentDetail paymentDetail)
        {

            //khởi tạo kết nối
            using (var sqlConnection = new NpgsqlConnection(_sqlConnectionString))
            {
                NpgsqlTransaction trans = null;

                // Mở kết nối
                sqlConnection.Open();
                trans = sqlConnection.BeginTransaction();
                //sử dụng proc update
                string sqlCommand = $"Func_Create_payment_detail";


                using (NpgsqlCommand cmd = new NpgsqlCommand(sqlCommand, sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    //lấy danh sách property của entity
                    var listProperties = typeof(PaymentDetail).GetProperties();
                    foreach (var prop in listProperties)
                    {
                        //xem prop có cho phép map vào database không
                        var isNotMap = prop.IsDefined(typeof(NotMap), false);
                        if (!isNotMap)
                        {
                            //giá trị mới được truyền vào
                            var type = paymentDetail.GetType().GetProperty(prop.Name).PropertyType.Name;
                            var newVal = paymentDetail.GetType().GetProperty(prop.Name).GetValue(paymentDetail);

                            if (type.Equals("Guid") || (newVal != null && newVal.GetType().Name.Equals("Guid")))
                            {

                                cmd.Parameters.Add(new NpgsqlParameter($"@{BindingEntity.ToSnakeCase(prop.Name)}", newVal.ToString()));
                            }
                            else if (newVal != null)
                                cmd.Parameters.AddWithValue($"@{BindingEntity.ToSnakeCase(prop.Name)}", newVal.ToString());
                            else if (newVal == null)
                                cmd.Parameters.AddWithValue($"@{BindingEntity.ToSnakeCase(prop.Name)}", DBNull.Value);

                        }
                    }
                    cmd.Prepare();
                    var retuntcnt = cmd.ExecuteScalar();
                    return 1;
                }
            }
                
            return 0;
            
        }
        #endregion
    }
}
