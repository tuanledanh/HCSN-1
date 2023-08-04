﻿using MISA.FresherWeb202305.Demo.Domain.Entity;
using MISA.FresherWeb202305.Demo.Domain.Interface.department;
using MISA.FresherWeb202305.Demo.Domain.UnitOfWork;
using MISA.FresherWeb202305.Demo.Infracture.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.FresherWeb202305.Demo.Infracture.Repository
{
    public class DepartmentRepository : BaseCodeRepository<Department, Department>, IDepartmentRepository
    {
        public DepartmentRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
