using LocalizationService.Application.Services;
using LocalizationService.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace LocalizationService.WebAPI.Controllers
{
    [ApiController]
    [Route("api/localization_keys")]
    public class LocalizationKeysController : ControllerBase
    {
        private readonly LocalizationKeysService _service;
        public LocalizationKeysController(LocalizationKeysService service) => _service = service;

        [HttpGet]
        public async Task<ActionResult<List<LocalizationKey>>> GetAllKeys(CancellationToken ct)
        {
            return Ok(await _service.GetAllKeys(ct));
        }

        [HttpGet("{query}")]
        public async Task<ActionResult<List<LocalizationKey>>> SearchKeys(string query, CancellationToken ct)
        {
            return Ok(await _service.SearchKey(query, ct));
        }

        [HttpPost]
        public async Task<IActionResult> CreateLocalizationKey([FromBody] LocalizationKey key, CancellationToken ct)
        {
            var keyName = await _service.CreateKey(key, ct);

            return Ok(keyName);
        }

        [HttpDelete("{key}")]
        public async Task<IActionResult> DeleteLocalizationKey(LocalizationKey key, CancellationToken ct)
        {
            await _service.DeleteKey(key, ct);
            return NoContent();
        }
    }
}