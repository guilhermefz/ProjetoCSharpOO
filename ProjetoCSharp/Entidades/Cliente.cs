namespace ProjetoCSharp.Entidades
{
    public class Cliente
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateOnly DataNascimento { get; set; }

        public Cliente (string nome, string email, DateOnly nascimento)
        {
            Nome = nome;
            Email = email;
            DataNascimento = nascimento;
        }
        public Cliente()
        {
        }
        public void CarregarClientes(List<Cliente> clientes, string caminhoText)
        {
            if (File.Exists(caminhoText))
            {
                using (StreamReader sr = new StreamReader(caminhoText))
                {
                    string linha;
                    while ((linha = sr.ReadLine()) != null)
                    {
                        if (linha.StartsWith("CLIENTE:"))
                        {
                            Cliente cliente = LerBancoCliente(linha);
                            if (cliente != null) clientes.Add(cliente);
                        }

                    }
                }
            }
        }

        public Cliente LerBancoCliente(string linha)
        {
            string prefixo = "CLIENTE:"; //Prefixo que identifica o início de uma linha de conta no arquivo.
            string linhaArquivo = linha.Substring(prefixo.Length);
            linhaArquivo.Substring(0, 5);
            string[] blocos = linhaArquivo.Split(','); //Dividir a linha em blocos usando a vírgula como separador.
            string nomeCliente = blocos[0].Split(':')[1].Trim(); 
            string emailCliente = blocos[1].Split(':')[1].Trim(); 
            string bloconasc = blocos[2].Replace(";", "").Trim(); //Remover o ponto e vírgula do final do terceiro bloco.
            string datastring = bloconasc.Split(':')[1].Trim(); 
            DateOnly dataNascimento = DateOnly.Parse(datastring); //Converter a string de data para o tipo DateOnly.

            var cliente = new Cliente(nomeCliente, emailCliente, dataNascimento);
            
            return cliente;
        }

        public void CriarCliente(string caminhoText, Cliente cliente)
        {
            using (StreamWriter sw = File.AppendText(caminhoText))
            {
                sw.WriteLine($"CLIENTE: Nome : {cliente.Nome}, Email: {cliente.Email}, Nascimento: {cliente.DataNascimento};");
            }
        }
    }
}
