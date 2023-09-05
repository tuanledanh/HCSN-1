using Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSIA.WebFresher052023.Application.DTO.FixedAssett
{
    public class FixedAssetForTransferDto
    {
        public int? pageNumber { get; set; }
        public int? pageLimit {  get; set; }
        public List<FixedAssetDto>? FixedAssetDtos { get; set; }
    }
}
