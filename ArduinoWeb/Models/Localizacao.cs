using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace ArduinoWeb.Models
{
    public class Localizacao
    {
        [Required]
        public int LocalizacaoId { get; set; }
        [Required]
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public virtual ICollection<RelatorioDispositivo> RelatorioDispositivo { get; set; }
        public virtual ICollection<Medicao> Medicoes { get; set; }
    }
}
