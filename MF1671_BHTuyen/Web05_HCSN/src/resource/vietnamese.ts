import type { ResourceData } from '@/types'

const VN: ResourceData = {
    sidebar: {
        header: 'MISA QLTS',
        overview: 'Tổng quan',
        fixed_asset: 'Tài sản',
        infrastructure_asset: 'Tài sản HT-ĐB',
        infrastructure_asset_tooltip: 'Tài sản hạ tầng đường bộ',
        tool: 'Công cụ dụng cụ',
        category: 'Danh mục',
        search: 'Tra cứu',
        report: 'Báo cáo',
        fixed_asset_list: {
            fixed_asset_increase: 'Ghi tăng',
            information_change: 'Thay đổi thông tin',
            fixed_asset_reassessment: 'Đánh giá lại',
            depreciation_fixed_asset: 'Tính hao mòn',
            asset_transfer: 'Điều chuyển tài sản',
            reducing_asset: 'Ghi giảm',
            fixed_asset_inventory: 'Kiểm kê',
            other: 'Khác'
        },
        collapse: 'Thu gọn',
        expand: 'Mở rộng'
    },
    main: {
        header: {
            title: 'Danh sách tài sản',
            organization: 'Sở tài chính',
            year: 'Năm'
        }
    },

    fixed_asset: {
        fixed_asset_search: 'Tìm kiếm tài sản',
        fixed_asset_category_name: 'Tên loại tài sản',
        department_name: 'Tên bộ phận sử dụng',
        data_update: 'Cập nhật dữ liệu',
        fixed_asset_add: 'Thêm tài sản',
        fixed_asset_code: 'Mã tài sản',
        fixed_asset_name: 'Tên tài sản',
        quantity: 'Số lượng',
        cost: 'Nguyên giá',
        accumulation_depreciation: 'HM/KH lũy kế',
        remainder_cost: 'Giá trị còn lại',
        functional: 'Chức năng',
        success_update: 'Cập nhật tài sản thành công',
        success_create: 'Thêm mới tài sản thành công',
        success_delete: 'Xóa tài sản thành công',
        success_replicate: 'Nhân bản tài sản thành công',
        success_update_data: 'Cập nhật dữ liệu thành công'
    },

    fixed_asset_popup: {
        heading: {
            update: 'Sửa tài sản',
            create: 'Thêm tài sản',
            replicate: 'Nhân bản tài sản'
        },
        fixed_asset_category_name: 'Tên loại tài sản',
        fixed_asset_category_code: 'Mã loại tài sản',
        fixed_asset_category_code_choose: 'Chọn mã loại tài sản',
        department_name: 'Tên bộ phận sử dụng',
        department_code: 'Mã bộ phận sử dụng',
        department_code_choose: 'Chọn mã bộ phận sử dụng',
        fixed_asset_code: 'Mã tài sản',
        fixed_asset_code_input: 'Mã tài sản',
        fixed_asset_name: 'Tên tài sản',
        fixed_asset_name_input: 'Tên tài sản',
        quantity: 'Số lượng',
        cost: 'Nguyên giá',
        tracked_year: 'Năm theo dõi',
        depreciation_rate: 'Tỷ lệ hao mòn (%)',
        year_depreciation: 'Giá trị hao mòn năm',
        purchase_date: 'Ngày mua',
        using_started_date: 'Ngày bắt đầu sử dụng',
        life_time: 'Số năm sử dụng'
    },

    asset_transfer: {
        header: {
            title: 'Điều chuyển',
            button_title: 'Thêm chứng từ'
        },

        transfer_asset_code: 'Mã chứng từ',
        transaction_date: 'Ngày chứng từ',
        transfer_date: 'Ngày điều chuyển',
        cost: 'Nguyên giá',
        remainder_cost: 'Giá trị còn lại',
        note: 'Ghi chú',
        functional: 'Chức năng',
        caption: 'Thông tin chi tiết',
        fixed_asset_code: 'Mã tài sản',
        fixed_asset_name: 'Tên tài sản',
        department_name: 'Bộ phận sử dụng',
        transfer_department_name: 'Bộ phận điều chuyển',
        reason: 'Lý do',
        collapse: 'Thu gọn',
        expand: 'Mở rộng',
        success_update: 'Cập nhật chứng từ thành công',
        success_create: 'Tạo mới chứng từ thành công',
        success_delete: 'Xóa chứng từ thành công',
        success_update_data: 'Cập nhật dữ liệu thành công',
        empty_transfer_asset: 'Không có chứng từ điều chuyển',
        empty_transfer_asset_detail: 'Không có tài sản điều chuyển',
        message_delete: 'Bạn có muốn xóa chứng từ điều chuyển',
        no: 'không',
        message_delete_many:
            'chứng từ đã được chọn. Bạn có muốn xóa các chứng từ này khỏi danh sách không'
    },
    transfer_asset_info: {
        show_more_info: 'Hiển chi tiết chứng từ',
        hidden_more_info: 'Ẩn chi tiết chứng từ'
    },

    department_receive: {
        index: 'STT',
        fullname: 'Họ và tên',
        delegate: 'Đại diện',
        position: 'Chức vụ',
        fullname_input: 'Nhập họ và tên',
        delegate_input: 'Nhập Đại diện',
        position_input: 'Nhập Chức vụ'
    },
    asset_transfer_popup_detail: {
        heading: {
            create: 'Thêm chứng từ điều chuyển',
            update: 'Sửa chứng từ điều chuyển'
        },
        general_information: 'Thông tin chung',
        transfer_asset_code: 'Mã chứng từ',
        transaction_date: 'Ngày chứng từ',
        transfer_date: 'Ngày điều chuyển',
        note: 'Ghi chú',
        choose_receive_department: 'Chọn ban giao nhận',
        choose_fixed_asset: 'Chọn tài sản',
        add_receive_department_recent: 'Thêm ban giao nhận từ lần nhập trước',
        fixed_asset_transfer_information: 'Thông tin tài sản điều chuyển',
        search_fixed_asset: 'Tìm kiếm tài sản',
        fixed_asset_code: 'Mã tài sản',
        fixed_asset_name: 'Tên tài sản',
        cost: 'Nguyên giá',
        remainder_cost: 'Giá trị còn lại',
        department_name: 'Bộ phận sử dụng',
        transfer_department_name: 'Bộ phận điều chuyển',
        reason: 'Lý do',
        functional: 'Chức năng',
        list_detail_empty: 'Bạn chưa chọn tài sản điều chuyển nào'
    },

    asset_transfer_popup_choose: {
        heading: 'Chọn tài sản điều chuyển',
        fixed_asset_code: 'Mã tài sản',
        fixed_asset_name: 'Tên tài sản',
        department_name: 'Bộ phận sử dụng',
        cost: 'Nguyên giá',
        remainder_cost: 'Giá trị còn lại',
        tracked_year: 'Năm theo dõi',
        transfer_asset_name: 'Bộ phận sử dụng mới',
        transfer_asset_name_input: 'Chọn bộ phận sử dụng mới',
        note_input: 'Nhập ghi chú',
        note: 'Ghi chú',
        transfer_asset_no_more: 'Không còn tài sản nào để điều chuyển.',
        transfer_asset_no_choose: 'Bạn chưa chọn tài sản điều chuyển.',
        choose_transfer_department: 'Vui lòng chọn bộ phận sử dụng mới.',
        transfer_department_check:
            'Vui lòng chọn bộ phận sử dụng mới khác bộ phận sử dụng tài sản.',
        cancel: 'Hủy bỏ',
        no: 'Không',
        message_cancel: 'Bạn có muốn hủy bỏ thêm tài sản điều chuyển không?'
    },
    popup: {
        title_button_cancel: 'Hủy',
        title_button_save: 'Lưu',
        tootip_close: 'Đóng'
    },
    pagination: {
        total: 'Tổng số',
        item: 'bản ghi',
        limit: 'Số dòng/trang',
        page: 'Trang',
        page_first: 'Trang đầu',
        page_last: 'Trang cuối',
        page_next: 'Trang sau',
        page_prev: 'Trang trước'
    },
    table_column: {
        index: 'STT'
    },

    combobox: {
        not_found_data: 'Không tìm thấy dữ liệu'
    },
    select_info: {
        selected: 'Đã chọn:',
        unselected: 'Bỏ chọn',
        to_delete: 'Xóa'
    },
    toast_message: {
        cancel: 'Hủy bỏ',
        confirm: 'Xác nhận',
        delete: 'Xóa',
        not_save: 'Không lưu'
    }
}

export default VN
