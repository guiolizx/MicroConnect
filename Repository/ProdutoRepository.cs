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

    public ProdutoModel BuscarPorId(int idProduto)
    {
        return _bancoContext.Produto
            .FirstOrDefault(p => p.IdProduto == idProduto);
    }

    public void Edit(ProdutoModel produto,
    ProdutoProdutorModel pprodutor)
    {
        ProdutoModel produtoDb = BuscarPorId(produto.IdProduto);
        if (produtoDb == null) throw new Exception("Erro");

        produtoDb.Nome = produto.Nome;
        produtoDb.Safra = produto.Safra;

        var rel = _bancoContext.ProdutoProdutor
    .FirstOrDefault(pp =>
        pp.IdProduto == produto.IdProduto &&
        pp.IdProdutor == pprodutor.IdProdutor);

        if (rel == null) throw new Exception("Erro");

        rel.Quantidade = pprodutor.Quantidade;

        _bancoContext.SaveChanges();
    }
    public ProdutoProdutorModel BuscarRelacionamento(int idProduto, int idProdutor)
    {

        return _bancoContext.ProdutoProdutor
            .FirstOrDefault(pp =>
                pp.IdProduto == idProduto &&
                pp.IdProdutor == idProdutor
            );
    }

}