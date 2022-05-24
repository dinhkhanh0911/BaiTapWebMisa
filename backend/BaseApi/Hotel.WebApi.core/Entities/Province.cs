using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.WebApi.Core.Entities
{
    public class Province:BaseEntity
    {
        /// <summary>
        /// id bảng Province
        /// </summary>
        public int? ProvinceId { get; set; }

        /// <summary>
        /// tên Tỉnh/ thành phố
        /// </summary>
        public string? ProvinceName { get; set; }

        /// <summary>
        /// mô tả
        /// </summary>
        public string? ProvinceDescription { get; set; }

        /// <summary>
        /// max code phường/xã
        /// </summary>
        public string? ProvinceCode { get; set; }

        /// <summary>
        /// khóa ngoại bảng Country
        /// </summary>
        public int? CountryId { get; set; }
    }
}
