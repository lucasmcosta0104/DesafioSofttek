using Castle.DynamicProxy;

namespace Questao5.Application.Queries.Responses
{
    public class ObterSaldoQueryResponse
    {
        public SaldoQuerySucesso Sucesso { get; set; }
        public SaldoQueryError Error { get; set; }
    }

    public class SaldoQuerySucesso 
    {
        public string NumeroContaCorrente { get; set; }
        public string NomeTitular { get; set; }
        public string Data { get; set; }
        public string SaldoAtual { get; set; }
    }

    public class SaldoQueryError
    {
        public string TipoFalha { get; set; }
        public string Descricao { get; set; }
    }
}
