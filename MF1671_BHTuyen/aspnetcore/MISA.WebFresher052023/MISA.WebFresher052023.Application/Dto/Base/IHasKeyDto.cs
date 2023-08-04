using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Application.Dto.Base
{
    public interface IHasKeyDto
    {
        #region Methods
        /// <summary>
        /// Lấy mã Dto
        /// </summary>
        /// <returns>Mã Dto</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public string GetKeyCode(); 
        #endregion

    }
}
