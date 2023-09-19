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
        #region Properties
        /// <summary>
        /// Id của chi tiết điều chuyển
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Guid TransferAssetDetailId { get; set; }

        /// <summary>
        /// Id của tài sản điều chuyển
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Guid FixedAssetId { get; set; }

        /// <summary>
        /// Mã của tài sản điều chuyển
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public string FixedAssetCode { get; set; }

        /// <summary>
        /// Tên của tài sản điều chuyển
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public string FixedAssetName { get; set; }

        /// <summary>
        /// Nguyên giá của tài sản điều chuyển
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public double Cost { get; set; }

        /// <summary>
        /// Giá trị còn lại của tài sản điều chuyển
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public double RemainderCost { get; set; }

        /// <summary>
        /// Id của phòng ban đang sử dụng tài sản
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Tên của phòng ban đang sử dụng tài sản
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public string DepartmentName { get; set; }

        /// <summary>
        /// Id của phòng ban điều chuyển tài sản
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Guid TransferDepartmentId { get; set; }

        /// <summary>
        /// Tên của phòng ban điều chuyển tài sản
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public string TransferDepartmentName { get; set; }

        /// <summary>
        /// Lý do điều chuyển
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public string Reason { get; set; }

        /// <summary>
        /// Năm theo dõi tài sản
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public int TrackedYear { get; set; }

        /// <summary>
        /// Id của chứng từ điều chuyển
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Guid TransferAssetId { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Gán mã Id của chứng từ cho tài sản điều chuyển
        /// </summary>
        /// <param name="transferAssetId">Mã id của chứng từ</param>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public void SetTransferAssetId(Guid transferAssetId)
        {
            TransferAssetId = transferAssetId;
        }

        /// <summary>
        /// Lấy ra mã Id của tài sản điều chuyển
        /// </summary>
        /// <returns>Mã ID của tài sản điều chuyển</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Guid GetKey()
        {
            return TransferAssetDetailId;
        }

        /// <summary>
        /// Gán mã Id cho tài sản điều chuyển
        /// </summary>
        /// <param name="transferAssetDetailId">Mã Id của tài sản điều chuyển</param>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public void SetKey(Guid transferAssetDetailId)
        {
            TransferAssetDetailId = transferAssetDetailId;
        } 
        #endregion
    }
}
