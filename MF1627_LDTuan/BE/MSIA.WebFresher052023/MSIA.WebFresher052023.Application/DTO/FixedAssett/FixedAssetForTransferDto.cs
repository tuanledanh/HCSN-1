using Application.DTO;

namespace MSIA.WebFresher052023.Application.DTO.FixedAssett
{
    public class FixedAssetForTransferDto
    {
        public int? PageNumber { get; set; }
        public int? PageLimit { get; set; }
        public string? FilterName { get; set; }
        public List<FixedAssetDto>? FixedAssetDtos { get; set; }
        public List<Guid>? TransferAssetDetailIds { get; set; }
    }
}
