using GestioneHotel.Models;
using GestioneHotel.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

[Route("api/[controller]")]
[ApiController]
public class ClientiController : ControllerBase
{
    private readonly DatabaseService _databaseService;

    public ClientiController(DatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Clienti>>> GetClienti()
    {
        var clienti = new List<Clienti>();
        using (var connection = _databaseService.GetConnection())
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand("SELECT * FROM Clienti", connection))
            {
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        clienti.Add(new Clienti
                        {
                            CodiceFiscale = reader.GetString(0),
                            Cognome = reader.GetString(1),
                            Nome = reader.GetString(2),
                            Citta = reader.GetString(3),
                            Provincia = reader.GetString(4),
                            Email = reader.GetString(5),
                            Telefono = reader.IsDBNull(6) ? null : reader.GetString(6),
                            Cellulare = reader.IsDBNull(7) ? null : reader.GetString(7),
                        });
                    }
                }
            }
        }
        return clienti;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Clienti>> GetCliente(string id)
    {
        Clienti cliente = null;
        using (var connection = _databaseService.GetConnection())
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand("SELECT * FROM Clienti WHERE CodiceFiscale = @id", connection))
            {
                command.Parameters.Add(new SqlParameter("id", id));
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        cliente = new Clienti
                        {
                            CodiceFiscale = reader.GetString(0),
                            Cognome = reader.GetString(1),
                            Nome = reader.GetString(2),
                            Citta = reader.GetString(3),
                            Provincia = reader.GetString(4),
                            Email = reader.GetString(5),
                            Telefono = reader.IsDBNull(6) ? null : reader.GetString(6),
                            Cellulare = reader.IsDBNull(7) ? null : reader.GetString(7),
                        };
                    }
                }
            }
        }

        if (cliente == null)
        {
            return NotFound();
        }
        return cliente;
    }

    [HttpPost]
    public async Task<ActionResult<Clienti>> PostCliente(Clienti cliente)
    {
        using (var connection = _databaseService.GetConnection())
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand("INSERT INTO Clienti (CodiceFiscale, Cognome, Nome, Citta, Provincia, Email, Telefono, Cellulare) VALUES (@CodiceFiscale, @Cognome, @Nome, @Citta, @Provincia, @Email, @Telefono, @Cellulare)", connection))
            {
                command.Parameters.Add(new SqlParameter("CodiceFiscale", cliente.CodiceFiscale));
                command.Parameters.Add(new SqlParameter("Cognome", cliente.Cognome));
                command.Parameters.Add(new SqlParameter("Nome", cliente.Nome));
                command.Parameters.Add(new SqlParameter("Citta", cliente.Citta));
                command.Parameters.Add(new SqlParameter("Provincia", cliente.Provincia));
                command.Parameters.Add(new SqlParameter("Email", cliente.Email));
                command.Parameters.Add(new SqlParameter("Telefono", cliente.Telefono ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("Cellulare", cliente.Cellulare ?? (object)DBNull.Value));

                await command.ExecuteNonQueryAsync();
            }
        }

        return CreatedAtAction(nameof(GetCliente), new { id = cliente.CodiceFiscale }, cliente);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCliente(string id, Clienti cliente)
    {
        if (id != cliente.CodiceFiscale)
        {
            return BadRequest();
        }

        using (var connection = _databaseService.GetConnection())
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand("UPDATE Clienti SET Cognome = @Cognome, Nome = @Nome, Citta = @Citta, Provincia = @Provincia, Email = @Email, Telefono = @Telefono, Cellulare = @Cellulare WHERE CodiceFiscale = @CodiceFiscale", connection))
            {
                command.Parameters.Add(new SqlParameter("CodiceFiscale", cliente.CodiceFiscale));
                command.Parameters.Add(new SqlParameter("Cognome", cliente.Cognome));
                command.Parameters.Add(new SqlParameter("Nome", cliente.Nome));
                command.Parameters.Add(new SqlParameter("Citta", cliente.Citta));
                command.Parameters.Add(new SqlParameter("Provincia", cliente.Provincia));
                command.Parameters.Add(new SqlParameter("Email", cliente.Email));
                command.Parameters.Add(new SqlParameter("Telefono", cliente.Telefono ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("Cellulare", cliente.Cellulare ?? (object)DBNull.Value));

                int rowsAffected = await command.ExecuteNonQueryAsync();
                if (rowsAffected == 0)
                {
                    return NotFound();
                }
            }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCliente(string id)
    {
        using (var connection = _databaseService.GetConnection())
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand("DELETE FROM Clienti WHERE CodiceFiscale = @id", connection))
            {
                command.Parameters.Add(new SqlParameter("id", id));
                int rowsAffected = await command.ExecuteNonQueryAsync();
                if (rowsAffected == 0)
                {
                    return NotFound();
                }
            }
        }

        return NoContent();
    }
}
