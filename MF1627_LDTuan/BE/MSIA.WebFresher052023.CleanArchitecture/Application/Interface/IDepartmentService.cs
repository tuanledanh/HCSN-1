using Application.DTO.Depart;
using Domain.Entity;
using Domain.Model;

namespace Application.Interface
{
    public interface IDepartmentService : IBaseService<Department, DepartmentModel, DepartmentDto, DepartmentCreateDto, DepartmentUpdateDto>
    {
    }
}
