using MISA.FresherWeb202305.Demo.Application.Dto;
using MISA.FresherWeb202305.Demo.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace MISA.FresherWeb202305.Demo.Application.Map
{
    public class DepartmentMapper :Profile
    {
        public DepartmentMapper()
        {
            // Trả về dữ liệu
            CreateMap<Department, DepartmentDto>();

            // Trường hợp tạo mới
            CreateMap<DepartmentCreateDto, Department>();

            // Trường hợp cập nhật
            CreateMap<DepartmentUpdateDto, Department>();
        }
    }
}
