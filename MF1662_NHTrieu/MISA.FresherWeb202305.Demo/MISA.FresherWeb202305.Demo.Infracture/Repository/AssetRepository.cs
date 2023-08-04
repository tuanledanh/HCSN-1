using Dapper;
using System.Data;
using MISA.FresherWeb202305.Demo.Domain.Entity;
using MISA.FresherWeb202305.Demo.Domain.Interface.Assets;
using MISA.FresherWeb202305.Demo.Domain.Models;
using MISA.FresherWeb202305.Demo.Domain.UnitOfWork;
using MISA.FresherWeb202305.Demo.Infracture.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.FresherWeb202305.Demo.Infracture.Repository
{
    /// <summary>
    /// Repository nhân viên
    /// </summary>
    /// <typeparam name="Employee">Entity nhân viên</typeparam>
    /// Created by: nhtrieu (13/07/2023)
    public class AssetRepository : BaseCodeRepository<Asset, AssetModel>, IAssetRepository
    {
        public AssetRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<Pagination> FilterAsync(string search, int currentPage, int pageSize, string departmentCode, string assetTypeCode)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_Search", search);
            parameters.Add("p_Page", currentPage);
            parameters.Add("p_PageSize", pageSize);
            parameters.Add("p_AssetTypeName",assetTypeCode);
            parameters.Add("p_DepartmentName", departmentCode);
            parameters.Add("p_TotalRecords", dbType: DbType.Int32, direction: ParameterDirection.Output);
            var sql = $"{Procedure}FilterAndPagination";
            var assetsModel=  await _unitOfWork.Connection.QueryAsync<AssetModel>(sql, parameters, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);
            var totalRecords = parameters.Get<int>("p_TotalRecords");

            return new Pagination()
            {
                Data = assetsModel,
                TotalRecords = totalRecords,
            };
        }
    }
}
