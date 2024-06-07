using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Sqlite;
using Questao5.Interface;

namespace Questao5.Infrastructure.Database
{
    public class IdempotenciaRepository : IRepository<Idempotencia>
    {
        private readonly DatabaseConfig databaseConfig;

        public IdempotenciaRepository(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        public async Task<string> Adicionar(Idempotencia idempotencia, CancellationToken cancellationToken)
        {
            using var connection = new SqliteConnection(databaseConfig.Name);
            var query = @"INSERT INTO idempotencia (Chave_Idempotencia, Requisicao, Resultado) 
                          VALUES (@Chave_Idempotencia, @Requisicao, @Resultado)";
            await connection.ExecuteAsync(query, idempotencia);
            return "Ok";
        }

        public async Task<Idempotencia> Consultar(string id, CancellationToken cancellationToken)
        {
            using var connection = new SqliteConnection(databaseConfig.Name);
            var query = $"SELECT chave_idempotencia, requisicao, resultado  FROM idempotencia  Where chave_idempotencia = '{id}'";
            var idempotencia = await connection.QuerySingleOrDefaultAsync<Idempotencia>(query);
            return idempotencia;
        }

        public Task<List<Idempotencia>> ConsultarTodos(string id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
