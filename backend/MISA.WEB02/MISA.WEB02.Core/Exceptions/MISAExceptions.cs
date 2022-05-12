using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB02.Core.Exceptions
{
    public class MISAExceptions : Exception
    {
        public string MISAMessenger { get; set; }
        public IDictionary MISAErrorData { get; set; }
        public MISAExceptions(string msg, IDictionary data = null)
        {
            this.MISAMessenger = msg;
            this.MISAErrorData = data;
        }
        public override IDictionary Data => this.MISAErrorData;
        public override string Message => this.MISAMessenger;
    }
}
