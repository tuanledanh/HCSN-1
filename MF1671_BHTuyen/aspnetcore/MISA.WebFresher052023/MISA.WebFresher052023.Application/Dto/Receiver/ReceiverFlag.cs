using MISA.WebFresher052023.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Application.Dto.Receiver
{
    public class ReceiverFlag
    {
        public ReceiverDto Receiver { get; set; }

        public ActionMode ActionMode { get; set; }
    }
}
