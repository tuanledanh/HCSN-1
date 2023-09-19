/**
 * Thực hiện bôi xanh 1 dòng nếu click vào dòng đó
 * @param {object} asset thông tin tài sản
 * Author: LB.Thành (02/08/2023)
 */
export function rowOnClick(asset, assetType) {
    const index = this.selectedRows.indexOf(asset);
    if (index !== -1 && this.selectedRows.length == 1) {
        this.selectedRows = [];
    } else {
        this.selectedRows = [];
        this.selectedRows.push(asset);
    }
    if (this.selectedRows.length == 0) {
        this.lastIndex = 0;
    } else {
        if (assetType === 'assets') {
            this.lastIndex = this.assets.indexOf(asset);
        } else if (assetType === 'transferAssets') {
            this.lastIndex = this.transferAssets.indexOf(asset);
        }
    }
    this.selectedRowsByCheckBox = [];
}


/**
 * Chọn bản ghi bằng cách nhấn click vào ô checkbox
 * @param {object} asset bản ghi
 * Author: LB.Thành (09/08/2023)
 */
export function rowOnClickByCheckBox(asset, assetType) {
    const index = this.selectedRowsByCheckBox.indexOf(asset);
    if (index !== -1) {
        this.selectedRows.splice(index, 1);
        this.selectedRowsByCheckBox.splice(index, 1);
    } else {
        this.selectedRows.push(asset);
        this.selectedRowsByCheckBox.push(asset);
    }
    if (this.selectedRows.length == 0) {
        this.lastIndex = 0;
    } else {
        if (assetType === 'assets') {
            this.lastIndex = this.assets.indexOf(asset);
        } else if (assetType === 'transferAssets') {
            this.lastIndex = this.transferAssets.indexOf(asset);
        }
    }
}

/**
 * Chọn bản ghi bằng cách nhấn ctrl + click
 * @param {object} asset bản ghi
 * Author: LB.Thành (09/08/2023)
 */
export function rowOnCtrlClick(asset, assetType) {
    const index = this.selectedRows.indexOf(asset);
    const indexCheckbox = this.selectedRowsByCheckBox.indexOf(asset);
    if (index !== -1) {
        this.selectedRows.splice(index, 1);
        if (indexCheckbox !== -1)
            this.selectedRowsByCheckBox.splice(indexCheckbox, 1);
    } else {
        this.selectedRows.push(asset);
        this.selectedRowsByCheckBox.push(asset);
    }
    if (this.selectedRows.length == 0) {
        this.lastIndex = 0;
    } else {
        if (assetType === 'assets') {
            this.lastIndex = this.assets.indexOf(asset);
        } else if (assetType === 'transferAssets') {
            this.lastIndex = this.transferAssets.indexOf(asset);
        }
    }
}
