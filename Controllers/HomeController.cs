using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using pi.Models;
using pi.Repository;

namespace pi.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly IProdutoRepository _produtoRepository;

    public HomeController(ILogger<HomeController> logger, IProdutoRepository produtoRepository)
    {
        _logger = logger;
        _produtoRepository = produtoRepository;
    }

    public IActionResult Index()
    {

        var relacoes = _produtoRepository.BuscarTodosComProdutores();
        var produtos = relacoes
            .Where(r=>r.Produto != null)
            .GroupBy(r => r.IdProduto)
            .Select(g => new ProdutoHomeViewModel
            {
                Produto = g.First().Produto!,
                Produtores = g.ToList()
            }).ToList();
        return View(produtos);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
