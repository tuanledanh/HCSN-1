/**
 * Thực hiện bôi xanh 1 dòng nếu click vào dòng đó
 * @param {object} asset thông tin tài sản
 * Author: LDTUAN (02/08/2023)
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
    if (assetType === "assets") {
      this.lastIndex = this.assets.indexOf(asset);
    } else if (assetType === "transferAssets") {
      this.lastIndex = this.transferAssets.indexOf(asset);
    }
  }
  this.selectedRowsByCheckBox = [];
}

/**
 * Chọn bản ghi bằng cách nhấn click vào ô checkbox
 * @param {object} asset bản ghi
 * Author: LDTUAN (09/08/2023)
 */
export function rowOnClickByCheckBox(asset, assetType) {
  let index = null;
  if (assetType === "assets") {
    index = this.selectedRowsByCheckBox.findIndex(
      (item) => item.FixedAssetId === asset.FixedAssetId
    );
  } else if (assetType === "transferAssets") {
    index = this.selectedRowsByCheckBox.findIndex(
      (item) => item.TransferAssetId === asset.TransferAssetId
    );
  }

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
    if (assetType === "assets") {
      this.lastIndex = this.assets.findIndex(
        (item) => item.FixedAssetId === asset.FixedAssetId
      );
    } else if (assetType === "transferAssets") {
      this.lastIndex = this.transferAssets.findIndex(
        (item) => item.TransferAssetId === asset.TransferAssetId
      );
    }
  }
}

/**
 * Chọn bản ghi bằng cách nhấn ctrl + click
 * @param {object} asset bản ghi
 * Author: LDTUAN (09/08/2023)
 */
export function rowOnCtrlClick(asset, assetType) {
  let index = null;
  let indexCheckbox = null;

  if (assetType === "assets") {
    index = this.selectedRows.findIndex(
      (item) => item.FixedAssetId === asset.FixedAssetId
    );
    indexCheckbox = this.selectedRowsByCheckBox.findIndex(
      (item) => item.FixedAssetId === asset.FixedAssetId
    );
  } else if (assetType === "transferAssets") {
    index = this.selectedRows.findIndex(
      (item) => item.TransferAssetId === asset.TransferAssetId
    );
    indexCheckbox = this.selectedRowsByCheckBox.findIndex(
      (item) => item.TransferAssetId === asset.TransferAssetId
    );
  }

  if (index !== -1) {
    this.selectedRows.splice(index, 1);
  } else {
    this.selectedRows.push(asset);
  }
  if (indexCheckbox !== -1) {
    this.selectedRowsByCheckBox.splice(indexCheckbox, 1);
  } else {
    this.selectedRowsByCheckBox.push(asset);
  }
  if (this.selectedRows.length == 0) {
    this.lastIndex = 0;
  } else {
    if (assetType === "assets") {
      this.lastIndex = this.assets.findIndex(
        (item) => item.FixedAssetId === asset.FixedAssetId
      );
    } else if (assetType === "transferAssets") {
      this.lastIndex = this.transferAssets.findIndex(
        (item) => item.TransferAssetId === asset.TransferAssetId
      );
    }
  }
}

/**
 * Chọn bản ghi bằng cách nhấn shift + click
 * @param {object} asset bản ghi
 * Author: LDTUAN (09/08/2023)
 */
// export function rowOnShiftClick(asset, assetType) {
//   if (event.shiftKey) {
//     let data = null;
//     if (assetType === "assets") {
//       data = this.assets;
//     } else if (assetType === "transferAssets") {
//       data = this.transferAssets;
//     }

//     const index = data.indexOf(asset);
//     let list = [];
//     let newestIndex = index + 1;
//     if (newestIndex <= this.lastIndex) {
//       list = data.slice(newestIndex - 1, this.lastIndex + 1);
//     } else {
//       list = data.slice(this.lastIndex, newestIndex);
//     }

//     if (assetType === "assets") {
//       this.selectedRows = list;
//     } else if (assetType === "transferAssets") {
//       this.selectedRows = [];
//       this.selectedRowsByCheckBox = list;
//     }
//   }
// }
