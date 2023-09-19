﻿using Dapper;
using MISA.WebFresher052023.Domain.Entity.Department;
using MISA.WebFresher052023.Domain.Interface.department;
using MISA.WebFresher052023.Domain.Interface.UnitOfWork;
using MISA.WebFresher052023.Infrastructure.Repository.Base;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Infrastructure.Repository
{
    public class DepartmentRepository : BaseRepository<DepartmentEntity>, IDepartmentRepository
    {
        #region Constructors
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public DepartmentRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        #endregion

        #region Properties
        /// <summary>
        /// Ghi đè tên bảng
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public override string TableName { get; protected set; } = "Department";

        /// <summary>
        /// Ghi đè tên bảng dạng số nhiều
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023) 
        public override string TableNames { get; protected set; } = "Departments";

        /// <summary>
        /// Ghi đè tên khóa chính
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public override string TableNameId { get; protected set; } = "DepartmentId";
        #endregion

    }
}