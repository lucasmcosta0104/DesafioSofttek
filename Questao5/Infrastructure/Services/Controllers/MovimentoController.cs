using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovimentoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MovimentoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Adiciona uma movimentação bancária de crédito ou débito
        /// </summary>

        [HttpPost]
        public async Task<IActionResult> CriarMovimento(RealizarMovimentoCommand command)
        {
            var response = await _mediator.Send(command);
            if (response.Sucesso)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}
