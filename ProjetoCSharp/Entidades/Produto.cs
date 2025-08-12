namespace ProjetoCSharp.Entidades
{
    public class Produto
    {
        public string Nome { get; set; }
        public double Preco { get; set; }

        public Produto(string nome, double preco)
        {
            Nome = nome;
            Preco = preco;
        }
        public Produto()
        {
        }
        public void CriarProduto(string caminhoText, Produto produto)
        {
            using (StreamWriter sw = File.AppendText(caminhoText))
            {
                sw.WriteLine($"PRODUTO: Nome : {produto.Nome}, Email: {produto.Preco};");
            }
        }

        public Produto LerBancoProdutos(string linha)
        {
            string prefixo = "PRODUTO:"; //Prefixo que identifica o início de uma linha de conta no arquivo.
            string linhaArquivo = linha.Substring(prefixo.Length);
            linhaArquivo.Substring(0, 5);
            string[] blocos = linhaArquivo.Split(','); //Dividir a linha em blocos usando a vírgula como separador.
            string nomeProduto = blocos[0].Split(':')[1].Trim();
            string precoProduto = blocos[1].Split(':')[1].Replace(";", "").Trim();
            double preco = double.Parse(precoProduto);
            var produto = new Produto ( nomeProduto, preco );

            return produto;
        }

        public void CarregarProdutos(List<Produto> produtos, string caminhoText)
        {
            if (File.Exists(caminhoText))
            {
                using (StreamReader sr = new StreamReader(caminhoText))
                {
                    string linha;
                    while ((linha = sr.ReadLine()) != null)
                    {
                        if (linha.StartsWith("PRODUTO:"))
                        {
                            Produto produto = LerBancoProdutos(linha);
                            if (produto != null) produtos.Add(produto);
                        }

                    }
                }
            }
        }
    }
}
