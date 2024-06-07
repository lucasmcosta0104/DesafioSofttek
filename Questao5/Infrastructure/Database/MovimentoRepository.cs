using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Sqlite;
using Questao5.Interface;
using Dapper;

namespace Questao5.Infrastructure.Database
{
    public class MovimentoRepository : IRepository<Movimento>
    {
        private readonly DatabaseConfig databaseConfig;

        public MovimentoRepository(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
            
        }

        public async Task<string> Adicionar(Movimento movimento, CancellationToken cancellationToken)
        {
            using var connection = new SqliteConnection(databaseConfig.Name);
            var query = @"
                INSERT INTO movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor) 
                VALUES (@IdMovimento, @IdContaCorrente, @DataMovimento, @TipoMovimento, @Valor)";

            var parameters = new
            {
                IdMovimento = Guid.NewGuid().ToString(),
                IdContaCorrente = movimento.IdContaCorrente,
                DataMovimento = movimento.DataMovimento,
                TipoMovimento = movimento.TipoMovimento,
                Valor = movimento.Valor
            };

            var result = await connection.ExecuteAsync(query, parameters);
            if (result > 0)
                return parameters.IdMovimento;

            return string.Empty;
        }

        public Task<Movimento> Consultar(string id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Movimento>> ConsultarTodos(string idContaCorrente, CancellationToken cancellationToken)
        {
            using var connection = new SqliteConnection(databaseConfig.Name);
            var query = $"SELECT * FROM movimento  Where IdContaCorrente = '{idContaCorrente}'";
            var movimentos = await connection.QueryAsync<Movimento>(query);
            return movimentos.ToList();
        }
    }
}
