import MISAEnum from "../enum";
/**
 * Hàm chung cho việc xử lý các message thông báo lỗi liên quan đến các mã http
 * @param {*} res response trả về từ api
 * Author: LDTUAN (03/07/2023)
 */
export function processErrorResponse(res) {
  const status = res.response.status;
  const dataError = res.response.data;
  switch (status) {
    case MISAEnum.HttpStatusCode.ServerError:
      this.errors = [];
      this.errors.push(dataError.userMsg);
      this.showNotice = true;
      break;
    case MISAEnum.HttpStatusCode.BadRequest:
      this.errors = [];
      this.errors.push(dataError.userMsg);
      this.showNotice = true;
      break;
    case MISAEnum.HttpStatusCode.UnprocessableEntity:
      this.errors = [];
      this.errors.push(dataError.userMsg);
      this.showNotice = true;
      break;

    default:
      break;
  }
}
/**
 * Lấy ngày tháng hiện tại để hiển thị
 * Author: LDTUAN (04/07/2023)
 */
export function getCurrentDate() {
  return new Date().toJSON().slice(0, 10).replace(/-/g, "/");
}
/**
 * Thực hiện validate dữ liệu nhập vào
 * Author: LDTUAN (03/07/2023)
 */
export function onValidateData() {
  this.errors = [];
  // Các thông tin bắt buộc nhập
  if (this.asset.CustomerCode == "" || this.asset.CustomerCode == null) {
    this.errors.push(
      this.$_MISAResource[this.$_LANG_CODE].AssetCodeInvalidError.Empty
    );
  }
  // Các thông tin cần đúng định dạng (email)

  // Ngày tháng
}
