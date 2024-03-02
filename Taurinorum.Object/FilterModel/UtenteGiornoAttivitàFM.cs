namespace Taurinorum.Object.FilterModel
{
    public class UtenteGiornoAttivitàFM : BaseFM
    {
        public int IdAttività { get; set; }
        public int IdUtente { get; set; } = 0;
        public bool Lunedi { get; set; } = false;
        public bool Martedi { get; set; } = false;
        public bool Mercoledi { get; set; } = false;
        public bool Giovedi { get; set; } = false;
        public bool Venerdi { get; set; } = false;
        public bool Sabato { get; set; } = false;
        public bool Domenica { get; set; } = false;
        public double Paga { get; set; } = 0;
    }
}
