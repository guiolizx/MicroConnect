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

            var nome = produto.Nome.Trim().ToLower();

            var produtoExistente = _bancoContext.Produto.FirstOrDefault(p => p.Nome.ToLower() == nome);

            if (produtoExistente == null)
            {
                produto.Nome = produto.Nome.Trim();

                _bancoContext.Produto.Add(produto);
                _bancoContext.SaveChanges();

                produtoExistente = produto;
            }


            var relacaoExistente = _bancoContext.ProdutoProdutor
                .FirstOrDefault(pp =>
                    pp.IdProduto == produtoExistente.IdProduto &&
                    pp.IdProdutor == idProdutor
                );

            if (relacaoExistente == null)
            {
                var produtoProdutor = new ProdutoProdutorModel
                {
                    IdProduto = produtoExistente.IdProduto,
                    IdProdutor = idProdutor,
                    Quantidade = quantidade
                };
                _bancoContext.ProdutoProdutor.Add(produtoProdutor);
            }
            else
            {
                relacaoExistente.Quantidade = quantidade;
            }
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

    public List<ProdutoModel> BuscarTodos()
    {
        return _bancoContext.Produto.ToList();
    }

    public List<ProdutoProdutorModel> BuscarTodosComProdutores()
    {
        return _bancoContext.ProdutoProdutor
                    .Include(pp => pp.Produto)
                    .Include(pp => pp.Produtor)
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

    public void Delete(int idProduto, int idProdutor)
    {
        ProdutoProdutorModel ppDb = BuscarRelacionamento(idProduto,idProdutor);
        if(ppDb == null) return;
        _bancoContext.ProdutoProdutor.Remove(ppDb);
        _bancoContext.SaveChanges();
    }

}