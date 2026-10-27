using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using pi.Models;
using pi.Repository;

namespace pi.Controllers;

public class DashboardController : Controller
{

    private readonly IProdutoRepository _produtoRepository;

    public DashboardController(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }
    public IActionResult Dashboard()
    {
        try
        {
            var user = JsonConvert.DeserializeObject<ProdutorModel>(HttpContext.Session.GetString("UserProdutor"));

            if (user == null || string.IsNullOrEmpty(user.Cnpj)) return RedirectToAction("LoginProdutor", "Auth");

            var viewModel = new DashboardProdutorViewModel
            {
                Produtor = user,
                Produto = new ProdutoModel(),
                Produtos = _produtoRepository.BuscarPorProdutor(user.idProdutor),
                ProdutoProdutor = new ProdutoProdutorModel()
            };
            return View(viewModel);
        } catch (Exception)
        {
            return RedirectToAction("LoginProdutor","Auth");
        }
    }

    public IActionResult Reserve()
    {

        try
        {
            var user = JsonConvert.DeserializeObject<ClienteModel>(HttpContext.Session.GetString("UserCliente"));
            if (string.IsNullOrEmpty(user.Cpf)) return RedirectToAction("LoginCliente", "Auth");
            return View(user);
        }
        catch (Exception)
        {
            return RedirectToAction("LoginCliente", "Auth");
        }

    }

    [HttpPost]
    public IActionResult CadastrarProduto(DashboardProdutorViewModel model)
    {
        var sessao = HttpContext.Session.GetString("UserProdutor");
        if(string.IsNullOrEmpty(sessao)) return RedirectToAction("LoginProdutor","Auth");

        var produtor = JsonConvert.DeserializeObject<ProdutorModel>(sessao);

        if(produtor == null) return RedirectToAction("LoginProdutor","Auth");

        _produtoRepository.Add(
            model.Produto,
            produtor.idProdutor,
            model.ProdutoProdutor.Quantidade
        );

        return RedirectToAction("Dashboard");
    }
}