using ArduinoWeb.Models;

namespace ArduinoWeb.ViewModels
{
    public class DispositivoVm
    {
        public int? RelatorioDispositivoId { get; set; }
        public string TipoNome { get; set; }
        public string LocalizacaoNome { get; set; }
        public string LocalIp { get; set; }
        public List<RelatorioDispositivo> RelatorioDispositivos { get; set; } = new List<RelatorioDispositivo>();
        public List<Localizacao> Localizacoes { get; set; } = new List<Localizacao>();
        public MedicaoVm LastSet { get; set; } = new MedicaoVm();
    }
}
