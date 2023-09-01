using MSIA.WebFresher052023.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class TransferAssetUpdateDto
    {
        #region Fields
        // dữ liệu của chứng từ
        [Required]
        public TransferAsset? TransferAsset { get; set; }

        // danh sách dữ liệu của tài sản thuộc chứng từ
        [Required]
        public List<TransferAssetDetailUpdateDto>? ListTransferAssetDetail { get; set; }

        // danh sách dữ liệu của ban giao nhận
        public List<ReceiverUpdateDto>? ListReceiver { get; set; }
        #endregion
    }
}
