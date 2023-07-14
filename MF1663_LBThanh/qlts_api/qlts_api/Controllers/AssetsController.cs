using Microsoft.AspNetCore.Mvc;
using qlts_api.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace qlts_api.Controllers
{
    [Route("api/v1/Asset")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        private readonly IAssetRepo assetRepo;
        public AssetsController(IAssetRepo assetRepo)
        {
            this.assetRepo = assetRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsset()
        {
            try
            {
                var assets = await assetRepo.GetAllAsync();
                return StatusCode(200, assets);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/<AssetsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AssetsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AssetsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AssetsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
