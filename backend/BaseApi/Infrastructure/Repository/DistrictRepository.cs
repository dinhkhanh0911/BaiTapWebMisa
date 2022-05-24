using Base.WebApi.core.Interfaces;
using Base.WebApi.Core.Entities;
using Dapper;
using MISA.Infrastructure.Respository;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Respository
{
    public class DistrictRepository : BaseRepository<District>, IDistrictRepository
    {
        public IEnumerable<District> GetByProvince(int id)
        {
            //khởi tạo kết nối
            var sqlConnection = new MySqlConnection(_sqlConnectionString);
            //lấy dữ liệu
            string sqlCommand = $"SELECT * FROM District AS d WHERE d.ProvinceId = @m_id";
            var param = new DynamicParameters();
            param.Add("@m_id", id);
            //danh sach cas huyen tim ddc
            var data = sqlConnection.Query<District>(sql: sqlCommand, param);
            //trả về kết quả
            return data;
        }
    }
}
