namespace GestioneHotel.Models
{
    public class ServiziAggiuntivi
    {
        public int ID { get; set; }
        public DateTime Data { get; set; }
        public int Quantita { get; set; }
        public decimal Prezzo { get; set; }
        public string TipoServizio { get; set; }
        public int PrenotazioneID { get; set; }
    }
}
