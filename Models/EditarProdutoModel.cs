using pi.Enums;

namespace pi.Models;

public class EditarProdutoModel
{
    public int IdProduto {get;set;}
    public string Nome {get;set;} = "";
    public Safras Safra {get;set;}
    public int Quantidade {get;set;}
}