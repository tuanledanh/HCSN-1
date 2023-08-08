using Application.DTO.FixedAssett;
using Application.Interface;
using AutoMapper;
using ClosedXML.Excel;
using Domain.Entity;
using Domain.Exceptions;
using Domain.Model;
using FastMember;
using MSIA.WebFresher052023.Application.Response.Base;
using MSIA.WebFresher052023.Application.Service.Base;
using MSIA.WebFresher052023.Domain.Interface;
using MSIA.WebFresher052023.Domain.Interface.Manager;
using MSIA.WebFresher052023.Domain.Interface.Repository;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;

namespace Application.Service
{
    public class FixedAssetService : BaseService<FixedAsset, FixedAssetModel, FixedAssetDto, FixedAssetCreateDto, FixedAssetUpdateDto>, IFixedAssetService
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
            int totalPages = Convert.ToInt32(Math.Ceiling((double)totalRecords / (double) pageLimit));

            entities = await _assetRepository.GetFilterSearchAsync(pageNumber, pageLimit, filterName, departmentIdString, assetTypeIdString);
            List<FixedAssetDto> entitiesDto = new List<FixedAssetDto>();
            foreach (var entity in entities)
            {
                entitiesDto.Add(_mapper.Map<FixedAssetDto>(entity));
            }

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
            await _fixedAssetManager.CheckDuplicateCode(fixedAssetCreateDto.FixedAssetCode);
            var existDepartment = await _departmentRepository.GetAsync(fixedAssetCreateDto.DepartmentId);
            var existAssetType = await _fixedAssetCategoryRepository.GetAsync(fixedAssetCreateDto.FixedAssetCategoryId);
            if (existDepartment == null || existAssetType == null)
            {
                // Để hàm check exist sang 1 class khác để tái sử dụng
                throw new NotFoundException("Không tìm thấy phòng ban hoặc tài sản");
            }
            var entity = _mapper.Map<FixedAsset>(fixedAssetCreateDto);
            entity.CreatedDate = DateTime.Now;
            entity.ModifiedDate = DateTime.Now;
            var id = Guid.NewGuid();
            entity.SetKey(id);
            bool result = await _baseRepository.InsertAsync(entity);
            if (!result)
            {
                throw new Exception("Service: Không thể thêm mới tài sản");
            }
            else
            {
                return result;
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
            await _fixedAssetManager.CheckDuplicateCode(fixedAssetUpdateDto.FixedAssetCode, oldAsset.FixedAssetCode);
            var existDepartment = await _departmentRepository.GetAsync(fixedAssetUpdateDto.DepartmentId);
            var existAssetType = await _fixedAssetCategoryRepository.GetAsync(fixedAssetUpdateDto.FixedAssetCategoryId);
            if (existDepartment == null || existAssetType == null)
            {
                // Để hàm check exist sang 1 class khác để tái sử dụng
                throw new NotFoundException("Không tìm thấy phòng ban hoặc tài sản");
            }
            var entity = _mapper.Map<FixedAsset>(fixedAssetUpdateDto);
            entity.FixedAssetId = id;
            entity.ModifiedDate = DateTime.Now;
            bool result = await _baseRepository.UpdateAsync(entity);
            if (!result)
            {
                throw new Exception("Service: Không thể cập nhật tài sản");
            }
            else
            {
                return result;
            }
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

            //List<FixedAssetDto> fixedAssetDtos = new List<FixedAssetDto>();
            //fixedAssetDtos.Add(new FixedAssetDto
            //{
            //    FixedAssetId= Guid.Parse("aab92299-30f0-11ee-b14c-f875a4da458a"),
            //      FixedAssetCode= "FA00052",
            //      FixedAssetName= "oijiohjpoij[ ouig iuhphouh oph 09 i90u 98 ",
            //     DepartmentId= Guid.Parse("4e272fc4-7875-78d6-7d32-6a1673ffca7c"),
            //      DepartmentCode= "DP0002",
            //      DepartmentName= "Phòng ban Nghiên cứu và phát triển",
            //      FixedAssetCategoryId= Guid.Parse("697be7ba-1738-6adf-298f-4d82f3a13455"),
            //      FixedAssetCategoryCode= "FAC0007",
            //      FixedAssetCategoryName= "Công cụ và dụng cụ thủ công",
            //      PurchaseDate= DateTime.Now,
            //      StartUsingDate= DateTime.Now,
            //      Cost= 9098,
            //      Quantity= 890809,
            //      TrackedYear= 2023,
            //      LifeTime= 13,
            //      ProductionYear= 0
            //});
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
