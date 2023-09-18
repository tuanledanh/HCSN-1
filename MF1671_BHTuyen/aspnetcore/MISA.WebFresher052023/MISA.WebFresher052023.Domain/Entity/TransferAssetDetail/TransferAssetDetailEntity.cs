using MISA.WebFresher052023.Domain.Interface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Domain.Entity.TransferAssetDetail
{
    public class TransferAssetDetailEntity : BaseAuditEntity, IHasKeyEntity
    {
        /// <summary>
        /// Id của chi tiết điều chuyển
        /// </summary>
        public Guid TransferAssetDetailId { get; set; }

        /// <summary>
        /// Id của tài sản điều chuyển
        /// </summary>
        public Guid FixedAssetId { get; set; }

        /// <summary>
        /// Mã của tài sản điều chuyển
        /// </summary>
        public string FixedAssetCode { get; set; }

        /// <summary>
        /// Tên của tài sản điều chuyển
        /// </summary>
        public string FixedAssetName { get; set; }

        /// <summary>
        /// Nguyên giá của tài sản điều chuyển
        /// </summary>
        public double Cost { get; set; }

        /// <summary>
        /// Giá trị còn lại của tài sản điều chuyển
        /// </summary>
        public double RemainderCost { get; set; }

        /// <summary>
        /// Id của phòng ban đang sử dụng tài sản
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Tên của phòng ban đang sử dụng tài sản
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// Id của phòng ban điều chuyển tài sản
        /// </summary>
        public Guid TransferDepartmentId { get; set; }

        /// <summary>
        /// Tên của phòng ban điều chuyển tài sản
        /// </summary>
        public string TransferDepartmentName { get; set; }

        /// <summary>
        /// Lý do điều chuyển
        /// </summary>
        public string Reason { get; set; }

        public int TrackedYear { get; set; }

        /// <summary>
        /// Id của chứng từ điều chuyển
        /// </summary>
        public Guid TransferAssetId { get; set; }

        public void SetTransferAssetId(Guid id)
        {
            TransferAssetId = id;
        }

        public Guid GetKey()
        {
            return TransferAssetDetailId;
        }

        public void SetKey(Guid id)
        {
            TransferAssetDetailId = id;
        }
    }
}
