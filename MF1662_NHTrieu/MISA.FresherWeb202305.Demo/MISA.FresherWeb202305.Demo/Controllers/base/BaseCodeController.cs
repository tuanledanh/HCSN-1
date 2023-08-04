using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.FresherWeb202305.Demo.Application.Interface;
using MISA.FresherWeb202305.Demo.Application.Interface.Base;

namespace MISA.FresherWeb202305.Demo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseCodeController<TEntityDto, TEntityCreateDto, TEntityUpdateDto> : BaseController<TEntityDto, TEntityCreateDto, TEntityUpdateDto>
{
    protected readonly IBaseCodeService<TEntityDto, TEntityCreateDto, TEntityUpdateDto> _baseCodeService;
   

    public BaseCodeController(IBaseCodeService<TEntityDto, TEntityCreateDto, TEntityUpdateDto> baseService) : base(baseService)
    {
        _baseCodeService = baseService;
    }


    [HttpGet("NewCode")]
    public async Task<IActionResult> FindNewEmployeeCodeAsync()
    {
        var newCode = await _baseCodeService.FindNewCodeAsync();

        return Ok(newCode);
    }

}
