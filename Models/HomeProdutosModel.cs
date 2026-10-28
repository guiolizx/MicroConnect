
namespace pi.Models;

class HomeProdutosModel
{
    public List<ProdutoModel> produtos;

    public void Limpar()
    {
        var old = produtos;
        produtos = new List<ProdutoModel>();
        foreach(var oitem in old)
        {
            if(!produtos.Any(item => item.Nome == oitem.Nome))
                produtos.Add(oitem);
        }
    }
}