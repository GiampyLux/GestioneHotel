namespace GestioneHotel.Models
{
    public class Prenotazioni
    {
        public int Id { get; set; }
        public string CodiceFiscaleCliente { get; set; }
        public int NumeroCamera { get; set; }
        public DateTime DataPrenotazione { get; set; }
        public int NumeroProgressivoAnno { get; set; }
        public int Anno { get; set; }
        public DateTime Dal { get; set; }
        public DateTime Al { get; set; }
        public decimal CaparraConfirmatoria { get; set; }
        public decimal TariffaApplicata { get; set; }
        public string DettagliSoggiorno { get; set; }
    }
}
