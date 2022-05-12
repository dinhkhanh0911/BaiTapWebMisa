using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB02.API.Test.Entities;
using MySqlConnector;

namespace MISA.WEB02.API.Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {

        //Lấy dữ liệu : GET
        [HttpGet]
        public IActionResult Get()
        {
            //Khai báo chuỗi kết nối
            String sqlConnectionString = "Server=3.0.89.182;Port=3306;Database=MISA.WEB02.DVKHANH;Uid=dev;Pwd=12345678;";

            //Khởi tạo kết nối
            var sqlConnection = new MySqlConnection(sqlConnectionString);

            var data = sqlConnection.Query<Object>("SELECT * FROM Employee");


            return Ok(data);
        }
        // Thêm mới dữ liệu :POST

        // Cập nhật dữ liệu :PUT

        // Xóa dữ liệu: DELETE
    }
}
