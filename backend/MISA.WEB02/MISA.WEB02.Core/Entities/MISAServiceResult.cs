using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB02.Core.Entities
{
    /// <summary>
    /// đối tượng trả về khi gọi api
    /// </summary>
    public class MISAServiceResult
    {
        #region Properties
        public string? devMsg { get; set; } = null;
        public string? userMsg { get; set; } = null;
        public object? data { get; set; } = null;
        #endregion
    }
}
