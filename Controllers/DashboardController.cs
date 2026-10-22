using Microsoft.AspNetCore.Mvc;

namespace pi.Controllers;

public class DashboardController : Controller
{
    public IActionResult Dashboard()
    {
        return View();
    }

    public IActionResult Reserve()
    {
        return View();
    }
}