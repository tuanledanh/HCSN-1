const MISAResource = {
  // Tiếng việt
  VN: {
    // Mã tài sản bị lỗi
    AssetCodeInvalidError: {
      Empty: "Mã tài sản không được phép để trống",
    },
    // Tên tài sản bị lỗi
    FullNameInvalidError: {
      Empty: "Tên tài sản không được phép để trống",
    },
    // Thông báo liên quan đến form
    Form: {
      // Cảnh báo
      Warning: {
        // Cảnh báo khi thoát form
        Cancel: {
          // Trường hợp thêm
          Add: "Bạn có muốn hủy bỏ khai báo tài sản này?",
          // Trường hợp cập nhật
          Update: "Bạn có muốn hủy cập nhật tài sản này?",
        },
        // Thông báo nếu người dùng thay đổi trong ô nhập liệu nào đó
        Edit: "Thông tin thay đổi sẽ không được cập nhật nếu bạn không lưu. Bạn có muốn lưu các thay đổi này",
        // Trường hợp xóa
        Delete: {
          // Xóa 1 bản ghi
          Single: "Bạn có muốn xóa tài sản ",
          // Xóa nhiều bản ghi
          Multiple: " tài sản đã được chọn. Bạn có muốn xóa các tài sản này khỏi danh sách?",
          // Nếu không chọn bản ghi nào thì hiện thông báo
          Zero: "Hãy chọn ít nhất một tài sản để xóa!",
        },
        // Xuất excel
        Export: {
          // Xuất 1 bản ghi
          Single: "Bạn có muốn xuất excel tài sản ",
          // Xuất nhiều bản ghi
          Multiple: " tài sản đã được chọn. Bạn có muốn xuất excel các tài sản này không?"
        },
      },
      // Lưu thành công
      Success: "Lưu dữ liệu thành công",
      // Kiểm trả dữ liệu nhập
      Validate: {
        FixedAssetCode: "mã tài sản",
        FixedAssetName: "tên tài sản",
        DepartmentId: "mã bộ phận sử dụng",
        FixedAssetCategoryId: "mã loại tài sản",
        Quantity: "số lượng tài sản",
        Cost: "nguyên giá tài sản",
        LifeTime: "số năm sử dụng",
        AssetDepreciation: "tỉ lệ hao mòn",
        YearlyDepreciationAmount: "Giá trị hao mòn năm",
        CompareDate: "Ngày mua phải nhỏ hơn ngày bắt đầu sử dụng",
        PurchaseDate: "ngày mua",
        StartUsingDate: "ngày bắt đầu sử dụng",
      },
      // Giới hạn ký tự trong ô nhập liệu
      Max_value:{
        FixedAssetCode: "Mã tài sản không được vượt quá 50 ký tự",
        FixedAssetName: "Tên tài sản không được vượt quá 255 ký tự",
        Quantity: "Số lượng tài sản lớn hơn giới hạn cho phép",
        Cost: "Nguyên giá tài sản lớn hơn giới hạn cho phép",
      },
    },
    // Các hành động của context menu, khi click 1 option thì contextMenu sẽ bắn index về cho component cha
    // Component cha nhận index và tìm event tương ứng để thực hiện
    ContextMenu:{
      Add:{
        icon: "icon-add-black",
        text: "Thêm mới",
        index: 1,
      },
      Update:{
        icon: "icon-edit",
        text: "Cập nhật",
        index: 2,
      },
      Clone:{
        icon: "icon-copy",
        text: "Sao chép",
        index: 3,
      },
      Delete:{
        icon: "icon-delete-black",
        text: "Xóa",
        index: 4,
      },
    },
  },
  // Tiếng anh
  EN: {
    // Mã tài sản lỗi
    AssetCodeInvalidError: {
      Empty: "The asset code cannot be left blank",
    },
    // Tên tài sản lỗi
    FullNameInvalidError: {
      Empty: "Asset names cannot be left blank",
    },
  },
};

export default MISAResource;
