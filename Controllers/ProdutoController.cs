using Microsoft.AspNetCore.Mvc;

namespace pi.Controllers;

public class ProdutoController : Controller
{
    public IActionResult Produtos()
    {
        return View();
    }

    public IActionResult Produtores()
    {
        return View();
    }
}