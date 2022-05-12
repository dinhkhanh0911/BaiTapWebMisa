using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB02.Core.Enum
{
    public enum GenderEnum
    {
        /// <summary>
        /// Nữ
        /// </summary>
        Female=0,

        /// <summary>
        /// Nam
        /// </summary>
        Male = 1,

        /// <summary>
        /// Giới tính khác
        /// </summary>
        Other = 2,
    }
    public enum WorkStatusEnum
    {

        /// <summary>
        /// Đang làm việc
        /// </summary>
        Working = 1,


        /// <summary>
        /// Đã nghỉ việc
        /// </summary>
        Stop = 0,

        /// <summary>
        /// Nghỉ lễ
        /// </summary>
        Holiday = 2,
    }
}
