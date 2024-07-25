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
        private readonly DatabaseService _databaseService;

        public ServiziAggiuntiviController(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetServiziAggiuntivi()
        {
            var serviziAggiuntivi = new List<ServiziAggiuntivi>();
            using (var connection = _databaseService.GetConnection())
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM ServiziAggiuntivi", connection);
                var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    serviziAggiuntivi.Add(new ServiziAggiuntivi
                    {
                        Id = reader.GetInt32(0),
                        PrenotazioneId = reader.GetInt32(1),
                        Descrizione = reader.GetString(2),
                        Data = reader.GetDateTime(3),
                        Quantita = reader.GetInt32(4),
                        Prezzo = reader.GetDecimal(5)
                    });
                }
            }
            return Ok(serviziAggiuntivi);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetServizioAggiuntivo(int id)
        {
            ServiziAggiuntivi servizioAggiuntivo = null;
            using (var connection = _databaseService.GetConnection())
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM ServiziAggiuntivi WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    servizioAggiuntivo = new ServiziAggiuntivi
                    {
                        Id = reader.GetInt32(0),
                        PrenotazioneId = reader.GetInt32(1),
                        Descrizione = reader.GetString(2),
                        Data = reader.GetDateTime(3),
                        Quantita = reader.GetInt32(4),
                        Prezzo = reader.GetDecimal(5)
                    };
                }
            }
            if (servizioAggiuntivo == null)
                return NotFound();

            return Ok(servizioAggiuntivo);
        }

        [HttpPost]
        public async Task<IActionResult> CreateServizioAggiuntivo([FromBody] ServiziAggiuntivi servizioAggiuntivo)
        {
            using (var connection = _databaseService.GetConnection())
            {
                await connection.OpenAsync();
                var command = new SqlCommand("INSERT INTO ServiziAggiuntivi (PrenotazioneId, Descrizione, Data, Quantita, Prezzo) VALUES (@PrenotazioneId, @Descrizione, @Data, @Quantita, @Prezzo)", connection);
                command.Parameters.AddWithValue("@PrenotazioneId", servizioAggiuntivo.PrenotazioneId);
                command.Parameters.AddWithValue("@Descrizione", servizioAggiuntivo.Descrizione);
                command.Parameters.AddWithValue("@Data", servizioAggiuntivo.Data);
                command.Parameters.AddWithValue("@Quantita", servizioAggiuntivo.Quantita);
                command.Parameters.AddWithValue("@Prezzo", servizioAggiuntivo.Prezzo);
                await command.ExecuteNonQueryAsync();
            }
            return CreatedAtAction(nameof(GetServizioAggiuntivo), new { id = servizioAggiuntivo.Id }, servizioAggiuntivo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateServizioAggiuntivo(int id, [FromBody] ServiziAggiuntivi servizioAggiuntivo)

        {
            using (var connection = _databaseService.GetConnection())
            {
                await connection.OpenAsync();
                var command = new SqlCommand("UPDATE ServiziAggiuntivi SET PrenotazioneId = @PrenotazioneId, Descrizione = @Descrizione, Data = @Data, Quantita = @Quantita, Prezzo = @Prezzo WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@PrenotazioneId", servizioAggiuntivo.PrenotazioneId);
                command.Parameters.AddWithValue("@Descrizione", servizioAggiuntivo.Descrizione);
                command.Parameters.AddWithValue("@Data", servizioAggiuntivo.Data);
                command.Parameters.AddWithValue("@Quantita", servizioAggiuntivo.Quantita);
                command.Parameters.AddWithValue("@Prezzo", servizioAggiuntivo.Prezzo);
                var rows = await command.ExecuteNonQueryAsync();
                if (rows == 0)
                    return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServizioAggiuntivo(int id)
        {
            using (var connection = _databaseService.GetConnection())
            {
                await connection.OpenAsync();
                var command = new SqlCommand("DELETE FROM ServiziAggiuntivi WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                var rows = await command.ExecuteNonQueryAsync();
                if (rows == 0)
                    return NotFound();
            }
            return NoContent();
        }
    }
}
