using System.Globalization;

namespace Questao1
{
    public class ContaBancaria
    {
        private string Nome { get; set; }
        private int Conta { get; set; }
        private double Saldo { get; set; }

        public ContaBancaria(int conta, string nome, double depositoInicial = 0)
        {
            Nome = nome;
            Conta = conta;
            Saldo = depositoInicial;
        }

        public void Deposito(double valor)
        {
            this.Saldo += valor;
        } 

        public void Saque(double valor)
        {
            this.Saldo -= (valor + 3.5);
        }

        public void Alterar(string nome)
        {
            this.Nome = nome;
        }

        public override string ToString()
        {
            return $"Conta {this.Conta}, Titular: {this.Nome}, Saldo: $ {this.Saldo.ToString("F2")}";
        }
    }
}
