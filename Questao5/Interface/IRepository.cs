namespace Questao5.Interface
{
    public interface IRepository<T>
    {
        Task<string> Adicionar(T item, CancellationToken cancellationToken);
        Task<T> Consultar(string id, CancellationToken cancellationToken);
        Task<List<T>> ConsultarTodos(string id, CancellationToken cancellationToken);
    }
}
