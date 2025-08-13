namespace ProjetoCSharp.Entidades
{
    public class ProdutoImportado : Produto
    {
        public Double TaxaAlfandega { get; set; }

        public ProdutoImportado()
        {
        }
        public ProdutoImportado(string nome, double preco, double taxaAlfandega) : base(nome, preco)
        {
            TaxaAlfandega = taxaAlfandega;
        }
        public override string Etiqueta()
        {
            return base.Etiqueta() + ($", Taxa Alfândega: {TaxaAlfandega}, Preço Total: {PrecoTotal()}");
        }
        public double PrecoTotal()
        {
            return Preco + TaxaAlfandega;
        }

        public override void CriarProduto(string caminhoText, Produto produto)
        {
            using (StreamWriter sw = File.AppendText(caminhoText))
            {
                sw.WriteLine($"PRODUTO IMPORTADO: Nome : {produto.Nome}, Preco: {produto.Preco}, Taxa Alfândega: {TaxaAlfandega}, Preco Total: {PrecoTotal()};");
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
            string taxaString = blocos[2].Split(':')[1].Trim();
            string valorTotal = blocos[3].Split(':')[1].Replace(";", "").Trim();
            double taxa = double.Parse(taxaString);
            var produto = new ProdutoImportado(nomeProduto, preco, taxa);

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
                        if (linha.StartsWith("PRODUTO IMPORTADO:"))
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
