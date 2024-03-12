using System.ComponentModel.DataAnnotations;

namespace ArduinoWeb.Models
{
    public class Medicao
    {
        [Required]
        public int MedicaoId { get; set; }
        [Required]
        public int RelatorioDispositivoId { get; set; }
        [Required]
        public int TipoMedicaoId { get; set; }
        [Required]
        public int LocalizacaoId { get; set; }
        [Required]
        public decimal ValorLido { get; set; }
        [Required]
        public System.DateTime DataMedicao { get; set; }
        public virtual RelatorioDispositivo RelatorioDispositivo { get; set; }
        public virtual TipoMedicao TipoMedicao { get; set; }
        public virtual Localizacao Localizacao { get; set; }
    }
}
