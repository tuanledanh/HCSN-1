using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Domain.Exceptions
{
    public class UserException : Exception
    {
        public List<string>? MoreInfo { get; set; }

        public UserException() { }

        public UserException(string message) : base(message) { }

        public UserException(string message, List<string> moreInfo) : base(message)
        {
            MoreInfo = moreInfo;
        }
    }
}
