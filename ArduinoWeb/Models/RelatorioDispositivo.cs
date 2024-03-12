using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace ArduinoWeb.Models
{
    public class RelatorioDispositivo
    {
        [Required]
        public int RelatorioDispositivoId { get; set; }
        [Required]
        public int DispositivoId { get; set; }
        [Required]
        public int LocalizacaoId { get; set; }
        [Required]
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string UltimoIpAddress { get; set; }
        public virtual Dispositivo Dispositivo { get; set; }
        public virtual Localizacao Localizacao { get; set; }
        public virtual ICollection<Medicao> Medicoes { get; set; }
    }
}
