using System;

namespace pi.Models;

public class ProdutorHomeViewModel
{
    public ProdutorModel Produtor {get;set;} = new();
    public List<ProdutoProdutorModel> Produtos {get;set;} = new();
}
