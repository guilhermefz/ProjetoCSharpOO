using ProjetoCSharp.Entidades;
using System.Security.AccessControl;

namespace ProjetoCSharp
{
    public class Program
    {

        const string bancoText = @"C:\Users\Guilherme.Campos\source\repos\ProjetoCSharp\arquivo.txt";
        
        static void Main(string[] args)
        {
            List<Produto> produtos = new List<Produto>();
            List<Cliente> clientes = new List<Cliente>();
            List<ContaBancaria> contas = new List<ContaBancaria>();
            Cliente clienteCarregar = new Cliente();


            Produto produtoCarregar = new ProdutoUsado();
            produtoCarregar.CarregarProdutos(produtos, bancoText);

            Produto produtoCar = new Produto();
            produtoCar.CarregarProdutos(produtos, bancoText);
            
            Produto produtoCarr = new ProdutoImportado();
            produtoCarr.CarregarProdutos(produtos, bancoText);


            CarregarContas(contas);
            clienteCarregar.CarregarClientes(clientes, bancoText);
            int escolha = 0;
            int final;


            while (escolha == 0)
            {
                Console.WriteLine("**************MENU*************");
                Console.WriteLine("Escolha a operação desejada!");
                Console.WriteLine("Menu de Contas - 1");
                Console.WriteLine("Menu de Clientes - 2");
                Console.WriteLine("Sair - 0");

                int menuPrincipal = Convert.ToInt32(Console.ReadLine());
                if (menuPrincipal == 1)
                {
                    do
                    {
                        Console.WriteLine("**************MENU*************");
                        Console.WriteLine("Escolha a operação desejada!");
                        Console.WriteLine("Criar uma conta - 1");
                        Console.WriteLine("Realizar um deposito - 2");
                        Console.WriteLine("REalizar um saque - 3");
                        Console.WriteLine("Listar contas - 4");
                        Console.WriteLine("Menu de Produtos - 5");

                        int escolha1 = Convert.ToInt32(Console.ReadLine());



                        switch (escolha1)

                        {

                            case 1:

                                Console.WriteLine("--------------------------");

                                Console.WriteLine("CRIAÇÃO DA CONTA");

                                Console.WriteLine("--------------------------");

                                Console.WriteLine("Digite o numero da conta");

                                string numConta = Console.ReadLine();

                                Console.WriteLine("Digite o seu nome");

                                string nome = Console.ReadLine();



                                var contaNova = new ContaBancaria(numConta, nome);

                                contas.Add(contaNova);



                                Console.WriteLine("\nConta Criada!!!");


                                Console.WriteLine("Esta conta terá deposito inicial? Sim-1, Não-0");
                                int depositoInicial = Convert.ToInt32(Console.ReadLine());
                                if (depositoInicial == 1)
                                {
                                    Console.WriteLine("Quanto você deseja depositar?");

                                    double valorDeposito1 = Convert.ToDouble(Console.ReadLine());



                                    ContaBancaria contaDepositada1 = contas.FirstOrDefault(c => c.NumeroConta == numConta);

                                    contaDepositada1.SetDeposito(valorDeposito1);


                                    Console.WriteLine($"\nOlá {contaDepositada1.Nome}, o seu Saldo atual é de: {contaDepositada1.Saldo:F2}");

                                    CriarConta(numConta, nome, contaDepositada1.Saldo);
                                    Console.WriteLine("DEPOSITADO COM SUCESSO!!!!!!!!!!");
                                }
                                else
                                {
                                    CriarConta(numConta, nome, 0);
                                }

                                Console.WriteLine("\nOk, Deseja continuar? Sim-1, Não-0");
                                final = Convert.ToInt32(Console.ReadLine());

                                escolha = (final == 1) ? 1 : 0;

                                Console.Clear();
                                break;



                            case 2:

                                Console.WriteLine("--------------------------");

                                Console.WriteLine("DEPOSITO");

                                Console.WriteLine("--------------------------");

                                Console.WriteLine("Digite o numero da conta:");

                                string numContaDeposito = Console.ReadLine();

                                Console.WriteLine("Quanto você deseja depositar?");

                                double valorDeposito = Convert.ToDouble(Console.ReadLine());



                                ContaBancaria contaDepositada = contas.FirstOrDefault(c => c.NumeroConta == numContaDeposito);

                                contaDepositada.SetDeposito(valorDeposito);
                                SalvarBanco(contas);

                                Console.WriteLine("DEPOSITADO COM SUCESSO!!!!!!!!!!");
                                Console.WriteLine($"\nOlá {contaDepositada.Nome}, o seu Saldo atual é de: {contaDepositada.Saldo:F2}");



                                Console.WriteLine("\nDeseja continuar? Sim-1, Não-0");

                                final = Convert.ToInt32(Console.ReadLine());

                                escolha = (final == 1) ? 1 : 0;



                                break;



                            case 3:
                                Console.WriteLine("--------------------------");
                                Console.WriteLine("SAQUE");
                                Console.WriteLine("--------------------------");
                                Console.WriteLine("Digite o numero da conta:");
                                string numContaSaque = Console.ReadLine().Trim();

                                Console.WriteLine("\nQuanto você deseja sacar?");
                                double valorSaque = Convert.ToDouble(Console.ReadLine());


                                ContaBancaria contaSaque = contas.FirstOrDefault(c => c.NumeroConta == numContaSaque);
                                if (contaSaque == null)
                                {
                                    Console.WriteLine("Conta não encontrada.");
                                }
                                contaSaque?.SetSaque(valorSaque);
                                SalvarBanco(contas);
                                foreach (var cont in contas)
                                {
                                    Console.WriteLine($"Numero da conta: {cont.NumeroConta}, Nome: {cont.Nome}, Saldo: {cont.Saldo};");
                                }
                                Console.WriteLine("SAQUE COM SUCESSO!!!!!!!!!!");
                                Console.WriteLine($"\nOlá {contaSaque?.Nome}, o seu Saldo atual é de: {contaSaque?.Saldo:F2}");

                                Console.WriteLine("\nDeseja continuar? Sim-1, Não-0");
                                final = Convert.ToInt32(Console.ReadLine());
                                escolha = (final == 1) ? 1 : 0;



                                break;

                            case 4:
                                foreach (var cont in contas)
                                {
                                    Console.WriteLine($"Numero da conta: {cont.NumeroConta}, Nome: {cont.Nome}, Saldo: {cont.Saldo};");
                                }
                                Console.WriteLine("\nDeseja continuar? Sim-1, Não-0");

                                final = Convert.ToInt32(Console.ReadLine());

                                escolha = (final == 1) ? 1 : 0;
                                break;

                            case 5:
                                escolha = 0;
                                break;

                            default:
                                Console.WriteLine("Opção inválida, tente novamente.");
                                break;


                        }

                        Console.Clear();

                    } while (escolha == 1);
                }
                if (menuPrincipal == 2)
                {


                    do
                    {
                        Console.WriteLine("**************MENU*************");
                        Console.WriteLine("Escolha a operação desejada!");
                        Console.WriteLine("Cadastrar cliente - 1");
                        Console.WriteLine("Listar clientes - 2");
                        Console.WriteLine("cadastrar Produtos - 3");
                        Console.WriteLine("Listar produtos - 4");
                        Console.WriteLine("Criar um novo pedido - 5");
                        escolha = Convert.ToInt32(Console.ReadLine());

                        switch (escolha)
                        {
                            case 1:
                                Console.WriteLine("Cadastrar cliente");
                                Console.Write("\n Nome:");
                                string nome = Console.ReadLine();
                                Console.Write("\n Email:");
                                string email = Console.ReadLine();
                                Console.Write("\n Data de Nascimento no formato (dd/mm/yyyy):");
                                DateOnly dataNascimento = DateOnly.Parse(Console.ReadLine());
                                Cliente cliente = new Cliente(nome, email, dataNascimento);
                                cliente.CriarCliente(bancoText, cliente);
                                clientes.Add(cliente);
                                Console.WriteLine("\nCliente cadastrado com sucesso!");
                                Console.WriteLine("\nDeseja continuar? Sim-1, Não-0");

                                final = Convert.ToInt32(Console.ReadLine());

                                escolha = (final == 1) ? 1 : 0;
                                break;

                            case 2:
                                Console.WriteLine("Listar clientes");
                                foreach (var cli in clientes)
                                {
                                    Console.WriteLine($"Nome: {cli.Nome}, Email: {cli.Email}, Data de Nascimento: {cli.DataNascimento}");
                                }

                                Console.WriteLine("\nDeseja continuar? Sim-1, Não-0");

                                final = Convert.ToInt32(Console.ReadLine());

                                escolha = (final == 1) ? 1 : 0;
                                break;
                            case 3:
                                Console.WriteLine("Cadastrar Produtos");
                                Console.WriteLine("O Produto é novo(n), Usado(u) ou Importado(i)?");
                                char tipoProduto = char.Parse(Console.ReadLine().ToLower());
                                Console.Write("\n Nome do Produto:");
                                string nomeProduto = Console.ReadLine();
                                Console.Write("\n Preço do Produto:");
                                double precoProduto = Convert.ToDouble(Console.ReadLine());
                                if (tipoProduto == 'n')
                                {
                                    Produto produto = new Produto(nomeProduto, precoProduto);
                                    produto.CriarProduto(bancoText, produto);
                                    produtos.Add(produto);
                                }else if (tipoProduto == 'u')
                                {
                                    Console.Write("\n Data de Fabricação no formato (dd/mm/yyyy):");
                                    DateOnly dataFabricacao = DateOnly.Parse(Console.ReadLine());
                                    Produto produto = new ProdutoUsado(nomeProduto, precoProduto, dataFabricacao);
                                    produto.CriarProduto(bancoText, produto);
                                    produtos.Add(produto);
                                }
                                else
                                {
                                    Console.Write("\n Taxa Alfândega:");
                                    double taxaAlfandega = Convert.ToDouble(Console.ReadLine());
                                    Produto produto = new ProdutoImportado(nomeProduto, precoProduto, taxaAlfandega);
                                    produto.CriarProduto(bancoText, produto);
                                    produtos.Add(produto);
                                }
                                Console.WriteLine("\nProduto cadastrado com sucesso!");
                                Console.WriteLine("\nDeseja continuar? Sim-1, Não-0");
                                final = Convert.ToInt32(Console.ReadLine());
                                escolha = (final == 1) ? 1 : 0;
                                Console.Clear();
                                break;

                            case 4:
                                Console.WriteLine("Listar Produtos");
                                foreach (var p in produtos)
                                {
                                    Console.WriteLine(p.Etiqueta());
                                }
                                Console.WriteLine("\nDeseja continuar? Sim-1, Não-0");
                                final = Convert.ToInt32(Console.ReadLine());
                                escolha = (final == 1) ? 1 : 0;
                                break;


                                Console.Clear();
                        }
                    } while (escolha == 1);
                }
                else
                    escolha = 2;
            }
        }
        static void SalvarBanco(List<ContaBancaria> contas)
        {
            using (StreamWriter sw = new StreamWriter(bancoText, false))
            {   foreach (var conta in contas)
                {
                    sw.WriteLine($"CONTA: Numero da conta: {conta.NumeroConta}, Nome: {conta.Nome}, Saldo: {conta.Saldo};");
                }
            }
        }
        static void CriarConta(string numConta, string nome, double saldo)
        {
            using (StreamWriter sw = File.AppendText(bancoText))
            {
                    sw.WriteLine($"CONTA: Numero da conta: {numConta}, Nome: {nome}, Saldo: {saldo};");
            }
        }

