using Dapper;
using MISA.WebFresher052023.Domain.Entity.FixedAsset;
using MISA.WebFresher052023.Domain.Interface.FixedAsset;
using MISA.WebFresher052023.Domain.Interface.UnitOfWork;
using MISA.WebFresher052023.Domain.Model.FixedAsset;
using MISA.WebFresher052023.Infrastructure.Repository.Base;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace MISA.WebFresher052023.Infrastructure.Repository
{
    public class FixedAssetRepository : BaseRepository<FixedAssetEntity>, IFixedAssetRepository
    {
        #region Constructors
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="unitOfWork">unitOfWork</param>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public FixedAssetRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Ghi đè tên bảng dạng số nhiều
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023) 
        public override string TableNamesProc { get; protected set; } = "FixedAssets";

        /// <summary>
        /// Ghi đè tên bảng
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public override string TableNameProc { get; protected set; } = "FixedAsset";

        /// <summary>
        /// Ghi đè tên khóa chính
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public override string TableNameId { get; protected set; } = "FixedAssetId";
        #endregion

        #region Methods
        /// <summary>
        /// Sinh mã tài sản
        /// </summary>
        /// <returns>Mã tài sản</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public async Task<string> GetFixedAssetCodeNewAsync()
        {
            var procname = $"Proc_Get{TableNameProc}CodeNew";
            var transferAssetCode = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<string>(procname, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);
            return transferAssetCode;
        }

        /// <summary>
        /// Xuất danh sách tài sản ra file excel
        /// </summary>
        /// <param name="FixedAssetExcelEntities">Danh sách tài sản</param>
        /// <returns>Nội dung file excel</returns>
        /// Created By: Bùi Huy Tuyền (27/07/2023)
        public byte[] ExportListFixedAssetToExcel(IEnumerable<FixedAssetExcelModel> fixedAssetExcelModels)
        {
            var stream = new MemoryStream();

            var templateFile = Path.Combine(Directory.GetCurrentDirectory(), "TemplateExcel", "TemplateListFixedAsset.xlsx");

            var fileInfo = new FileInfo(templateFile);

            if (fileInfo.Exists)
            {
                using var pck = new ExcelPackage(fileInfo);

                var ws = pck.Workbook.Worksheets["FixedAsset"];

                var startRow = 2;

                foreach (var fixedAssetExcelModel in fixedAssetExcelModels)
                {
                    ws.Cells.SetCellValue(startRow, 0, startRow - 1);
                    startRow++;
                    ws.Row(startRow).Height = 30;
                    for (var i = 1; i <= 18; i++)
                    {
                        ws.Cells[startRow, i].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        ws.Cells[startRow, i].Style.Font.Size = 13;
                    }
                }

                ws.Cells["B3"].LoadFromCollection(fixedAssetExcelModels);

                pck.SaveAs(stream);

                stream.Position = 0;
            }

            var contentFile = stream.ToArray();

            return contentFile;
        }

        /// <summary>
        /// Filter tài sản
        /// </summary>
        /// <param name="FixedAssetCodeOrName">Mã hoặc tên tài sản</param>
        /// <param name="DepartmentName">Tên phòng ban</param>
        /// <param name="FixedAssetCategoryName">Tên loại tài sản</param>
        /// <returns>Danh sách tài sản</returns>
        /// Created By: Bùi Huy Tuyền (27/07/2023)
        public async Task<IEnumerable<FixedAssetEntity>> GetFixedAssetFilterAsync(string? FixedAssetCodeOrName, string? DepartmentName, string? FixedAssetCategoryName)
        {
            var procname = $"Proc_Get{TableNameProc}Filter";
            var parameters = new DynamicParameters();
            parameters.Add("FixedAssetCodeOrName", FixedAssetCodeOrName);
            parameters.Add("DepartmentName", DepartmentName);
            parameters.Add("FixedAssetCategoryName", FixedAssetCategoryName);

            var fixedAssetEntities = await _unitOfWork.Connection.QueryAsync<FixedAssetEntity>(procname, parameters, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);

            return fixedAssetEntities;
        }

        /// <summary>
        /// Tìm kiếm nhiều tài sản
        /// </summary>
        /// <param name="fixedAssetIds">Danh sách mã Id của tài sản</param>
        /// <returns>Danh sách tài sản</returns>
        /// Created By: Bùi Huy Tuyền (27/07/2023)
        public async Task<IEnumerable<FixedAssetEntity>> FindManyFixedAssetAsync(List<Guid> fixedAssetIds)
        {
            var query = $"SELECT * FROM fixed_asset WHERE {TableNameId} IN @FixedAssetId";

            var parameters = new DynamicParameters();

            parameters.Add("@FixedAssetId", fixedAssetIds.Select(x => x.ToString()));

            var fixedAssets = await _unitOfWork.Connection.QueryAsync<FixedAssetEntity>(query, parameters, commandType: CommandType.Text, transaction: _unitOfWork.Transaction);

            return fixedAssets;
        }

        /// <summary>
        /// Phân trang tài sản
        /// </summary>
        /// <param name="fixedAssetFilterModel">Điều kiện phân trang</param>
        /// <returns>Danh sách tài sản</returns>
        /// Created By: Bùi Huy Tuyền (27/07/2023)
        public async Task<FixedAssetPagingModel> GetFixedAssetPagingAsync(FixedAssetFilterModel fixedAssetFilterModel)
        {
            var procedureName = $"Proc_Get{TableNameProc}Paging";

            var parameters = new DynamicParameters();
            parameters.Add("FixedAssetCodeOrName", fixedAssetFilterModel.FixedAssetCodeOrName);
            parameters.Add("DepartmentName", fixedAssetFilterModel.DepartmentName);
            parameters.Add("FixedAssetCategoryName", fixedAssetFilterModel.FixedAssetCategoryName);
            parameters.Add("PageLimit", fixedAssetFilterModel.PageLimit);
            parameters.Add("PageNumber", fixedAssetFilterModel.PageNumber);

            parameters.Add("FixedAssetTotal", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var fixedAssets = await _unitOfWork.Connection.QueryAsync<FixedAssetEntity>(procedureName, parameters, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);

            var fixedAssetTotal = parameters.Get<int>("FixedAssetTotal");

            var fixedAssetPagingModel = new FixedAssetPagingModel
            {
                FixedAssets = fixedAssets,
                FixedAssetTotal = fixedAssetTotal
            };

            return fixedAssetPagingModel;
        }

        /// <summary>
        /// Xóa nhiều tài sản
        /// </summary>
        /// <param name="fixedAssets">Danh sách tài sản cần xóa</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (27/07/2023)
        public async Task DeleteManyAsync(IEnumerable<FixedAssetEntity> fixedAssets)
        {
            var query = $"DELETE FROM fixed_asset WHERE FixedAssetId IN @FixedAssetId;";

            var parameters = new DynamicParameters();

            parameters.Add("FixedAssetId", fixedAssets.Select(e => e.FixedAssetId.ToString()));

            await _unitOfWork.Connection.ExecuteAsync(query, parameters, commandType: CommandType.Text, transaction: _unitOfWork.Transaction);
        }

        /// <summary>
        /// Lấy danh sách tài sản cập nhật phòng ban mới nhất
        /// </summary>
        /// <param name="fixedAssetFilterModel"></param>
        /// <returns>Danh sách tài sản</returns>
        public async Task<FixedAssetPagingModel> GetFixedAssetTransferPagingAsync(FixedAssetFilterModel fixedAssetFilterModel)
        {
            var procedureName = $"Proc_Get{TableNameProc}TransferPaging";

            var parameters = new DynamicParameters();

            var fixedAssetIdIgnores = string.Join(",", fixedAssetFilterModel.FixedAssetIdIgnores);

            var transferAssetDetailIdIgnores = string.Join(",", fixedAssetFilterModel.TransferAssetDetailIdIgnores);

            parameters.Add("FixedAssetIdIgnores", fixedAssetIdIgnores);
            parameters.Add("TransferAssetDetailIdIgnores", transferAssetDetailIdIgnores);
            parameters.Add("PageLimit", fixedAssetFilterModel.PageLimit);
            parameters.Add("PageNumber", fixedAssetFilterModel.PageNumber);

            parameters.Add("FixedAssetTotal", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var fixedAssets = await _unitOfWork.Connection.QueryAsync<FixedAssetEntity>(procedureName, parameters, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);

            var fixedAssetTotal = parameters.Get<int>("FixedAssetTotal");

            var fixedAssetPagingModel = new FixedAssetPagingModel
            {
                FixedAssets = fixedAssets,
                FixedAssetTotal = fixedAssetTotal
            };

            return fixedAssetPagingModel;
        }
        #endregion
    }
}
