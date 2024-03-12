using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ArduinoWeb.ViewModels
{
    public class LocalizacaoHandlerVm
    {
        public int RelatorioDispositivoId { get; set; }

        public string DispositivoNome { get; set; }

        [DisplayName("Nome da Localizacao")]
        [Required(ErrorMessage = "Insira uma localização")]
        public string NomeLocalizacao { get; set; }

        [DisplayName("Descrição da Localização")]
        public string LocalizacaoDescricao { get; set; }

        public bool Sucesso { get; set; }

        public string Mensagem { get; set; }
    }
}
