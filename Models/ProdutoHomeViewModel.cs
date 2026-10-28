namespace pi.Models;

public class ProdutoHomeViewModel
{
    public ProdutoModel Produto {get;set;} = new();

    public List<ProdutoProdutorModel> Produtores{get;set;} = new();
}