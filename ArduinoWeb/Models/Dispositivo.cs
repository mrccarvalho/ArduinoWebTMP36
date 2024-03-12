using System.ComponentModel.DataAnnotations;

namespace ArduinoWeb.Models
{
    public class Dispositivo
    {
        [Required]
        public int DispositivoId { get; set; }
        [Required]
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public virtual ICollection<RelatorioDispositivo> RelatorioDispositivos { get; set; }
    }
}
