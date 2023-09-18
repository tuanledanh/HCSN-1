using MISA.WebFresher052023.Domain.Entity.TransferAsset;
using MISA.WebFresher052023.Domain.Enum;
using MISA.WebFresher052023.Domain.Exceptions;
using MISA.WebFresher052023.Domain.Interface.TransferAsset;
using MISA.WebFresher052023.Domain.Resource;
using MISA.WebFresher052023.Domain.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Domain.Service
{
    public class TransferAssetManager : BaseManager<TransferAssetEntity>, ITransferAssetManager
    {
        private readonly ITransferAssetRepository _transferAssetRepository;

        public TransferAssetManager(ITransferAssetRepository transferAssetRepository) : base(transferAssetRepository)
        {
            _transferAssetRepository = transferAssetRepository;
        }

        public override string MessageError { get; set; } = VietNamese.TransferAssetCodeConflict;



        public void CheckTransferDateLaterTransTransactionDateAsync(DateTime transferDate, DateTime transactionDate)
        {
            var checkYear = transferDate.Year - transactionDate.Year;
            var checkMonth = transferDate.Month - transactionDate.Month;
            var checkDay = transferDate.Day - transactionDate.Day;
            if(checkYear >= 0 || checkMonth >= 0 || checkDay >= 0) { return;}

            throw new Exception(VietNamese.TransferDateLaterTransactionDate);
        }

        public async Task CheckDeleteTransferAssetAsync(List<Guid> transferAssetIds)
        {
            var fixedAssetTransferInfo = await _transferAssetRepository.GetTransferAssetsLaterAsync(transferAssetIds);

            if (fixedAssetTransferInfo.TransferAssets.Any())
            {
                var errorMessage = string.Format(VietNamese.NoDeleteTransferAsset, fixedAssetTransferInfo.TransferAssetCode, fixedAssetTransferInfo.TransferDate?.ToString("dd/MM/yyyy"));

                errorMessage = string.Concat(errorMessage, " ", string.Format(VietNamese.TransferAssetExist, fixedAssetTransferInfo.FixedAssetCode));

                var transferAssetInfo = fixedAssetTransferInfo.TransferAssets.Select(transferAsset => string.Format(VietNamese.TransferAssetInfo, transferAsset.TransferAssetCode, transferAsset.TransferDate.ToString("dd/MM/yyyy"))).ToList();

                throw new UserException(errorMessage, transferAssetInfo);
            }
        }

    }
}
