using Application.DTO;
using Domain.Model;
using MSIA.WebFresher052023.Application.Interface.Base;
using MSIA.WebFresher052023.Domain.Entity;

namespace Application.Interface
{
    public interface ITransferAssetDetailService : IBaseService<TransferAssetDetail, TransferAssetDetailModel, TransferAssetDetailDto, TransferAssetDetailCreateDto, TransferAssetDetailUpdateDto, TransferAssetDetailUpdateMultiDto>
    {
    }
}
