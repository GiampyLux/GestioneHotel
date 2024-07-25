using Microsoft.AspNetCore.Mvc;
using GestioneHotel.Models;
using GestioneHotel.Services;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace GestioneHotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrenotazioniController : ControllerBase
    {
        private readonly DatabaseService _databaseService;

        public PrenotazioniController(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPrenotazioni()
        {
            var prenotazioni = new List<Prenotazioni>();
            using (var connection = _databaseService.GetConnection())
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM Prenotazioni", connection);
                var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    prenotazioni.Add(new Prenotazioni
                    {
                        Id = reader.GetInt32(0),
                        CodiceFiscaleCliente = reader.GetString(1),
                        NumeroCamera = reader.GetInt32(2),
                        DataPrenotazione = reader.GetDateTime(3),
                        NumeroProgressivoAnno = reader.GetInt32(4),
                        Anno = reader.GetInt32(5),
                        Dal = reader.GetDateTime(6),
                        Al = reader.GetDateTime(7),
                        CaparraConfirmatoria = reader.GetDecimal(8),
                        TariffaApplicata = reader.GetDecimal(9),
                        DettagliSoggiorno = reader.GetString(10)
                    });
                }
            }
            return Ok(prenotazioni);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPrenotazione(int id)
        {
            Prenotazioni prenotazione = null;
            using (var connection = _databaseService.GetConnection())
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM Prenotazioni WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    prenotazione = new Prenotazioni
                    {
                        Id = reader.GetInt32(0),
                        CodiceFiscaleCliente = reader.GetString(1),
                        NumeroCamera = reader.GetInt32(2),
                        DataPrenotazione = reader.GetDateTime(3),
                        NumeroProgressivoAnno = reader.GetInt32(4),
                        Anno = reader.GetInt32(5),
                        Dal = reader.GetDateTime(6),
                        Al = reader.GetDateTime(7),
                        CaparraConfirmatoria = reader.GetDecimal(8),
                        TariffaApplicata = reader.GetDecimal(9),
                        DettagliSoggiorno = reader.GetString(10)
                    };
                }
            }
            if (prenotazione == null)
                return NotFound();

            return Ok(prenotazione);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePrenotazione([FromBody] Prenotazioni prenotazione)
        {
            using (var connection = _databaseService.GetConnection())
            {
                await connection.OpenAsync();
                var command = new SqlCommand("INSERT INTO Prenotazioni (CodiceFiscaleCliente, NumeroCamera, DataPrenotazione, NumeroProgressivoAnno, Anno, Dal, Al, CaparraConfirmatoria, TariffaApplicata, DettagliSoggiorno) VALUES (@CodiceFiscaleCliente, @NumeroCamera, @DataPrenotazione, @NumeroProgressivoAnno, @Anno, @Dal, @Al, @CaparraConfirmatoria, @TariffaApplicata, @DettagliSoggiorno)", connection);
                command.Parameters.AddWithValue("@CodiceFiscaleCliente", prenotazione.CodiceFiscaleCliente);
                command.Parameters.AddWithValue("@NumeroCamera", prenotazione.NumeroCamera);
                command.Parameters.AddWithValue("@DataPrenotazione", prenotazione.DataPrenotazione);
                command.Parameters.AddWithValue("@NumeroProgressivoAnno", prenotazione.NumeroProgressivoAnno);
                command.Parameters.AddWithValue("@Anno", prenotazione.Anno);
                command.Parameters.AddWithValue("@Dal", prenotazione.Dal);
                command.Parameters.AddWithValue("@Al", prenotazione.Al);
                command.Parameters.AddWithValue("@CaparraConfirmatoria", prenotazione.CaparraConfirmatoria);
                command.Parameters.AddWithValue("@TariffaApplicata", prenotazione.TariffaApplicata);
                command.Parameters.AddWithValue("@DettagliSoggiorno", prenotazione.DettagliSoggiorno);
                await command.ExecuteNonQueryAsync();
            }
            return CreatedAtAction(nameof(GetPrenotazione), new { id = prenotazione.Id }, prenotazione);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePrenotazione(int id, [FromBody] Prenotazioni prenotazione)
        {
            using (var connection = _databaseService.GetConnection())
            {
                await connection.OpenAsync();
                var command = new SqlCommand("UPDATE Prenotazioni SET CodiceFiscaleCliente = @CodiceFiscaleCliente, NumeroCamera = @NumeroCamera, DataPrenotazione = @DataPrenotazione, NumeroProgressivoAnno = @NumeroProgressivoAnno, Anno = @Anno, Dal = @Dal, Al = @Al, CaparraConfirmatoria = @CaparraConfirmatoria, TariffaApplicata = @TariffaApplicata, DettagliSoggiorno = @DettagliSoggiorno WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@CodiceFiscaleCliente", prenotazione.CodiceFiscaleCliente);
                command.Parameters.AddWithValue("@NumeroCamera", prenotazione.NumeroCamera);
                command.Parameters.AddWithValue("@DataPrenotazione", prenotazione.DataPrenotazione);
                command.Parameters.AddWithValue("@NumeroProgressivoAnno", prenotazione.NumeroProgressivoAnno);
                command.Parameters.AddWithValue("@Anno", prenotazione.Anno);
                command.Parameters.AddWithValue("@Dal", prenotazione.Dal);
                command.Parameters.AddWithValue("@Al", prenotazione.Al);
                command.Parameters.AddWithValue("@CaparraConfirmatoria", prenotazione.CaparraConfirmatoria);
                command.Parameters.AddWithValue("@TariffaApplicata", prenotazione.TariffaApplicata);
                command.Parameters.AddWithValue("@DettagliSoggiorno", prenotazione.DettagliSoggiorno);
                var rows = await command.ExecuteNonQueryAsync();
                if (rows == 0)
                    return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrenotazione(int id)
        {
            using (var connection = _databaseService.GetConnection())
            {
                await connection.OpenAsync();
                var command = new SqlCommand("DELETE FROM Prenotazioni WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                var rows = await command.ExecuteNonQueryAsync();
                if (rows == 0)
                    return NotFound();
            }
            return NoContent();
        }
    }
}
