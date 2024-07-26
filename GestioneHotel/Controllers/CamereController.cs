using Microsoft.AspNetCore.Mvc;
using GestioneHotel.Models;
using GestioneHotel.Services;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace GestioneHotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CamereController : ControllerBase
    {
        private readonly DatabaseService _databaseService;

        public CamereController(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCamere()
        {
            var camere = new List<Camere>();
            using (var connection = _databaseService.GetConnection())
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM Camere", connection);
                var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    camere.Add(new Camere
                    {
                        Numero = reader.GetInt32(0),
                        Descrizione = reader.GetString(1),
                        Tipologia = reader.GetString(2)
                    });
                }
            }
            return Ok(camere);
        }

        [HttpGet("{numero}")]
        public async Task<IActionResult> GetCamera(int numero)
        {
            Camere camera = null;
            using (var connection = _databaseService.GetConnection())
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM Camere WHERE Numero = @Numero", connection);
                command.Parameters.AddWithValue("@Numero", numero);
                var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    camera = new Camere
                    {
                        Numero = reader.GetInt32(0),
                        Descrizione = reader.GetString(1),
                        Tipologia = reader.GetString(2)
                    };
                }
            }
            if (camera == null)
                return NotFound();

            return Ok(camera);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCamera([FromBody] Camere camera)
        {
            using (var connection = _databaseService.GetConnection())
            {
                await connection.OpenAsync();
                var command = new SqlCommand("INSERT INTO Camere (Numero, Descrizione, Tipologia) VALUES (@Numero, @Descrizione, @Tipologia)", connection);
                command.Parameters.AddWithValue("@Numero", camera.Numero);
                command.Parameters.AddWithValue("@Descrizione", camera.Descrizione);
                command.Parameters.AddWithValue("@Tipologia", camera.Tipologia);
                await command.ExecuteNonQueryAsync();
            }
            return CreatedAtAction(nameof(GetCamera), new { numero = camera.Numero }, camera);
        }

        [HttpPut("{numero}")]
        public async Task<IActionResult> UpdateCamera(int numero, [FromBody] Camere camera)
        {
            using (var connection = _databaseService.GetConnection())
            {
                await connection.OpenAsync();
                var command = new SqlCommand("UPDATE Camere SET Descrizione = @Descrizione, Tipologia = @Tipologia WHERE Numero = @Numero", connection);
                command.Parameters.AddWithValue("@Numero", numero);
                command.Parameters.AddWithValue("@Descrizione", camera.Descrizione);
                command.Parameters.AddWithValue("@Tipologia", camera.Tipologia);
                var rows = await command.ExecuteNonQueryAsync();
                if (rows == 0)
                    return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{numero}")]
        public async Task<IActionResult> DeleteCamera(int numero)
        {
            using (var connection = _databaseService.GetConnection())
            {
                await connection.OpenAsync();
                var command = new SqlCommand("DELETE FROM Camere WHERE Numero = @Numero", connection);
                command.Parameters.AddWithValue("@Numero", numero);
                var rows = await command.ExecuteNonQueryAsync();
                if (rows == 0)
                    return NotFound();
            }
            return NoContent();
        }
    }
}
