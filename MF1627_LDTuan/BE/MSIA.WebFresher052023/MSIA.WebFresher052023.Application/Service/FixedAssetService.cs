using Application.DTO;
using Application.Interface;
using AutoMapper;
using Domain.Entity;
using Domain.Exceptions;
using Domain.Model;
using FastMember;
using MSIA.WebFresher052023.Application.Response.Base;
using MSIA.WebFresher052023.Application.Service.Base;
using MSIA.WebFresher052023.Domain.Enum;
using MSIA.WebFresher052023.Domain.Interface;
using MSIA.WebFresher052023.Domain.Interface.Manager;
using MSIA.WebFresher052023.Domain.Interface.Repository;
using MSIA.WebFresher052023.Domain.Result;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;

namespace Application.Service
{
    public class FixedAssetService : BaseService<FixedAsset, FixedAssetModel, FixedAssetDto, FixedAssetCreateDto, FixedAssetUpdateDto, FixedAssetUpdateMultiDto>, IFixedAssetService
    {
        #region Fields
        private readonly IFixedAssetRepository _assetRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IFixedAssetCategoryRepository _fixedAssetCategoryRepository;
        private readonly IFixedAssetManager _fixedAssetManager;
        #endregion

        #region Constructor
        public FixedAssetService(IFixedAssetRepository assetRepository, IMapper mapper, IDepartmentRepository departmentRepository, IFixedAssetCategoryRepository fixedAssetCategoryRepository, IFixedAssetManager fixedAssetManager, IUnitOfWork unitOfWork) : base(assetRepository, mapper, fixedAssetManager, unitOfWork)
        {
            _assetRepository = assetRepository;
            _departmentRepository = departmentRepository;
            _fixedAssetCategoryRepository = fixedAssetCategoryRepository;
            _fixedAssetManager = fixedAssetManager;
        }
        #endregion


        #region Methods
        /// <summary>
        /// Lấy danh sách các bản ghi, có phân trang, tìm kiếm theo mã code, lọc theo phòng ban và loại tài sản
        /// </summary>
        /// <param name="pageNumber">Số trang</param>
        /// <param name="pageLimit">Số lượng tối đa bản ghi mỗi trang</param>
        /// <param name="filterName">Mã code để tìm kiếm</param>
        /// <param name="departId">Id của phòng ban dùng để lọc</param>
        /// <param name="aTypeId">Id của loại tài sản dùng để lọc</param>
        /// <returns>Danh sách tài sản đáp ứng đúng các điều kiện</returns>
        /// Created by: ldtuan (23/07/2023)
        public async Task<BaseFilterResponse<FixedAssetDto>> GetAllCustomAsync(int? pageNumber, int? pageLimit, string filterName, string? departId, string? aTypeId)
        {
            List<FixedAssetModel> entities;
            pageNumber = pageNumber.HasValue ? pageNumber : 1;
            pageLimit = pageLimit.HasValue ? pageLimit : 20;
            filterName = string.IsNullOrEmpty(filterName) ? "" : filterName;
            string departmentIdString = Guid.TryParse(departId, out Guid departmentId) ? departId : string.Empty;
            string assetTypeIdString = Guid.TryParse(aTypeId, out Guid assetTypeId) ? aTypeId : string.Empty;

            int totalRecords = await _assetRepository.GetCountFilterAsync(filterName, departmentIdString, assetTypeIdString);
            int totalPages = Convert.ToInt32(Math.Ceiling((double)totalRecords / (double)pageLimit));

            entities = await _assetRepository.GetFilterSearchAsync(pageNumber, pageLimit, filterName, departmentIdString, assetTypeIdString);
            List<FixedAssetDto> entitiesDto = _mapper.Map<List<FixedAssetDto>>(entities);
            var filterData = new BaseFilterResponse<FixedAssetDto>(totalPages, totalRecords, entitiesDto);
            return filterData;
        }

