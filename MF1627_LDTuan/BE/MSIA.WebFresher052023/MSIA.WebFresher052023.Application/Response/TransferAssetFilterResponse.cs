using Application.DTO;
using MSIA.WebFresher052023.Application.Response.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSIA.WebFresher052023.Application.Response
{
    public class TransferAssetFilterResponse : BaseFilterResponse<TransferAssetDto>
    {
        public TransferAssetFilterResponse(int? totalPages, int? totalRecords, List<TransferAssetDto>? data) : base(totalPages, totalRecords, data)
        {
        }
    }
}