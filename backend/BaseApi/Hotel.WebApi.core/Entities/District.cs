using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.WebApi.Core.Entities
{
    public class District:BaseEntity
    {
        /// <summary>
        /// id bảng Province
        /// </summary>
        public int? DistrictId { get; set; }

        /// <summary>
        /// tên Tỉnh/ thành phố
        /// </summary>
        public string? DistrictName { get; set; }

        /// <summary>
        /// mô tả
        /// </summary>
        public string? DistrictDescription { get; set; }

        /// <summary>
        /// max code quận/huyện
        /// </summary>
        public string? DistrictCode { get; set; }

        /// <summary>
        /// khóa ngoại bảng Province
        /// </summary>
        public int? ProvinceId { get; set; }
    }
}
