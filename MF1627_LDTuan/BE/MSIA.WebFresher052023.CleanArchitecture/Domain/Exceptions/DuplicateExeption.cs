using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class DuplicateExeption : Exception
    {
        #region Methods
        public DuplicateExeption(string message) : base(message) { } 
        #endregion
    }
}
