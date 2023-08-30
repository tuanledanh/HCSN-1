using Application.DTO;
using Domain.Model;
using MSIA.WebFresher052023.Application.Interface.Base;
using MSIA.WebFresher052023.Domain.Entity;

namespace Application.Interface
{
    public interface ITransferAssetService : IBaseService<TransferAsset, TransferAssetModel, TransferAssetDto, TransferAssetCreateDto, TransferAssetUpdateDto, TransferAssetUpdateMultiDto>
    {
    }
}
