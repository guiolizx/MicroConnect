using Microsoft.EntityFrameworkCore;
using pi.Data;
using pi.Models;

namespace pi.Repository;

public class ProdutoRepository : IProdutoRepository
{
    private readonly BancoContext _bancoContext;

    public ProdutoRepository(BancoContext bancoContext)
    {
        _bancoContext = bancoContext;
    }

    public void Add(ProdutoModel produto, int idProdutor, int quantidade)
    {
        using var transacao = _bancoContext.Database.BeginTransaction();
        try
        {
            _bancoContext.Produto.Add(produto);
            _bancoContext.SaveChanges();

            var produtoProdutor = new ProdutoProdutorModel
            {
                IdProduto = produto.IdProduto,
                IdProdutor = idProdutor,
                Quantidade = quantidade
            };

            _bancoContext.ProdutoProdutor.Add(produtoProdutor);
            _bancoContext.SaveChanges();

            transacao.Commit();
        }
        catch
        {
            transacao.Rollback();
            throw;
        }
    }

    public List<ProdutoProdutorModel> BuscarPorProdutor(int idProdutor)
    {
        return _bancoContext.ProdutoProdutor
            .Where(p => p.IdProdutor == idProdutor)
            .Include(p => p.Produto)
            .ToList();
    }
}