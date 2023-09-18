import axios from "axios";

// Url chung
const baseURL = "https://localhost:7225/api/v1/";

// Tên url của tài sản
const baseAssetApi = baseURL + "FixedAsset";
// Tạo axios cho tài sản
const baseAssetAxios = axios.create({
  baseURL: baseAssetApi,
});

// Tên url của loại tài sản
const baseFixedAssetCategoryApi = baseURL + "FixedAssetCategory";
// Tạo axios cho loại tài sản
const baseFixedAssetCategoryAxios = axios.create({
  baseURL: baseFixedAssetCategoryApi,
});

// Tên url của phòng ban
const baseDepartmentApi = baseURL + "Department";
// Tạo axios cho phòng ban
const baseDepartmentAxios = axios.create({
  baseURL: baseDepartmentApi,
});

// Tên url của chứng từ điều chuyển tài sản
const baseTransferAssetApi = baseURL + "TransferAsset";
// Tạo axios cho chứng từ điều chuyển tài sản
const baseTransferAssetAxios = axios.create({
  baseURL: baseTransferAssetApi,
});

// Tên url của ban giao nhận
const baseReceiverApi = baseURL + "Receiver";
// Tạo axios cho ban giao nhận
const baseReceiverAxios = axios.create({
  baseURL: baseReceiverApi,
});

// Các api của tài sản
const MISAApi = {
  FixedAsset: {
    /**
     * Api filter, lọc, phân trang
     * @param {Int16Array} pageNumber số trang đang ở
     * @param {Int16Array} pageLimit số bản ghi tối đa mỗi trang
     * @param {string} filterName mã code để tìm kiếm
     * @param {string} departId id của phòng ban
     * @param {string} aTypeId id của loại tài sản
     * @returns
     */
    Filter: (pageNumber, pageLimit, filterName, departId, aTypeId) =>
      baseAssetAxios.get("", {
        params: {
          pageNumber,
          pageLimit,
          filterName,
          departId,
          aTypeId,
        },
      }),
    // Api chọn tài sản theo phòng ban mới nhất từ chứng từ và bỏ những tài sản đã được chọn
    FilterForTransfer: (data) =>
      baseAssetAxios.post("/FilterForTransfer", data),
    // Api của tài sản
    Api: baseAssetApi,
    // Api lấy mã code mới
    GetNewCode: () => baseAssetAxios.get("/GetNewCode"),
    // Api đếm tổng số bản ghi
    GetCount: () => baseAssetAxios.get("/Count"),
    // Api lấy tất cả bản ghi
    GetAll: () => baseAssetAxios.get(""),
    // Api tạo bản ghi mới
    Create: (assetData) => baseAssetAxios.post("", assetData),
    CheckTransfer: (assetIds, action) => baseAssetAxios.post("/CheckTransfer?action=" + action, assetIds),
    // Api lấy bản ghi bằng mã code
    GetByCode: (code) => baseAssetAxios.get(`/${code}`),
    // Api cập nhật
    Update: (id, assetData) => baseAssetAxios.put(`/${id}`, assetData),
    // Api xóa nhiều
    DeleteMany: (listAssetData) =>
      baseAssetAxios.delete("", { data: listAssetData }),
    // Api xuất excel
    Export: (listAssetData, listId) =>
      baseAssetAxios.get(`export?idsQuery=${listId}`, listAssetData),
  },
  FixedAssetCategory: {
    // Api của loại tài sản
    Api: baseFixedAssetCategoryApi,
    // Api lấy tất cả bản ghi
    GetAll: () => baseFixedAssetCategoryAxios.get(""),
  },
  Department: {
    // Api của phòng ban
    Api: baseDepartmentApi,
    // Api lấy tất cả bản ghi
    GetAll: () => baseDepartmentAxios.get(""),
  },
  Receiver: {
    // Lấy các phòng ban mới nhất
    GetNewest: () => baseReceiverAxios.get("/getNewest"),
  },
  TransferAsset: {
    Api: baseTransferAssetApi,
    // Api lấy mã code mới
    GetNewCode: () => baseTransferAssetAxios.get("/GetNewCode"),
    GetNewest: (transferAssetId) =>
      baseTransferAssetAxios.post(`/GetNewest/${transferAssetId}`),
    /**
     * Api filter, lọc, phân trang
     * @param {Int16Array} pageNumber số trang đang ở
     * @param {Int16Array} pageLimit số bản ghi tối đa mỗi trang
     * @param {string} filterName mã code để tìm kiếm
     * @returns
     */
    Filter: (pageNumber, pageLimit, filterName) =>
      baseTransferAssetAxios.get("", {
        params: {
          pageNumber,
          pageLimit,
          filterName,
        },
      }),
    // Api tạo bản ghi mới
    Create: (transferAssetData) =>
      baseTransferAssetAxios.post("", transferAssetData),
    // Api cập nhật
    Update: (id, transferAssetData) =>
      baseTransferAssetAxios.put(`/${id}`, transferAssetData),
    // Api xóa nhiều
    DeleteMany: (listTransferAssetData) =>
      baseTransferAssetAxios.delete("", { data: listTransferAssetData }),
    // Api lấy bản ghi bằng mã code
    GetByCode: (code) => baseTransferAssetAxios.get(`/${code}`),
  },
};

export default MISAApi;
