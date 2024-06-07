using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Sqlite;
using Questao5.Interface;

namespace Questao5.Infrastructure.Database
{
    public class ContaCorrenteRepository : IRepository<ContaCorrente>
    {
        private readonly DatabaseConfig databaseConfig;

        public ContaCorrenteRepository(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;

        }

        public Task<string> Adicionar(ContaCorrente item, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<ContaCorrente> Consultar(string id, CancellationToken cancellationToken)
        {
            using var connection = new SqliteConnection(databaseConfig.Name);
            var query = $"SELECT IdContaCorrente, Numero, Nome, Ativo FROM contacorrente Where IdContaCorrente = '{id}'";
            var contasCorrente = await connection.QuerySingleOrDefaultAsync<ContaCorrente>(query);
            return contasCorrente;
        }

        public Task<List<ContaCorrente>> ConsultarTodos(string id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
