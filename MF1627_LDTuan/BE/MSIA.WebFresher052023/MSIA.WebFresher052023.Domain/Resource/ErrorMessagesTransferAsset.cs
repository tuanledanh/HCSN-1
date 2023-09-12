using MSIA.WebFresher052023.Domain.Model;

namespace MSIA.WebFresher052023.Domain.Resource
{
    public class ErrorMessagesTransferAsset
    {
        public static string Arise(string fixedAssetCode)
        {
            return $"Tài sản <strong>{fixedAssetCode}</strong> đã có phát sinh chứng từ. Bạn không thể xóa chứng từ này";
        }
        public static string Infor(List<TransferAssetDeleteModel> transfers)
        {
            var result = string.Join(", ", transfers
                .Select(transfer =>
                $"- Chứng từ điều chuyển <strong>{transfer.TransferAssetCode} ({transfer.TransferDate.ToString("dd/MM/yyyy")})</strong>"));
            return result;
        }
    }
}
