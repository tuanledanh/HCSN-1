const MISAResource = {
  VN: {
    AssetCodeInvalidError: {
      Empty: "Mã tài sản không được phép để trống",
    },
    FullNameInvalidError: {
      Empty: "Tên tài sản không được phép để trống",
    },

    Form: {
      Warning: {
        Cancel: {
          Add: "Bạn có muốn hủy bỏ khai báo tài sản này?",
          Update: "Bạn có muốn hủy cập nhật tài sản này?",
        },
        Edit: "Thông tin thay đổi sẽ không được cập nhật nếu bạn không lưu. Bạn có muốn lưu các thay đổi này",
        Delete: {
          Single: "Bạn có muốn xóa tài sản ",
          Multiple: " tài sản đã được chọn. Bạn có muốn xóa các tài sản này khỏi danh sách?"
        },
        Export: {
          Single: "Bạn có muốn xuất excel tài sản ",
          Multiple: " tài sản đã được chọn. Bạn có muốn xuất excel các tài sản này không?"
        },
      },
      Success: "Lưu dữ liệu thành công",
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
        PurchaseDate: "Ngày mua phải nhỏ hơn ngày bắt đầu sử dụng",
        StartUsingDate: "ngày bắt đầu sử dụng phải lớn hơn ngày mua",
      },
    },
  },
  EN: {
    AssetCodeInvalidError: {
      Empty: "The asset code cannot be left blank",
    },
    FullNameInvalidError: {
      Empty: "Asset names cannot be left blank",
    },
  },
};

export default MISAResource;
