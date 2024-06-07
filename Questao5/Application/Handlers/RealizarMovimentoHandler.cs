using MediatR;
using Newtonsoft.Json;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Queries.Requests;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Interface;

namespace Questao5.Application.Handlers
{
    public class RealizarMovimentoHandler : IRequestHandler<RealizarMovimentoCommand, RealizarMovimentoResponse>
    {
        private readonly IMediator _mediator;
        private readonly IRepository<Movimento> _repository;
        private readonly IRepository<Idempotencia> _repositoryIdempotencia;

        public RealizarMovimentoHandler(IMediator mediator, IRepository<Movimento> repository, IRepository<Idempotencia> repositoryIdempotencia)
        {
            _mediator = mediator;
            _repository = repository;
            _repositoryIdempotencia = repositoryIdempotencia;
        }

        public async Task<RealizarMovimentoResponse> Handle(RealizarMovimentoCommand request, CancellationToken cancellationToken)
        {
            var response = new RealizarMovimentoResponse();
            var idempotencia = await _repositoryIdempotencia.Consultar(request.IdRequisicao, cancellationToken);

            if (idempotencia != null)
            {
                response = JsonConvert.DeserializeObject<RealizarMovimentoResponse>(idempotencia.Resultado);
                return response;
            }

            var contaCorrente = await _mediator.Send(new ObterContaCorrenteQuery(request.IdContaCorrente));

            if (contaCorrente == null)
            {
                response.Tipo = EnumTipoRetorno.INVALID_ACCOUNT.ToString();
                response.Descricao = "Conta Inexistente";
            }
            else if(contaCorrente.Ativo == 0)
            {
                response.Tipo = EnumTipoRetorno.INACTIVE_ACCOUNT.ToString();
                response.Descricao = "Conta Inátiva";
            }
            else if(request.TipoMovimento != "C" && request.TipoMovimento != "D")
            {
                response.Tipo = EnumTipoRetorno.INVALID_TYPE.ToString();
                response.Descricao = "O tipo informado deve ser C ou D";
            }
            else if(request.Valor <= 0)
            {
                response.Tipo = EnumTipoRetorno.INVALID_VALUE.ToString();
                response.Descricao = "Valores menores ou iguais a zero não são aceitos.";
            }

            if (!string.IsNullOrEmpty(response.Tipo))
            {
                response.Sucesso = false;
            }
            else
            {
                try
                {
                    var movimento = new Movimento
                    {
                        IdContaCorrente = request.IdContaCorrente,
                        DataMovimento = DateTime.Now.ToString(),
                        TipoMovimento = request.TipoMovimento,
                        Valor = request.Valor
                    };

                    var result = await _repository.Adicionar(movimento, cancellationToken);

                    response.Sucesso = true;
                    response.IdMovimento = result;
                }
                catch (Exception ex)
                {
                    response.Sucesso = false;
                    response.Tipo = EnumTipoRetorno.ERROR.ToString();
                    response.Descricao = ex.Message;
                }       
            }

            idempotencia = new Idempotencia
            {
                Chave_Idempotencia = request.IdRequisicao,
                Requisicao = JsonConvert.SerializeObject(request),
                Resultado = JsonConvert.SerializeObject(response)
            };

            await _repositoryIdempotencia.Adicionar(idempotencia, cancellationToken);

            return response;
        }
    }

}
