using System;
using pi.Models;

namespace pi.Repository;

public interface IProdutoRepository
{
    void Add(ProdutoModel produto,int idProdutor,int quantidade);
    List<ProdutoProdutorModel> BuscarPorProdutor(int idProdutor);
    ProdutoModel BuscarPorId(int idProduto);
    void Edit(ProdutoModel produto,
    ProdutoProdutorModel pprodutor);

    ProdutoProdutorModel BuscarRelacionamento(int idProduto, int idProdutor);
}
