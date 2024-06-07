using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Queries.Requests;
namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ContaCorrenteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Obtem o saldo da conta bancária atual.
        /// </summary>

        [HttpGet("Saldo/{id}")]
        public async Task<IActionResult> CriarContaCorrente(string id, CancellationToken cancellationToken = default)
        {

            var response = await _mediator.Send(new ObterSaldoQuery(id));
            if (response.Sucesso != null)
            {
                return Ok(response.Sucesso);
            }

            return BadRequest(response.Error);
        }
    }
}
