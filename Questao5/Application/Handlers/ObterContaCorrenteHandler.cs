using MediatR;
using Questao5.Application.Queries.Requests;
using Questao5.Domain.Entities;
using Questao5.Interface;

public class ObterContaCorrenteHandler : IRequestHandler<ObterContaCorrenteQuery, ContaCorrente>
{
    private readonly IRepository<ContaCorrente> _repository;

    public ObterContaCorrenteHandler(IRepository<ContaCorrente> repository)
    {
        _repository = repository;
    }

    public async Task<ContaCorrente> Handle(ObterContaCorrenteQuery request, CancellationToken cancellationToken)
    {
        return await _repository.Consultar(request.IdContaCorrente, cancellationToken);
    }
}