        /// <summary>
        /// Lấy danh sách tài sản có loại những bản ghi đã chọn để hiện thị trên form chọn tài sản cho chứng từ
        /// </summary>
        /// <param name="pageNumber">Số trang</param>
        /// <param name="pageLimit">Số lượng tối đa bản ghi mỗi trang</param>
        /// <param name="dtos">Danh sách truyền vào để loại những bản ghi đó ra</param>
        /// <returns>Danh sách loại tài sản đáp ứng đúng các điều kiện trên</returns>
        /// Created by: ldtuan (05/09/2023)
        public async Task<BaseFilterResponse<FixedAssetDto>> FilterFixedAssetForTransfer(int? pageNumber, int? pageLimit, string filterName, List<FixedAssetDto> dtos, List<Guid> transferAssetDetailIds)
        {
            string ids = "";
            if (dtos != null && dtos.Count > 0)
            {
                ids = string.Join(",", dtos.Select(asset => asset.FixedAssetId));
            }

            string detailIds = "";
            if (transferAssetDetailIds != null && transferAssetDetailIds.Count > 0)
            {
                detailIds = string.Join(",", transferAssetDetailIds);
            }

            List<FixedAssetModel> entities;
            pageNumber = pageNumber.HasValue && pageNumber.Value > 0 ? pageNumber : 1;
            pageLimit = pageLimit.HasValue && pageLimit.Value > 0 ? pageLimit : 20;
            filterName = string.IsNullOrEmpty(filterName) ? "" : filterName;

            var model = await _assetRepository.FilterFixedAssetForTransfer(pageNumber, pageLimit, filterName, ids, detailIds);
            int totalRecords = model.TotalRecords;
            int totalPages = Convert.ToInt32(Math.Ceiling((double)totalRecords / (double)pageLimit));
            entities = model.FixedAssetModels;

            List <FixedAssetDto> entitiesDto = _mapper.Map<List<FixedAssetDto>>(entities);
            var filterData = new BaseFilterResponse<FixedAssetDto>(totalPages, totalRecords, entitiesDto);

            return filterData;
        }

        /// <summary>
        /// Gọi đến hàm InsertAsync ở repository để xử lý thêm mới một bản ghi
        /// </summary>
        /// <param name="assetCreateDto">Thông tin của bản ghi</param>
        /// <returns>True nếu thêm mới thành công, false nếu thêm mới thất bại</returns>
        /// Created by: ldtuan (17/07/2023)
        public override async Task<bool> InsertAsync(FixedAssetCreateDto fixedAssetCreateDto)
        {
            await _fixedAssetManager.CheckDuplicateCodeAsync(fixedAssetCreateDto.FixedAssetCode);
            var existDepartment = await _departmentRepository.GetAsync(fixedAssetCreateDto.DepartmentId);
            var existAssetType = await _fixedAssetCategoryRepository.GetAsync(fixedAssetCreateDto.FixedAssetCategoryId);
            if (existDepartment == null || existAssetType == null)
            {
                // Để hàm check exist sang 1 class khác để tái sử dụng
                throw new NotFoundException();
            }
            var entity = _mapper.Map<FixedAsset>(fixedAssetCreateDto);
            entity.CreatedDate = DateTime.Now;
            entity.ModifiedDate = DateTime.Now;
            var id = Guid.NewGuid();
            entity.SetKey(id);

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _baseRepository.InsertAsync(entity);

                await _unitOfWork.CommitAsync();
                return true;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw new Exception();
            }
        }

