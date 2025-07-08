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
        private readonly LanguagesService _langService;

        public TranslationsController(TranslationsService service, LanguagesService languagesService)
        {
            _service = service;
            _langService = languagesService;
        }

        [HttpGet("page")]
        public async Task<ActionResult<PagedResult<Translation>>> GetPage(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery(Name = "search")] string? search = null,
            CancellationToken ct = default)
        {
            var result = await _service.GetTranslationsPageAsync(page, pageSize, search, ct);
            return Ok(result);
        }

        [HttpPatch]
        public async Task<IActionResult> Update([FromBody] UpdateTranslationDto dto, CancellationToken ct)
        {

            var key = new LocalizationKey(dto.LocalizationKey);
            var language = _langService.GetLanguageByCode(dto.LanguageCode, ct).Result;

            var translation = new Translation(key, language, dto.TranslationText);
            await _service.UpdateTranslation(key, translation, ct);

            return NoContent();
        }
    }
}
