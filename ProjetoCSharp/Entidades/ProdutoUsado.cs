namespace ProjetoCSharp.Entidades
{
    public class ProdutoUsado : Produto
    {
        public DateOnly DataFabricacao { get; set; }

        public ProdutoUsado()
        {
        }
        public ProdutoUsado(string nome, double preco, DateOnly dataFabricacao) : base(nome, preco)
        {
            DataFabricacao = dataFabricacao;
        }

        public override string Etiqueta()
        {
            return base.Etiqueta() + ($", Data de Fabricação: {DataFabricacao}");
        }

        public override void CriarProduto(string caminhoText, Produto produto)
        {
            using (StreamWriter sw = File.AppendText(caminhoText))
            {
                sw.WriteLine($"PRODUTO USADO: Nome : {produto.Nome}, Preco: {produto.Preco}, Data de Fabricação: {DataFabricacao};");
            }
        }

        public override Produto LerBancoProdutos(string linha)
        {
            string prefixo = "PRODUTO USADO:"; //Prefixo que identifica o início de uma linha de conta no arquivo.
            string linhaArquivo = linha.Substring(prefixo.Length);
            linhaArquivo.Substring(0, 5);
            string[] blocos = linhaArquivo.Split(','); //Dividir a linha em blocos usando a vírgula como separador.
            string nomeProduto = blocos[0].Split(':')[1].Trim();
            string precoProduto = blocos[1].Split(':')[1].Trim();
            double preco = double.Parse(precoProduto);
            string dataFabricacao = blocos[2].Split(':')[1].Replace(";", "").Trim();
            DateOnly data = DateOnly.Parse(dataFabricacao);
            var produto = new ProdutoUsado(nomeProduto, preco, data);

            return produto;
        }

        public override void CarregarProdutos(List<Produto> produtos, string caminhoText)
        {
            if (File.Exists(caminhoText))
            {
                using (StreamReader sr = new StreamReader(caminhoText))
                {
                    string linha;
                    while ((linha = sr.ReadLine()) != null)
                    {
                        if (linha.StartsWith("PRODUTO USADO:"))
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
