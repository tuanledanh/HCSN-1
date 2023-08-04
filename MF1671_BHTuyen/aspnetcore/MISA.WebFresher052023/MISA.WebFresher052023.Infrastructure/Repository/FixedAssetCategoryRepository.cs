using Dapper;
using MISA.WebFresher052023.Domain.Entity.Department;
using MISA.WebFresher052023.Domain.Entity.FixedAssetCategory;
using MISA.WebFresher052023.Domain.Interface.FixedAssetCategory;
using MISA.WebFresher052023.Domain.Interface.UnitOfWork;
using MISA.WebFresher052023.Infrastructure.Repository.Base;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Infrastructure.Repository
{
    public class FixedAssetCategoryRepository : BaseRepository<FixedAssetCategoryEntity>, IFixedAssetCategoryRepository
    {
        #region Constructors
        /// <summary>
        /// Hà khởi tạo
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public FixedAssetCategoryRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Ghi đè tên bảng
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public override string TableName { get; protected set; } = "FixedAssetCategory";

        /// <summary>
        /// Ghi đè tên bảng số nhiều
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public override string TableNames { get; protected set; } = "FixedAssetCategories";

        /// <summary>
        /// Ghi đè tên khóa chính
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public override string TableNameId { get; protected set; } = "FixedAssetCategoryId";
        #endregion
    }
}
