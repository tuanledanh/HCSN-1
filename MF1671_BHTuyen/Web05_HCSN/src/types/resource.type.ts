export interface ResourceData {
    sidebar: {
        header: string
        overview: string
        fixed_asset: string
        infrastructure_asset: string
        infrastructure_asset_tooltip: string
        tool: string
        category: string
        search: string
        report: string
        fixed_asset_list: {
            fixed_asset_increase: string
            information_change: string
            fixed_asset_reassessment: string
            depreciation_fixed_asset: string
            asset_transfer: string
            reducing_asset: string
            fixed_asset_inventory: string
            other: string
        }
        expand: string
        collapse: string
    }
    main: {
        header: {
            title: string
            organization: string
            year: string
        }
    }

    fixed_asset: {
        fixed_asset_search: string
        fixed_asset_category_name: string
        department_name: string
        data_update: string
        fixed_asset_add: string
        fixed_asset_code: string
        fixed_asset_name: string
        quantity: string
        cost: string
        accumulation_depreciation: string
        remainder_cost: string
        functional: string
        success_update: string
        success_create: string
        success_delete: string
        success_replicate: string
        success_update_data: string
    }

    fixed_asset_popup: {
        heading: {
            update: string
            create: string
            replicate: string
        }
        fixed_asset_category_name: string
        fixed_asset_category_code: string
        fixed_asset_category_code_choose: string
        department_name: string
        department_code: string
        department_code_choose: string
        fixed_asset_name: string
        fixed_asset_name_input: string
        fixed_asset_code: string
        fixed_asset_code_input: string
        quantity: string
        cost: string
        tracked_year: string
        depreciation_rate: string
        year_depreciation: string
        purchase_date: string
        using_started_date: string
        life_time: string
    }
    asset_transfer: {
        header: {
            title: string
            button_title: string
        }
        transfer_asset_code: string
        transaction_date: string
        transfer_date: string
        note: string
        functional: string
        caption: string
        fixed_asset_code: string
        fixed_asset_name: string
        cost: string
        remainder_cost: string
        department_name: string
        transfer_department_name: string
        reason: string
        expand: string
        collapse: string
        success_update: string
        success_create: string
        success_delete: string
        success_update_data: string
        empty_transfer_asset: string
        empty_transfer_asset_detail: string
        message_delete: string
        no: string
        message_delete_many: string
    }

    transfer_asset_info: {
        show_more_info: string
        hidden_more_info: string
    }

    department_receive: {
        index: string
        fullname: string
        delegate: string
        position: string
        fullname_input: string
        delegate_input: string
        position_input: string
    }
    asset_transfer_popup_detail: {
        heading: {
            create: string
            update: string
        }
        general_information: string
        transfer_asset_code: string
        transaction_date: string
        transfer_date: string
        note: string
        choose_receive_department: string
        choose_fixed_asset: string
        add_receive_department_recent: string
        fixed_asset_transfer_information: string
        search_fixed_asset: string
        fixed_asset_code: string
        fixed_asset_name: string
        cost: string
        remainder_cost: string
        department_name: string
        transfer_department_name: string
        reason: string
        functional: string
        list_detail_empty: string
    }

    asset_transfer_popup_choose: {
        heading: string
        fixed_asset_code: string
        fixed_asset_name: string
        department_name: string
        cost: string
        remainder_cost: string
        tracked_year: string
        transfer_asset_name: string
        transfer_asset_name_input: string
        note_input: string
        note: string
        transfer_asset_no_more: string
        transfer_asset_no_choose: string
        choose_transfer_department: string
        transfer_department_check: string
        cancel: string
        no: string
        message_cancel: string
    }
    popup: {
        title_button_cancel: string
        title_button_save: string
        tootip_close: string
    }
    pagination: {
        total: string
        item: string
        limit: string
        page: string
        page_first: string
        page_last: string
        page_next: string
        page_prev: string
    }
    table_column: {
        index: string
    }

    combobox: {
        not_found_data: string
    }

    select_info: {
        selected: string
        unselected: string
        to_delete: string
    }

    toast_message: {
        confirm: string
        cancel: string
        delete: string
        not_save: string
    }
}

export interface Resource {
    VN: ResourceData
}
