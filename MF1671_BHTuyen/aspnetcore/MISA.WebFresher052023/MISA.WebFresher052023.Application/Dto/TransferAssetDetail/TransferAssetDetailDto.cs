using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Application.Dto.TransferAssetDetail
{
    public class TransferAssetDetailDto
    {

        /// <summary>
        /// Id của chi tiết điều chuyển
        /// </summary>
        public Guid TransferAssetDetailId { get; set; }

        public Guid FixedAssetId { get; set; }

        public string FixedAssetCode { get; set; }

        public string FixedAssetName { get; set; }

        public double Cost { get; set; }

        public double RemainderCost { get; set; }

        public Guid DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public Guid TransferDepartmentId { get; set; }

        public string TransferDepartmentName { get; set; }

        public string Reason { get; set; }

        public int TrackedYear { get; set; }

        /// <summary>
        /// Id của chứng từ điều chuyển
        /// </summary>
        public Guid TransferAssetId { get; set; }
    }
}
