using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.FresherWeb202305.Demo.Domain.Enums
{
    public enum ErrorCode
    {
        #region Mã lỗi chung
        /// <summary>
        /// Không tìm thấy dữ liệu
        /// </summary>
        NotFound = 1000,
        /// <summary>
        /// Dữ liệu không hợp lệ
        /// </summary>
        InvalidData = 1001,
        /// <summary>
        /// Server error
        /// </summary>
        ConflictData = 1002,
        /// <summary>
        /// Server error
        /// </summary>
        ServerError = 1003,
        /// <summary>
        /// Server error
        /// </summary>
        InvalidPageSize = 1004,
        #endregion

        #region Mã lỗi phân hệ nhân viên
        /// <summary>
        /// Mã nhân viên sai định dạng
        /// </summary>
        AssetWrongFormat = 2001,
        /// <summary>
        /// Trùng mã nhân viên
        /// </summary>
        AssetCodeDuplicated = 2002,
        /// <summary>
        /// Trùng mã nhân viên
        /// </summary>
        AssetCodeOutOfRange = 2003,
        /// <summary>
        /// Không tìm thấy tài sản
        /// </summary>
        EmployeeNotFound = 2004,
        #endregion

        #region Mã lỗi phân hệ phòng ban
        /// <summary>
        /// Không tìm thấy phòng ban
        /// </summary>
        DepartmentNotFound = 3004,
        AssetTypeNotFound=3005
        #endregion
    }
}
