namespace GestioneHotel.Models
{
    public class ServiziAggiuntivi
    {
        public int Id { get; set; }
        public int PrenotazioneId { get; set; }
        public string Descrizione { get; set; }
        public DateTime Data { get; set; }
        public int Quantita { get; set; }
        public decimal Prezzo { get; set; }
    }
}
