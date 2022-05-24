using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.WebApi.Core.Entities
{
    public class Country:BaseEntity
    {
        /// <summary>
        /// id bảng Country
        /// </summary>
        public int? CountryId { get; set; }
        /// <summary>
        /// Tên nước
        /// </summary>
        public string? CountryName { get; set; }
        /// <summary>
        /// mô tả
        /// </summary>
        public string? CountryDescription { get; set; }
        /// <summary>
        /// max code phường/xã
        /// </summary>
        public string? CountryCode { get; set; }
    }
}
