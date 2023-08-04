using MISA.FresherWeb202305.Demo.Application.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.FresherWeb202305.Demo.Application.Interface.Base
{
    public interface IBaseCodeService<TEntityDto, TEntityCreateDto, TEntityUpdateDto> :
        IBaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto>
    {
        /// <summary>
        /// Lấy mã mới
        /// </summary>
        /// <returns>Mã mới</returns>
        /// CreatedBy: nhtrieu (18/07/2023)
        Task<string?> FindNewCodeAsync();
    }
}
