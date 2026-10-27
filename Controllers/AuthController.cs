using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using pi.Data;
using pi.Models;
using pi.Repository;
using teste.Helper;

namespace pi.Controllers;

public class AuthController : Controller
{

    private readonly IClienteRepository _userContext;
    private readonly IProdutorRepository _produtorContext;

    public AuthController(IClienteRepository userContext, IProdutorRepository produtorContext)
    {
        _userContext = userContext;
        _produtorContext = produtorContext;
    }
    public IActionResult RegisterCliente()
    {
        return View();
    }

    public IActionResult RegisterProdutor()
    {
        return View();
    }

    public IActionResult LoginArea()
    {
        return View();
    }

    public IActionResult LoginCliente()
    {
        return View();
    }

    public IActionResult LoginProdutor()
    {
        return View();
    }

    [HttpPost]
    public IActionResult LoginCliente(LoginClienteViewModel model)
    {
        Console.WriteLine(JsonConvert.SerializeObject(model));

        if (ModelState.IsValid)
        {
            ClienteModel cliente = _userContext.SearchByCPF(model.Cpf);
            Console.WriteLine(JsonConvert.SerializeObject(cliente));
            if (cliente != null)
            {
                if (cliente.SenhaValida(model.Senha))
                {
                    return RedirectToAction("Reserve", "Dashboard");
                }
                TempData["ErrorMessage"] = "Senha Incorreta!";
                return View(model);
            }
            TempData["ErrorMessage"] = "CPF Incorreto!";
        }

        return View(model);
    }

    [HttpPost]
    public IActionResult LoginProdutor(LoginProdutorViewModel model)
    {
        Console.WriteLine(JsonConvert.SerializeObject(model));
        if (ModelState.IsValid)
        {
            
            ProdutorModel produtor = _produtorContext.SearchByCNPJ(model.Cnpj);
            if (produtor != null)
            {
                if (produtor.SenhaValida(model.Senha))
                {
                    return RedirectToAction("Dashboard", "Dashboard");
                }
                TempData["ErrorMessage"] = "Senha Incorreta!";
                return View(model);
            }
            TempData["ErrorMessage"] = "CNPJ Incorreto!";
        }

        return View(model);
    }



    [HttpPost]
    public IActionResult RegisterCliente(RegisterClienteViewModel model)
    {
        if (ModelState.IsValid)
        {

            var cliente = new ClienteModel
            {
                Nome = model.Nome,
                Cpf = model.Cpf,
                Email = model.Email,
                Senha = model.Senha
            };

            _userContext.Add(cliente);
            return RedirectToAction("LoginArea");
        }
        return View(model);
    }


    [HttpPost]

    public IActionResult RegisterProdutor(RegisterProdutorViewModel model)
    {
        if (ModelState.IsValid)
        {

            var produtor = new ProdutorModel
            {
                Nome = model.Nome,
                Cnpj = model.Cnpj,
                Telefone = model.Telefone,
                Endereco = model.Endereco,
                Email = model.Email,
                Senha = model.Senha.genHash()
            };

            _produtorContext.Add(produtor);

            return RedirectToAction("LoginArea");
        }
        return View(model);
    }





}