using NSubstitute;
using Questao5.Domain.Entities;


namespace TesteQuestao5
{
    public class ContaCorrenteTeste
    {
        [Fact]
        public void ContaCorrente_Movimentos_ValidaInstaciaObjeto()
        {
            var mockMovimentos = Substitute.For<ICollection<Movimento>>();
            var contaCorrente = new ContaCorrente { Movimentos = mockMovimentos };

            Assert.NotNull(contaCorrente.Movimentos);
        }

        [Fact]
        public void ContaCorrente_IdContaCorrente_ValidarQuatidadeCaracteres()
        {
            var idContaCorrente = "12345678901234567890123456789012345"; 
            var contaCorrente = new ContaCorrente();

            contaCorrente.IdContaCorrente = idContaCorrente;

            Assert.Equal(idContaCorrente, contaCorrente.IdContaCorrente);
        }

        [Fact]
        public void ContaCorrente_Nome_ValidarMaximoCaracteres()
        {

            var mockMovimentos = Substitute.For<ICollection<Movimento>>();
            var contaCorrente = new ContaCorrente { Movimentos = mockMovimentos };
            var nome = new string('a', 100);

            contaCorrente.Nome = nome;

            Assert.Equal(nome, contaCorrente.Nome);
        }
    }
}
