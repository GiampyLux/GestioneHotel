using Microsoft.AspNetCore.Mvc;
using GestioneHotel.Models;
using GestioneHotel.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestioneHotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiziAggiuntiviController : ControllerBase
    {
        private readonly ServiziAggiuntiviService _serviziAggiuntiviService;

        public ServiziAggiuntiviController(ServiziAggiuntiviService serviziAggiuntiviService)
        {
            _serviziAggiuntiviService = serviziAggiuntiviService;
        }

        [HttpGet]
        public async Task<IActionResult> GetServiziAggiuntivi()
        {
            var serviziAggiuntivi = await _serviziAggiuntiviService.GetAllServiziAggiuntiviAsync();
            return Ok(serviziAggiuntivi);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetServizioAggiuntivo(int id)
        {
            var servizioAggiuntivo = await _serviziAggiuntiviService.GetServiziAggiuntiviByIdAsync(id);
            if (servizioAggiuntivo == null)
                return NotFound();
            return Ok(servizioAggiuntivo);
        }

        [HttpPost]
        public async Task<IActionResult> CreateServizioAggiuntivo([FromBody] ServiziAggiuntivi servizioAggiuntivo)
        {
            await _serviziAggiuntiviService.AddServiziAggiuntiviAsync(servizioAggiuntivo);
            return CreatedAtAction(nameof(GetServizioAggiuntivo), new { id = servizioAggiuntivo.Id }, servizioAggiuntivo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateServizioAggiuntivo(int id, [FromBody] ServiziAggiuntivi servizioAggiuntivo)
        {
            var updated = await _serviziAggiuntiviService.UpdateServizioAggiuntivoAsync(id, servizioAggiuntivo);
            if (!updated)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServizioAggiuntivo(int id)
        {
            var deleted = await _serviziAggiuntiviService.DeleteServizioAggiuntivoAsync(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }
    }
}