        /// <summary>
        /// Thêm mới nhiều tài sản cùng lúc
        /// </summary>
        /// <param name="fixedAssetCreateDtos">Danh sách nội dung cần thêm mới</param>
        /// Created by: ldtuan (02/09/2023)
        public override async Task<ApiResponse> InsertMultiAsync(List<FixedAssetCreateDto> fixedAssetCreateDtos)
        {
            // Sửa chỗ check mã trùng của 1 list, viết sql ném vào where code in @listCode
            foreach (var fixedAssetCreateDto in fixedAssetCreateDtos)
            {
                await _fixedAssetManager.CheckDuplicateCodeAsync(fixedAssetCreateDto.FixedAssetCode);
                var existDepartment = await _departmentRepository.GetAsync(fixedAssetCreateDto.DepartmentId);
                var existAssetType = await _fixedAssetCategoryRepository.GetAsync(fixedAssetCreateDto.FixedAssetCategoryId);
                if (existDepartment == null || existAssetType == null)
                {
                    throw new NotFoundException();
                }
            }

            var fixedAssetsRaw = _mapper.Map<List<FixedAsset>>(fixedAssetCreateDtos);
            var fixedAssets = new List<FixedAsset>();
            foreach (var fixedAsset in fixedAssetsRaw)
            {
                fixedAsset.CreatedDate = DateTime.Now;
                fixedAsset.ModifiedDate = DateTime.Now;
                var id = Guid.NewGuid();
                fixedAsset.SetKey(id);

                fixedAssets.Add(fixedAsset);
            }

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _baseRepository.InsertMultiAsync(fixedAssets);

                await _unitOfWork.CommitAsync();
                return new ApiResponse { UserMessage = "Success" };
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw new Exception();
            }
        }

        /// <summary>
        /// Gọi đến hàm UpdateAsync ở repository để xử lý cập nhật một bản ghi
        /// </summary>
        /// <param name="entityCreateDto">Thông tin của bản ghi</param>
        /// <returns>True nếu cập nhật thành công, false nếu cập nhật thất bại</returns>
        /// Created by: ldtuan (17/07/2023)
        public override async Task<bool> UpdateAsync(Guid id, FixedAssetUpdateDto fixedAssetUpdateDto)
        {
            var oldAsset = await _baseRepository.GetAsync(id);
            if (oldAsset == null) return false;
            await _fixedAssetManager.CheckDuplicateCodeAsync(fixedAssetUpdateDto.FixedAssetCode, oldAsset.FixedAssetCode);
            var existDepartment = await _departmentRepository.GetAsync(fixedAssetUpdateDto.DepartmentId);
            var existAssetType = await _fixedAssetCategoryRepository.GetAsync(fixedAssetUpdateDto.FixedAssetCategoryId);
            if (existDepartment == null || existAssetType == null)
            {
                // Để hàm check exist sang 1 class khác để tái sử dụng
                throw new NotFoundException();
            }
            var entity = _mapper.Map<FixedAsset>(fixedAssetUpdateDto);
            entity.FixedAssetId = id;
            entity.ModifiedDate = DateTime.Now;

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _baseRepository.UpdateAsync(entity);

                await _unitOfWork.CommitAsync();
                return true;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw new Exception();
            }
        }

        /// <summary>
        /// Cập nhật nhiều tài sản cùng lúc
        /// </summary>
        /// <param name="fixedAssetUpdateMultipleDtos">Danh sách thông tin cần cập nhật</param>
        /// Created by: ldtuan (02/09/2023)
        public override async Task<ApiResponse> UpdateMultiAsync(List<FixedAssetUpdateMultiDto> fixedAssetUpdateMultipleDtos)
        {
            var ids = fixedAssetUpdateMultipleDtos.Select(f => f.FixedAssetId).ToList();
            var fixedAssetUpdateDtos = _mapper.Map<List<FixedAssetUpdateDto>>(fixedAssetUpdateMultipleDtos);

            var oldAssets = await _baseRepository.GetListByIdsAsync(ids);
            if (ids.Count == 0 || oldAssets.Count != ids.Count)
            {
                throw new Domain.Exceptions.DataException();
            }
            var oldAssetModels = _mapper.Map<List<FixedAssetModel>>(oldAssets);
            var assetModels = _mapper.Map<List<FixedAssetModel>>(fixedAssetUpdateDtos);
            for (int i = 0; i < assetModels.Count; i++)
            {
                await _manager.CheckDuplicateCodeAsync(assetModels[i].FixedAssetCode, oldAssetModels[i].FixedAssetCode);
                var existDepartment = await _departmentRepository.GetAsync(assetModels[i].DepartmentId);
                var existAssetType = await _fixedAssetCategoryRepository.GetAsync(assetModels[i].FixedAssetCategoryId);
                if (existDepartment == null || existAssetType == null)
                {
                    throw new NotFoundException();
                }
            }
            var assets = _mapper.Map<List<FixedAsset>>(fixedAssetUpdateDtos);
            for (int i = 0; i < oldAssets.Count; i++)
            {
                assets[i].FixedAssetId = oldAssets[i].FixedAssetId;
                assets[i].ModifiedDate = DateTime.Now;
            }

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _baseRepository.UpdateMultiAsync(assets);

                await _unitOfWork.CommitAsync();
                return new ApiResponse { UserMessage = "Success" };
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw new Exception();
            }
        }

        /// <summary>
        /// Kiểm tra tài sản có phát sinh chứng từ không
        /// </summary>
        /// <param name="assetIds">Danh sách id tài sản</param>
        /// <param name="action">Hành động như xóa hay cập nhật</param>
        /// <returns>Ném ra 1 ngoại lệ nếu có chứng từ phát sinh</returns>
        /// Created by: ldtuan (17/09/2023)
        public async Task CheckExistTransferAsync(List<Guid> assetIds, ActionMode action)
        {
            await _fixedAssetManager.CheckExistFixedAssetAsync(assetIds);
            await _fixedAssetManager.CheckExistTransferAsync(assetIds, action);
        }

        /// <summary>
        /// Lấy dữ liệu tài sản dưới dạng dataTable
        /// </summary>
        /// <param name="idsQuery">Danh sách id các bản ghi</param>
        /// <returns>DataTable</returns>
        /// Created by: ldtuan (06/08/2023)
        public async Task<DataTable> ExportExcel(string idsQuery)
        {
            List<Guid> ids = idsQuery.Split(",").Select(x => Guid.Parse(x)).ToList();

            List<FixedAsset> fixedAssets = await _baseRepository.GetListByIdsAsync(ids);

            List<ExportFixedAsset> fixedAssetDtos = fixedAssets.Select(fa => _mapper.Map<ExportFixedAsset>(fa)).ToList();
            if (fixedAssetDtos != null && fixedAssetDtos.Count > 0)
            {
                var assetData = GetFixedAssetData(fixedAssetDtos);
                return assetData;
            }
            else
            {
                throw new NotFoundException();
            }
        }

        /// <summary>
        /// Chuyển đổi danh sách các đối tượng ExportFixedAsset thành cấu trúc dữ liệu dataTable để phục vụ cho việc xuất excel
        /// </summary>
        /// <param name="assetList">Danh sách bản ghi truyền vào</param>
        /// <returns>Chứa dữ liệu đã được chuyển đổi</returns>
        /// Created by: ldtuan (06/08/2023)
        private DataTable GetFixedAssetData(List<ExportFixedAsset> assetList)
        {
            var data = new DataTable();
            data.TableName = "Báo cáo tài sản";
            var properties = typeof(ExportFixedAsset).GetProperties();
            foreach (var property in properties)
            {
                string columnName = GetColumnName(property);
                data.Columns.Add(columnName, property.PropertyType);
            }

            if (assetList != null && assetList.Count > 0)
            {
                var accessor = TypeAccessor.Create(typeof(ExportFixedAsset));
                foreach (var item in assetList)
                {
                    var row = data.NewRow();
                    foreach (var property in properties)
                    {
                        string columnName = GetColumnName(property);
                        var value = accessor[item, property.Name];
                        row[columnName] = value;
                    }
                    data.Rows.Add(row);
                }
            }

            return data;
        }

        /// <summary>
        /// Lấy tên hiển thị của thuộc tính để gán cho tên cột trong excel
        /// </summary>
        /// <param name="property">Thuộc tính</param>
        /// <returns>Tên cột</returns>
        /// Created by: ldtuan (07/08/2023)
        private string GetColumnName(PropertyInfo property)
        {
            var displayAttribute = property.GetCustomAttributes(typeof(DisplayAttribute), false)
                                        .OfType<DisplayAttribute>()
                                        .FirstOrDefault();
            // Nếu displayAttribute != null thì nó lấy name của displayAttribute
            // Nếu displayAttribute.Name null thì lấy name của property
            string columnName = displayAttribute?.Name ?? property.Name;
            return columnName;
        }
        #endregion
    }
}
