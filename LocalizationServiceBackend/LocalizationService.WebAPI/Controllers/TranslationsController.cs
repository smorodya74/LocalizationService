using LocalizationService.Application.Models;
using LocalizationService.Application.Services;
using LocalizationService.DAL.DTO.TranslationDTO;
using LocalizationService.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace LocalizationService.WebAPI.Controllers
{
    [ApiController]
    [Route("api/translations")]
    public class TranslationsController : ControllerBase
    {
        private readonly TranslationsService _service;
        public TranslationsController(TranslationsService service) => _service = service;

        [HttpGet("page")]
        public async Task<ActionResult<PagedResult<Translation>>> GetPage(
            [FromQuery] int page = 1, 
            [FromQuery] int pageSize = 10, 
            CancellationToken ct = default)
        {
            var result = await _service.GetTranslationsPBP(page, pageSize, ct);
            return Ok(result);
        }

        [HttpGet("{key}")]
        public async Task<ActionResult<List<Translation>?>> GetTranslationByKey(string key, CancellationToken ct)
        {
            var localKey = new LocalizationKey(key);
            return Ok(await _service.GetTranslationsByKey(localKey, ct));
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<Translation>>> Search([FromQuery] string query, CancellationToken ct)
        {
            return Ok(await _service.SearchTranslationsByKey(query, ct));
        }

        [HttpPatch]
        public async Task<IActionResult> Update([FromBody] UpdateTranslationDto dto, CancellationToken ct)
        {
            var key = new LocalizationKey(dto.LocalizationKey);
            var language = new Language(dto.LanguageCode, dto.LanguageName);

            var translation = new Translation(key, language, dto.TranslationText);
            await _service.UpdateTranslation(key, translation, ct);

            return NoContent();
        }
    }
}
