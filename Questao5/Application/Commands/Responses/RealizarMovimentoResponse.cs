namespace Questao5.Application.Commands.Responses
{
    public class RealizarMovimentoResponse
    {
        public string IdMovimento { get; set; }
        public string Tipo { get; set; }
        public string Descricao { get; set; }
        public bool Sucesso { get; set; }
    }
}
