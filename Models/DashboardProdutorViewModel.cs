namespace pi.Models;

public class DashboardProdutorViewModel
{
    public ProdutorModel? Produtor { get; set; }
    public ProdutoModel? Produto { get; set; }
    public ProdutoProdutorModel ProdutoProdutor { get; set; } = new();


    public List<ProdutoProdutorModel> Produtos { get; set; } = new();
}