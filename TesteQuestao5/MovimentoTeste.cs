using Questao5.Domain.Entities;

namespace TesteQuestao5
{
    public class MovimentoTeste
    {
        [Fact]
        public void Movimento_IdMovimento_DeveSerValidoQuandoTamanhoCorreto()
        {
            var idMovimento = "12345678901234567890123456789012345";
            var movimento = new Movimento();

            movimento.IdMovimento = idMovimento;

            Assert.Equal(idMovimento, movimento.IdMovimento);
        }

        [Fact]
        public void Movimento_IdContaCorrente_DeveSerValidoQuandoTamanhoCorreto()
        {
            var idContaCorrente = "12345678901234567890123456789012345";
            var movimento = new Movimento();

            movimento.IdContaCorrente = idContaCorrente;

            Assert.Equal(idContaCorrente, movimento.IdContaCorrente);
        }

        [Fact]
        public void Movimento_DataMovimento_DeveSerValidaQuandoDentroDoTamanhoLimite()
        {
            var movimento = new Movimento();
            var dataMovimento = new string('0', 25); 

            movimento.DataMovimento = dataMovimento;

            Assert.Equal(dataMovimento, movimento.DataMovimento);
        }

        [Fact]
        public void Movimento_TipoMovimento_DeveSerValidoQuandoCorrespondeAoPadrao()
        {
            var movimento = new Movimento();
            var tipoMovimento = "C"; 

            movimento.TipoMovimento = tipoMovimento;

            Assert.Equal(tipoMovimento, movimento.TipoMovimento);
        }

        [Fact]
        public void Movimento_Valor_DeveSerValidoQuandoDentroDoTipoEscala()
        {
            var movimento = new Movimento();
            var valor = 123.45m;

            movimento.Valor = valor;

            Assert.Equal(valor, movimento.Valor);
        }
    }
}
