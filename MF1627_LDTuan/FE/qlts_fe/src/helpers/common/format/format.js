/**
 * Format số sang chuỗi, mỗi 3 số có 1 dấu chấm
 * @param {Int16Array} price giá trị muốn format lại
 * Author: LDTUAN (02/08/2023)
 */
export const formatMoney = (price) => {
  if (price) {
    return String(price).replace(/\B(?=(\d{3})+(?!\d))/g, ".");
  }
};

/**
 * Format chuỗi sang số để lưu lại trong database
 * @param {Int16Array} price giá trị muốn format lại
 * Author: LDTUAN (02/08/2023)
 */
export const formatMoneyToInt = (price) => {
  if (typeof price === "string" || price instanceof String) {
    return parseInt(price.replace(/\./g, ""));
  } else {
    return price;
  }
};

/**
 * Tính toán giá trị hao mòn
 * @param {Int16Array} price tiền
 * @param {Int16Array} year năm
 * Author: LDTUAN (02/08/2023)
 */
export const AssetDepreciation = (price, year) => {
  if (price && year) {
    var finalPrice = price - (price * (1 / year)).toFixed(0);
    return formatMoney(finalPrice);
  }
};

/**
 * Data truyền về có nhiều dạng, ngày tháng năm giờ ....
 * Format lại dạng duy nhất là ngày tháng năm
 * @param {date} date ngày tháng
 * Author: LDTUAN (02/08/2023)
 */
export const DateFormat = (date) => {
  if (date) {
    // Create a Date object from the input date string in the local timezone
    const localDate = new Date(date);

    // Get the UTC time equivalent of the localDate
    const utcDate = new Date(
      Date.UTC(
        localDate.getFullYear(),
        localDate.getMonth(),
        localDate.getDate(),
        localDate.getHours(),
        localDate.getMinutes(),
        localDate.getSeconds(),
        localDate.getMilliseconds()
      )
    );

    // Convert the UTC date to ISO format and extract the date part
    const dateAfterFormat = utcDate.toISOString().slice(0, 10);
    return dateAfterFormat;
  }
};

/**
 * Cắt chuỗi nếu độ dài vượt quá maxLength và thêm dấu ba chấm "..."
 * @param {string} text Chuỗi cần cắt
 * @param {number} maxLength Độ dài tối đa cho phép
 * @returns {string} Chuỗi đã cắt (nếu cần)
 */
export const truncateText = (text, maxLength) => {
  if (text.length > maxLength) {
    return text.substring(0, maxLength) + "...";
  } else {
    return text;
  }
};
