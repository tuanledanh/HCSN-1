using MSIA.WebFresher052023.Domain.Entity;

namespace Application.DTO
{
    public class TransferAssetCreateDto
    {
        #region Fields
        // dữ liệu của chứng từ
        public TransferAsset? TransferAsset { get; set; }

        // danh sách dữ liệu của tài sản thuộc chứng từ
        public List<TransferAssetDetailCreateDto>? ListTransferAssetDetail { get; set; }

        // danh sách dữ liệu của ban giao nhận
        public List<ReceiverCreateDto>? ListReceiver { get; set; }
        #endregion
    }
}
