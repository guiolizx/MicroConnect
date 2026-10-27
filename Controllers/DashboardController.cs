using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

    [HttpPost]
    public IActionResult EditarProduto(EditarProdutoModel produto)
    {
        if (!ModelState.IsValid)
            return View(produto);

        var user = JsonConvert.DeserializeObject<ProdutorModel>(
            HttpContext.Session.GetString("UserProdutor")
        );
        
        if(user == null || string.IsNullOrEmpty(user.Cnpj))
            return RedirectToAction("LoginProdutor","Auth");
        
        var produtoo = new ProdutoModel
        {
            IdProduto = produto.IdProduto,
            Nome = produto.Nome,
            Safra = produto.Safra
        };

        var rel = new ProdutoProdutorModel
        {
            IdProduto = produto.IdProduto,
            IdProdutor = user.idProdutor,
            Quantidade = produto.Quantidade
        };

        _produtoRepository.Edit(produtoo,rel);

        return RedirectToAction("Dashboard");
    }
    public IActionResult EditarProduto(int id)
    {
        Console.WriteLine(id);
        var user = JsonConvert.DeserializeObject<ProdutorModel>(
            HttpContext.Session.GetString("UserProdutor")
        );

        if (user == null || string.IsNullOrEmpty(user.Cnpj))
            return RedirectToAction("LoginProdutor", "Auth");

        var produto = _produtoRepository.BuscarPorId(id);

        if (produto == null) return RedirectToAction("Dashboard");

        var rel = _produtoRepository.BuscarRelacionamento(produto.IdProduto,user.idProdutor);

        if(rel == null) return RedirectToAction("Dashboard");


        return View(new EditarProdutoModel
        {
            IdProduto = produto.IdProduto,
            Nome = produto.Nome,
            Safra = produto.Safra,
            Quantidade = rel.Quantidade
        });
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
        }
        catch (Exception)
        {
            return RedirectToAction("LoginProdutor", "Auth");
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
        if (string.IsNullOrEmpty(sessao)) return RedirectToAction("LoginProdutor", "Auth");

        var produtor = JsonConvert.DeserializeObject<ProdutorModel>(sessao);

        if (produtor == null) return RedirectToAction("LoginProdutor", "Auth");

        _produtoRepository.Add(
            model.Produto,
            produtor.idProdutor,
            model.ProdutoProdutor.Quantidade
        );

        return RedirectToAction("Dashboard");
    }
}