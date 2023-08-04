using AutoMapper;
using MISA.WebFresher052023.Application.Dto.Department;
using MISA.WebFresher052023.Domain.Entity.Department;


namespace MISA.WebFresher052023.Application.Mapper
{
    public class DepartmentProfile : Profile
    {
        #region Constructors
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public DepartmentProfile()
        {
            CreateMap<DepartmentEntity, DepartmentDto>();
            CreateMap<DepartmentCreateDto, DepartmentEntity>();
            CreateMap<DepartmentUpdateDto, DepartmentEntity>();
        } 
        #endregion
    }
}
