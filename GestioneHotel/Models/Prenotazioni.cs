namespace GestioneHotel.Models
{
    public class Prenotazioni
    {
        public int ID { get; set; }
        public DateTime DataPrenotazione { get; set; }
        public int NumeroProgressivo { get; set; }
        public int Anno { get; set; }
        public DateTime PeriodoDal { get; set; }
        public DateTime PeriodoAl { get; set; }
        public decimal Caparra { get; set; }
        public decimal Tariffa { get; set; }
        public string TipoSoggiorno { get; set; }
        public string CodiceFiscaleCliente { get; set; }
        public int NumeroCamera { get; set; }
    }
}
