using MSIA.WebFresher052023.Domain.Entity;
using MSIA.WebFresher052023.Domain.Model;

namespace MSIA.WebFresher052023.Domain.Resource
{
    public class ErrorMessagesTransferAsset
    {
        public static string Arise(string fixedAssetCode)
        {
            return $"Tài sản <strong>{fixedAssetCode}</strong> đã có phát sinh chứng từ. Bạn không thể xóa chứng từ này";
        }
        public static string AriseUpdate(string fixedAssetCode)
        {
            return $"Tài sản <strong>{fixedAssetCode}</strong> đã có phát sinh chứng từ. Bạn không thể cập nhật chứng từ này";
        }
        public static string AriseAssetDelete(string fixedAssetCode)
        {
            return $"Tài sản <strong>{fixedAssetCode}</strong> đã có phát sinh chứng từ. Bạn không thể xóa tài sản này";
        }
        public static string AriseAssetUpdate(string fixedAssetCode)
        {
            return $"Tài sản <strong>{fixedAssetCode}</strong> đã có phát sinh chứng từ. Bạn không thể cập nhật tài sản này";
        }
        public static string Infor(List<TransferAssetDeleteModel> transfers)
        {
            var result = string.Join(", ", transfers
                .Select(transfer =>
                $"- Chứng từ điều chuyển <strong>{transfer.TransferAssetCode} ({transfer.TransferDate.ToString("dd/MM/yyyy")})</strong>"));
            return result;
        }
        public static string TransferSmallTransaction => "Ngày điều chuyển phải lớn hơn hoặc bằng ngày chứng từ";
        public static string InforDate(string fixedAssetCode, TransferAsset transferAsset)
        {
            return $"Tài sản <strong>{fixedAssetCode}</strong> đã có phát sinh chứng từ. Ngày điều chuyển phải " +
                $"lớn hơn của chứng từ <strong>{transferAsset.TransferAssetCode} ({transferAsset.TransferDate.ToString("dd/MM/yyyy")})</strong>";
        }
    }
}
