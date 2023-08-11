export default {
    VN: {
        noResult: 'Không tìm thấy kết quả phù hợp',
        deleteWarning: {
            deleteOne: 'Bạn có muốn xóa tài sản',
            deleteMany: 'tài sản đã được chọn. Bạn có muốn xóa các tài sản này không?',
            noDeleteOne: 'Không thể xóa tài sản này vì đã có chứng từ phát sinh.',
            noDeleteMany:
                'tài sản được chọn không thể xóa. Vui lòng kiểm tra lại tài sản trước khi thực hiện xóa.',
            deleteBlock: 'Bạn chưa chọn tài sản nào để xóa dữ liệu.'
        },
        exportWarning: {
            exportOne: 'Bạn có muốn xuất tài sản',
            exportMany:
                'tài sản đã được chọn. Bạn có muốn xuất các tài sản này ra file excel không?',
            exportBlock: 'Bạn chưa chọn tài sản nào để xuất dữ liệu.',
            exportAllLeft: 'Bạn có muốn xuất tất cả ',
            exportAllRight: ' tài sản ra file excel không?'
        },
        warningCancel: {
            noUpdate: 'Bạn có muốn hủy bỏ cập nhật tài sản này?',
            noCreate: 'Bạn có muốn hủy bỏ khai báo tài sản này?',
            edit: 'Thông tin thay đổi sẽ không được cập nhật nếu bạn không lưu. Bạn có muốn lưu các thay đổi này?'
        },
        validate: {
            message: 'Cần phải nhập thông tin ',
            DepartmentCode: 'Mã bộ phận không tồn tại.',
            FixedAssetCategoryCode: 'Mã loại tài sản không tồn tại.',
            DepreciationRateLifeTime: 'Tỷ lệ hao mòn phải bằng 1/Số năm sử dụng.',
            YearDepreciationCost: 'Hao mòn năm phải nhỏ hơn hoặc bằng nguyên giá.',
            UsingStartedDatePurchaseDate: 'Ngày bắt đầu sử dụng phải lớn hơn ngày mua.'
        },
        success: {
            update: 'Lưu dữ liệu thành công',
            create: 'Thêm dữ liệu thành công',
            replicate: 'Nhân bản dữ liệu thành công',
            delete: 'Xóa dữ liệu thành công'
        },
        error: {
            update: 'Lưu dữ liệu không thành công',
            create: 'Thêm dữ liệu không thành công',
            replicate: 'Nhân bản dữ liệu không thành công',
            loadData: 'Tải dữ liệu không thành công'
        },

        labelForm: {
            FixedAssetCode: 'Mã tài sản',
            FixedAssetName: 'Tên tài sản',
            DepartmentCode: 'Mã bộ phận sử dụng',
            DepartmentName: 'Tên bộ phận sử dụng',
            FixedAssetCategoryCode: 'Mã loại tài sản',
            FixedAssetCategoryName: 'Tên loại tài sản',
            Quantity: 'Số lượng',
            Cost: 'Nguyên giá',
            LifeTime: 'Số năm sử dụng',
            DepreciationRate: 'Tỷ lệ hao mòn (%)',
            YearDepreciation: 'Giá trị hao mòn năm',
            TrackedYear: 'Năm theo dõi',
            PurchaseDate: 'Ngày mua',
            UsingStartedDate: 'Ngày bắt đầu sử dụng'
        },
        placeholderForm: {
            FixedAssetCode: 'Nhập mã tài sản',
            FixedAssetName: 'Nhập tên tài sản',
            DepartmentCode: 'Chọn mã bộ phận sử dụng',
            FixedAssetCategoryCode: 'Chọn mã loại tài sản',
            time: 'dd/mm/yyyy'
        },
        fileNameExcel: 'Danh_sach_tai_san.xlsx',
        formTitle: {
            create: 'Thêm tài sản',
            update: 'Cập nhật tài sản',
            replicate: 'Nhân bản tài sản'
        },
        btnTitleForm: {
            create: 'Thêm',
            update: 'Lưu',
            replicate: 'Thêm',
            cancel: 'Hủy bỏ'
        },
        btnTitleToast: {
            cancel: 'Hủy bỏ',
            delete: 'Xóa',
            export: 'Xuất',
            dontSave: 'Không lưu'
        },
        btnDeleteActive: {
            delete: 'Xóa',
            cancel: 'Không',
            close: 'Đóng',
            agree: 'Đồng ý'
        },
        btnTitle: {
            create: 'Thêm tài sản'
        },
        tableHeaderTitle: {
            STT: 'STT',
            FixedAssetCode: 'Mã tài sản',
            FixedAssetName: 'Tên tài sản',
            DepartmentName: 'Tên bộ phận sử dụng',
            FixedAssetCategoryName: 'Tên loại tài sản',
            Quantity: 'Số lượng',
            Cost: 'Nguyên giá',
            cumulative: 'HM/KH lũy kế',
            remaining: 'Giá trị còn lại',
            option: 'Chức năng'
        }
    }
}
