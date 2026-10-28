using Microsoft.AspNetCore.Mvc;
using pi.Models;
using pi.Repository;

namespace pi.Controllers;

public class ProdutoController : Controller
{

    private readonly IProdutoRepository _produtoRepository;
    public ProdutoController(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }
    public IActionResult Produtos()
    {
        var relacoes = _produtoRepository.BuscarTodosComProdutores();
        var produtos = relacoes
            .Where(r => r.Produto != null)
            .GroupBy(r => r.IdProduto)
            .Select(g => new ProdutoHomeViewModel
            {
                Produto = g.First().Produto!,
                Produtores = g.ToList()
            }).ToList();

        return View(produtos);
    }

    public IActionResult Produtores()
    {
        var relacoes = _produtoRepository.BuscarTodosComProdutores();
        var produtos = relacoes
            .Where(r => r.Produtor != null && r.Produto != null)
            .GroupBy(r => r.IdProdutor)
            .Select(g => new ProdutorHomeViewModel
            {
                Produtor = g.First().Produtor!,
                Produtos = g.ToList()
            }).ToList();

        return View(produtos);
    }

    public IActionResult ExcluirProduto(int idProduto, int idProdutor)
    {
        // Excluir a relação entre o produtor e o produto

        _produtoRepository.Delete(idProduto,idProdutor);

        return RedirectToAction("Dashboard", "Dashboard");
    }
}