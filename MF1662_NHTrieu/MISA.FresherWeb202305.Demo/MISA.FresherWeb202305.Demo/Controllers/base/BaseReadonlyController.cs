using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.FresherWeb202305.Demo.Application.Dto;
using MISA.FresherWeb202305.Demo.Application.Interface;
using MISA.FresherWeb202305.Demo.Application.Interface.Base;
using MISA.FresherWeb202305.Demo.Application.Services;

namespace MISA.FresherWeb202305.Demo.Controllers;

[Route("api/[controller]")]
[ApiController]
public abstract class BaseReadonlyController<TEntityDto> : ControllerBase
{
    protected readonly IBaseReadonlyService<TEntityDto> _baseReadonlyService;

    public BaseReadonlyController(IBaseReadonlyService<TEntityDto> baseReadonlyService)
    {
        _baseReadonlyService = baseReadonlyService;
    }


    /// <summary>
    /// Get toàn bộ danh sách đối tượng
    /// </summary>
    /// <returns>Danh sách đối tượng</returns>
    /// CreatedBy: nhtrieu (18/07/2023)
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var entityDtos = await _baseReadonlyService.GetAllAsync();

        return Ok(entityDtos);
    }

    /// <summary>
    /// Get một đối tượng thông qua Id
    /// </summary>
    /// <param name="id">Mã đối tượng</param>
    /// <returns>Trả về thông tin một đối tượng tìm được</returns> 
    /// CreatedBy: nhtrieu (18/07/2023)
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
    {
        var entityDto = await _baseReadonlyService.GetByIdAsync(id);

        return Ok(entityDto);
    }

    
}
