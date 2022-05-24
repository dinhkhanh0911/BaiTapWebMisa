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
    public class WardRepository : BaseRepository<Ward>, IWardRepository
    {
        public IEnumerable<Ward> GetByDistrict(int id)
        {
            //khởi tạo kết nối
            var sqlConnection = new MySqlConnection(_sqlConnectionString);
            //lấy dữ liệu
            string sqlCommand = $"SELECT * FROM Ward AS w WHERE w.DistrictId = @m_id";
            var param = new DynamicParameters();
            param.Add("@m_id", id);
            //dữ liệu trả về gồm các propperty của Employee
            //và thêm các property: DeparmentId,DeparmentCode,PositionCode,PositionId
            var data = sqlConnection.Query<Ward>(sql: sqlCommand, param);
            //trả về kết quả
            return data;
        }
    }
}
