namespace ProjetoCSharp.Entidades
{
    public class ContaBancaria
    {
        public string Id { get; set; }

        public string Nome { get; set; }

        public string NumeroConta { get; set; }

        public double Saldo { get; private set; }

        public ContaBancaria(string numeroConta, string nome)
        {
            NumeroConta = numeroConta;
            Nome = nome;
        }
        public ContaBancaria()
        {
        }

        public void SetDeposito(double saldo)

        {

            Saldo += saldo;

        }

        public void SetSaque(double saldo)

        {

            Saldo -= saldo;

        }
    }
}
