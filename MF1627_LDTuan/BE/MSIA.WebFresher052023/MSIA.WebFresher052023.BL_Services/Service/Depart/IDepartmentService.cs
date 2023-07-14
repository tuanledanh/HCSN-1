using MSIA.WebFresher052023.BL_Services.DTO.Depart;
using MSIA.WebFresher052023.DL_Repositories.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSIA.WebFresher052023.BL_Services.Service.Depart
{
    public interface IDepartmentService : IBaseService<Department, DepartmentDto, DepartmentCreateDto, DepartmentUpdateDto>
    {
    }
}
