namespace ArduinoWeb.ViewModels
{
    public class MedicaoVm
    {
        public DateTime? DataMedicao { get; set; }
        public string NomeLocalizacao { get; set; }
        public decimal? Temperatura { get; set; }

        public string DateOnlyString => DataMedicao?.ToString("yyyy-MM-dd") ?? string.Empty;
        public string TimeOnlyString => DataMedicao?.ToString("hh:mm:ss tt") ?? string.Empty;

        public string GoogleDate => (DataMedicao.HasValue)
          ? string.Format("Date({0})", DataMedicao.Value.ToString("yyyy,M,d,H,m,s,f"))
          : string.Empty;

        public string TempString => (Temperatura != null) ? Temperatura.Value.ToString("###.0") : "0.0";

    }
}
