using GestioneHotel.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace GestioneHotel.Services
{
    public class ServiziAggiuntiviService
    {
        private readonly DatabaseService _databaseService;

        public ServiziAggiuntiviService(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<List<ServiziAggiuntivi>> GetAllServiziAggiuntiviAsync()
        {
            var servizi = new List<ServiziAggiuntivi>();

            using (var connection = _databaseService.GetConnection())
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("SELECT * FROM ServiziAggiuntivi", connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            servizi.Add(new ServiziAggiuntivi
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                PrenotazioneId = reader.GetInt32(reader.GetOrdinal("PrenotazioneId")),
                                Descrizione = reader.GetString(reader.GetOrdinal("Descrizione")),
                                Data = reader.GetDateTime(reader.GetOrdinal("Data")),
                                Quantita = reader.GetInt32(reader.GetOrdinal("Quantita")),
                                Prezzo = reader.GetDecimal(reader.GetOrdinal("Prezzo"))
                            });
                        }
                    }
                }
            }

            return servizi;
        }

        public async Task<ServiziAggiuntivi> GetServiziAggiuntiviByIdAsync(int id)
        {
            ServiziAggiuntivi servizio = null;

            using (var connection = _databaseService.GetConnection())
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("SELECT * FROM ServiziAggiuntivi WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            servizio = new ServiziAggiuntivi
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                PrenotazioneId = reader.GetInt32(reader.GetOrdinal("PrenotazioneId")),
                                Descrizione = reader.GetString(reader.GetOrdinal("Descrizione")),
                                Data = reader.GetDateTime(reader.GetOrdinal("Data")),
                                Quantita = reader.GetInt32(reader.GetOrdinal("Quantita")),
                                Prezzo = reader.GetDecimal(reader.GetOrdinal("Prezzo"))
                            };
                        }
                    }
                }
            }

            return servizio;
        }

        public async Task AddServiziAggiuntiviAsync(ServiziAggiuntivi servizio)
        {
            using (var connection = _databaseService.GetConnection())
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("INSERT INTO ServiziAggiuntivi (PrenotazioneId, Descrizione, Data, Quantita, Prezzo) VALUES (@PrenotazioneId, @Descrizione, @Data, @Quantita, @Prezzo)", connection))
                {
                    command.Parameters.AddWithValue("@PrenotazioneId", servizio.PrenotazioneId);
                    command.Parameters.AddWithValue("@Descrizione", servizio.Descrizione);
                    command.Parameters.AddWithValue("@Data", servizio.Data);
                    command.Parameters.AddWithValue("@Quantita", servizio.Quantita);
                    command.Parameters.AddWithValue("@Prezzo", servizio.Prezzo);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<bool> UpdateServizioAggiuntivoAsync(int id, ServiziAggiuntivi servizio)
        {
            var rowsAffected = 0;

            using (var connection = _databaseService.GetConnection())
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("UPDATE ServiziAggiuntivi SET PrenotazioneId = @PrenotazioneId, Descrizione = @Descrizione, Data = @Data, Quantita = @Quantita, Prezzo = @Prezzo WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@PrenotazioneId", servizio.PrenotazioneId);
                    command.Parameters.AddWithValue("@Descrizione", servizio.Descrizione);
                    command.Parameters.AddWithValue("@Data", servizio.Data);
                    command.Parameters.AddWithValue("@Quantita", servizio.Quantita);
                    command.Parameters.AddWithValue("@Prezzo", servizio.Prezzo);

                    rowsAffected = await command.ExecuteNonQueryAsync();
                }
            }

            return rowsAffected > 0;
        }

        public async Task<bool> DeleteServizioAggiuntivoAsync(int id)
        {
            var rowsAffected = 0;

            using (var connection = _databaseService.GetConnection())
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("DELETE FROM ServiziAggiuntivi WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    rowsAffected = await command.ExecuteNonQueryAsync();
                }
            }

            return rowsAffected > 0;
        }
    }
}