        static ContaBancaria LerBanco(string linha)
        {
            string prefixo = "CONTA:"; //Prefixo que identifica o início de uma linha de conta no arquivo.
            string linhaArquivo = linha.Substring(prefixo.Length);
            linhaArquivo.Substring(0, 5);
            string[] blocos = linhaArquivo.Split(','); //Dividir a linha em blocos usando a vírgula como separador.
            string numeroContaStr = blocos[0].Split(':')[1].Trim(); //Extrair o número da conta do primeiro bloco.
            string nomeStr = blocos[1].Split(':')[1].Trim(); //Extrair o nome do segundo bloco.
            string blocoSaldoLimpo = blocos[2].Replace(";", "").Trim(); //Remover o ponto e vírgula do final do terceiro bloco.
            string saldoStr = blocoSaldoLimpo.Split(':')[1].Trim(); //Extrair o saldo do terceiro bloco.

            double saldo = double.Parse(saldoStr);

            ContaBancaria conta = new ContaBancaria(numeroContaStr, nomeStr);
            conta.SetDeposito(saldo); //Definir o saldo da conta usando o método SetDeposito.

            return conta;
        }

        public static string LinhaArquivo(string numConta)
        {
            using (StreamReader sr = new StreamReader(bancoText)) 
            {
                string linha;
                while ((linha = sr.ReadLine()) != null) 
                {
                    if (linha.Contains(numConta))
                    {
                        return linha; // Retorna a linha que contém o número da conta.
                    }
                }
            }
            return null; // Retorna null se não encontrar a conta.
        }

        static void CarregarContas(List<ContaBancaria> contas)
        {
            if (File.Exists(bancoText))
            {
                using (StreamReader sr = new StreamReader(bancoText))
                {
                    string linha;
                    while((linha = sr.ReadLine()) != null)
                    {
                        if (linha.StartsWith("CONTA:"))
                        {
                            ContaBancaria conta = LerBanco(linha);
                            if (conta != null) contas.Add(conta);
                        }

                    }
                }
            }
        }
    }
}
