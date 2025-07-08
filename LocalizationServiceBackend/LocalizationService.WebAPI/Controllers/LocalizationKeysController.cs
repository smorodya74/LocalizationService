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