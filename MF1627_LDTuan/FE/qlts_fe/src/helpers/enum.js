const MISAEnum = {
  // Mở form thì form ở dạng cập nhật, thêm, sao chép
  FORM_MODE: {
    // Thêm
    ADD: 1,
    // Cập nhật
    UPDATE: 2,
    // Xem
    VIEW: 3,
    // Sao chép
    CLONE: 4,
  },
  // Giới tính
  GENDER: {
    // Giới tính nam
    MALE: 0,
    // Giới tính nữ
    FEMALE: 1,
  },
  // Mã lỗi
  HttpStatusCode: {
    // Server xử lý request HTTP thành công
    Success: 200,
    // Xử lý request thành công và trả về tài nguyên mới được tạo ra
    CreatedSuccess: 201,
    // Server xử lý thành công request nhưng không trả về dữ liệu
    NoContent: 204,
    // Request gửi đến server bị sai
    BadRequest: 400,
    // Từ chối truy cập ra không có thông tin xác thực hợp lệ
    NoAuthen: 401,
    // Server gặp trục trặc
    ServerError: 500,
  },
  // Mã code của các phím
  KEYCODE:{
    // Phím di chuyển xuống
    DOWN: 40,
    // Phím di chuyển lên
    UP: 38,
    // Phím enter
    ENTER: 13,
    // Phím tab
    TAB: 9,
    // Phím S
    S: 83,
  },
  // Giá trị của số nguyên
  INT:{
    // Giá trị lớn nhất
    MAX_VALUE: 2147483647,
  }
};

export default MISAEnum;
