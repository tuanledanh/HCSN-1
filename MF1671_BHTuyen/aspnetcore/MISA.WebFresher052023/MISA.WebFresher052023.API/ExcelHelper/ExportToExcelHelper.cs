using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.WebFresher052023.Application.Dto.FixedAsset;
using System.Runtime.Intrinsics.X86;

namespace MISA.WebFresher052023.API.ExcelHelper
{
    public static class ExportToExcelHelper
    {
        #region Methods
        /// <summary>
        /// Truyền dữ liệu vào file excel mẫu
        /// </summary>
        /// <param name="fixedAssetExcels">Danh sách tài sản cần xuất</param>
        /// <param name="filePath">Đường dẫn đến file mẫu</param>
        /// <returns>Stream</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public static Stream UpdateDataIntoExcelTemplate(IEnumerable<FixedAssetExcel> fixedAssetExcels, string filePath)
        {
            var stream = new MemoryStream();

            var fileInfo = new FileInfo(filePath);

            if (fileInfo.Exists)
            {
                using var pck = new ExcelPackage(fileInfo);

                var ws = pck.Workbook.Worksheets["FixedAsset"];

                ws.Cells["B5:R5"].LoadFromCollection(fixedAssetExcels);
                var formula = ws.Cells[5, 2].Formula;
                ws.Cells[5, 2, fixedAssetExcels.Count(), 2].Formula = formula;

                pck.SaveAs(stream);

                stream.Position = 0;
            }

            return stream;
        } 
        #endregion
    }
}
