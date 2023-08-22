/**
 * Hiển thị thông báo xóa
 * Author: LDTUAN (20/08/2023)
 */
export function showToastDelete(
  messageDeleteSingle = null,
  messageDeleteMultiple = null,
  messageDeleteZero = null
) {
  const selectedRows = this.selectedRows;
  const numberOfRecords = selectedRows.length;
  // Thêm số 0 vào trước số bản ghi, nếu số bản ghi nhỏ hơn 10
  // padStart là thêm 1 ký tự vào trước chuỗi ban đầu, nếu sau khi thêm mà độ dài chuổi bằng độ dài cung cấp
  // Ở đây ví dụ numberOfRecords = 1, thì nếu thêm số 0, độ dài chuỗi = 2, đúng
  // Nhưng nếu numberOfRecords = 10, thì nếu thêm số 0, độ dài chuỗi = 3, sai
  const formattedNumberOfRecords =
    numberOfRecords < 10
      ? numberOfRecords.toString().padStart(2, "0")
      : numberOfRecords;
  if (numberOfRecords == 1) {
    this.toast_content_delete =
      messageDeleteSingle +
      selectedRows[0].FixedAssetCode +
      " - " +
      selectedRows[0].FixedAssetName +
      "?";
  } else if (numberOfRecords > 1) {
    this.toast_content_delete =
      formattedNumberOfRecords + messageDeleteMultiple;
  } else {
    this.toast_content_delete = messageDeleteZero;
  }
  this.isShowToastDelete = true;
}

/**
 * Đóng thông báo xóa
 * Author: LDTUAN (20/08/2023)
 */
export function closeToastWarning() {
  this.isShowToastDelete = false;
  this.isShowToastExport = false;
  this.isShowToastValidateBE = false;
}
