using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.WebApi.Core.Entities
{
    public class Ward:BaseEntity
    {
        /// <summary>
        /// id bảng phường/xã
        /// </summary>
        public int? WardId { get; set; }
        /// <summary>
        /// tên xã/phường
        /// </summary>

        public string? WardName { get; set; }
        /// <summary>
        /// mô tả
        /// </summary>
        public string? WardDescription { get; set; }
        /// <summary>
        /// max code phường/xã
        /// </summary>
        public string? WardCode { get; set; }
        /// <summary>
        /// khóa ngoại bảng District
        /// </summary>
        public int? DistrictId { get; set; }
    }
}
