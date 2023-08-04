using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.FresherWeb202305.Demo.Application.Interface;
using MISA.FresherWeb202305.Demo.Application.Interface.Base;
using MISA.FresherWeb202305.Demo.Application.Services;
using MISA.FresherWeb202305.Demo.Domain;
using MISA.FresherWeb202305.Demo.Domain.Interface.Base;

namespace MISA.FresherWeb202305.Demo.Controllers;

[Route("api/[controller]")]
[ApiController]
public abstract class BaseController<TEntityDto, TEntityCreateDto, TEntityUpadteDto>
    : BaseReadonlyController<TEntityDto>
{


    private readonly IBaseService<TEntityDto, TEntityCreateDto, TEntityUpadteDto> _baseService;

    public BaseController(IBaseService<TEntityDto, TEntityCreateDto, TEntityUpadteDto> baseService)
        : base(baseService)
    {
        _baseService = baseService;
    }



    // Rest of your implementation...







    /// <summary>
    /// Tạo mới một đối tượng
    /// </summary>
    /// <param name="entityCreateDto">Data đối tượng cần tạo</param>
    ///
    /// CreatedBy: nhtrieu (18/07/2023)
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] TEntityCreateDto entityCreateDto)
    {
        
        
            await _baseService.InsertAsync(entityCreateDto);

            return StatusCode(StatusCodes.Status201Created, "Them thanh cong");
       
    }

    /// <summary>
    /// Sửa đối tượng theo id
    /// </summary>
    /// <param name="id">Mã đối tượng</param>
    /// <param name="entityUpdateDto">Data đối tượng cần sửa</param>
    /// 
    /// CreatedBy: txphuc (18/07/2023)
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] TEntityUpadteDto entityUpdateDto)
    {
        var entity=await _baseService.UpdateAsync(id, entityUpdateDto);

        return Ok(entity);
    }

    /// <summary>
    /// Xoá một đối tượng theo Id
    /// </summary>
    /// <param name="id">Mã đối tượng</param>
    /// 
    /// CreatedBy: nhtrieu (18/07/2023)
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteByIdAsync([FromRoute] Guid id)
    {
        var entity=await _baseService.DeleteByIdAsync(id);

        return Ok(entity);
    }

    /// <summary>
    /// Xoá nhiều đối tượng theo Id
    /// </summary>
    /// <param name="entityIds">Danh sách mã cần xoá</param>
    /// 
    /// CreatedBy: nhtrieu (18/07/2023)
    [HttpDelete("DeleteMulplite")]
    public async Task<IActionResult> DeleteAsync([FromBody] List<Guid> entityIds)
    {
         var entity=await _baseService .DeleteAsync(entityIds);

        return Ok(entity);
    }
}