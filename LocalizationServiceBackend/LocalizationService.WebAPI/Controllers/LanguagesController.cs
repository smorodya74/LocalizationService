using LocalizationService.Application.Services;
using LocalizationService.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace LocalizationService.WebAPI.Controllers
{

    [ApiController]
    [Route("api/languages")]
    public class LanguagesController : ControllerBase
    {
        private readonly LanguagesService _service;
        public LanguagesController(LanguagesService service) => _service = service;

        [HttpGet]
        public async Task<ActionResult<List<Language>>> GetAllLanguages(CancellationToken ct)
        {
            return Ok(await _service.GetAllLanguages(ct));
        }

        [HttpGet("{langCode}")]
        public async Task<IActionResult> GetLanguageByCode(string langCode, CancellationToken ct)
        {
            var lang = (await _service.GetAllLanguages(ct))
                .FirstOrDefault(l => l.LanguageCode == langCode);

            return (lang != null) ? Ok(lang) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateLanguage(Language language, CancellationToken ct)
        {
            var langCode = await _service.CreateLanguage(language, ct);

            return CreatedAtAction(nameof(GetLanguageByCode), new { langCode }, langCode);
        }

        [HttpDelete("{langCode}")]
        public async Task<IActionResult> Delete(string langCode, CancellationToken ct)
        {
            var response = await _service.DeleteLangugage(langCode, ct);

            return response ? Ok() : NotFound();
        }
    }
}
