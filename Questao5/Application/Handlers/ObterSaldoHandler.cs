using MediatR;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Interface;

namespace Questao5.Application.Handlers
{
    public class ObterSaldoHandler : IRequestHandler<ObterSaldoQuery, ObterSaldoQueryResponse>
    {
        private readonly IMediator _mediator;
        private IRepository<Movimento> _repository;

        public ObterSaldoHandler(IMediator mediator, IRepository<Movimento> repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<ObterSaldoQueryResponse> Handle(ObterSaldoQuery request, CancellationToken cancellationToken)
        {
            var response = new ObterSaldoQueryResponse();
             var contaCorrente = await _mediator.Send(new ObterContaCorrenteQuery(request.IdContaCorrente));

            if (contaCorrente == null)
            {
                response.Error = new SaldoQueryError
                {
                    TipoFalha = EnumTipoRetorno.INVALID_ACCOUNT.ToString(),
                    Descricao = "Conta inexistente"
                };
                return response;
            }
            else if(contaCorrente.Ativo == 0)
            {
                response.Error = new SaldoQueryError
                {
                    TipoFalha = EnumTipoRetorno.INACTIVE_ACCOUNT.ToString(),
                    Descricao = "Conta inátiva"
                };

                return response;
            }

            var listMovimentos = await _repository.ConsultarTodos(request.IdContaCorrente, cancellationToken);

            response.Sucesso = new SaldoQuerySucesso
            {
                NomeTitular = contaCorrente.Nome,
                NumeroContaCorrente = contaCorrente.Numero.ToString(),
                Data = DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
                SaldoAtual = (0M).ToString("C")
            };
                
            if(listMovimentos != null && listMovimentos.Any()) 
            {
                var saldoCredito = (decimal?)listMovimentos.Where(x => x.TipoMovimento == "C").Sum(x => x.Valor) ?? 0;
                var saldoDebito = (decimal?)listMovimentos.Where(x => x.TipoMovimento == "D").Sum(x => x.Valor) ?? 0;
                response.Sucesso.SaldoAtual = Math.Round(saldoCredito - saldoDebito, 2).ToString("C");
            }

            return response;
        }
    }
}
