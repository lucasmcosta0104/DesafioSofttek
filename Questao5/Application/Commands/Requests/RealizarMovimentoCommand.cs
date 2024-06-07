using MediatR;
using Questao5.Application.Commands.Responses;
using System.ComponentModel.DataAnnotations;

namespace Questao5.Application.Commands.Requests
{
    public class RealizarMovimentoCommand : IRequest<RealizarMovimentoResponse>
    {
        public string IdRequisicao { get; set; }
        public string IdContaCorrente { get; set; }
        public decimal Valor { get; set; }
        [MaxLength(1)]
        public string TipoMovimento { get; set; }
    }
}
