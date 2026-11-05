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

    public IActionResult EditarCliente()
    {

        int id = HttpContext.Session.GetInt32("idCliente") ?? 0;
        var user = JsonConvert.DeserializeObject<ClienteModel>(
            HttpContext.Session.GetString("UserCliente")
        );

        Console.WriteLine(JsonConvert.SerializeObject(user));
        

        if (user == null){
            
            return RedirectToAction("LoginProdutor", "Auth");
        }

        var cliente = _userContext.SearchById(id);

        if (cliente == null) return RedirectToAction("Dashboard","Dashboard");

        return View(new RegisterClienteViewModel
        {
            Nome = cliente.Nome,
            Cpf = cliente.Cpf,
            Email = cliente.Email,
            Senha = cliente.Senha
        });
    }
    [HttpPost]
    public IActionResult EditarCliente(RegisterClienteViewModel cliente)
    {
        if (!ModelState.IsValid)
            return View(cliente);

        var user = JsonConvert.DeserializeObject<ClienteModel>(
            HttpContext.Session.GetString("UserCliente")
        );

        Console.WriteLine(HttpContext.Session.GetInt32("idCliente"));

        if(user == null || string.IsNullOrEmpty(user.Cpf) || HttpContext.Session.GetInt32("idCliente") == null)
            return RedirectToAction("LoginCliente","Auth");
        
        var clienteModel = new ClienteModel
        {
            IdCliente = HttpContext.Session.GetInt32("idCliente") ?? 0,
            Nome = cliente.Nome,
            Cpf = cliente.Cpf,
            Email = cliente.Email,
            Senha = cliente.Senha
        };
        

        _userContext.Edit(clienteModel);
        var atualizado = _userContext.SearchById(HttpContext.Session.GetInt32("idCliente") ?? 0);
        HttpContext.Session.SetString("UserCliente", JsonConvert.SerializeObject(atualizado));

        return RedirectToAction("Reserve","Dashboard");
    }

    public IActionResult EditarProdutor(int id)
    {
        var user = JsonConvert.DeserializeObject<ProdutorModel>(
            HttpContext.Session.GetString("UserProdutor")
        );

        if (user == null || string.IsNullOrEmpty(user.Cnpj))
            return RedirectToAction("LoginProdutor", "Auth");

        var produtor = _produtorContext.SearchById(id);

        if (produtor == null) return RedirectToAction("Dashboard","Dashboard");

        HttpContext.Session.SetInt32("idProdutor", produtor.idProdutor);

        return View(new RegisterProdutorViewModel
        {
            Nome = produtor.Nome,
            Cnpj = produtor.Cnpj,
            Email = produtor.Email,
            Telefone = produtor.Telefone,
            Endereco = produtor.Endereco,
            Senha = produtor.Senha
        });
    }

    [HttpPost]
    public IActionResult EditarProdutor(RegisterProdutorViewModel produtor)
    {
        if (!ModelState.IsValid)
            return View(produtor);

        var user = JsonConvert.DeserializeObject<ProdutorModel>(
            HttpContext.Session.GetString("UserProdutor")
        );
        
        if(user == null || string.IsNullOrEmpty(user.Cnpj) || HttpContext.Session.GetInt32("idProdutor") == null)
            return RedirectToAction("LoginProdutor","Auth");
        
        var produtorr = new ProdutorModel
        {
            idProdutor = HttpContext.Session.GetInt32("idProdutor") ?? 0,
            Nome = produtor.Nome,
            Cnpj = produtor.Cnpj,
            Email = produtor.Email,
            Telefone = produtor.Telefone,
            Endereco = produtor.Endereco
        };

        _produtorContext.Edit(produtorr);
        var atualizado = _produtorContext.SearchById(HttpContext.Session.GetInt32("idProdutor") ?? 0);
        HttpContext.Session.SetString("UserProdutor", JsonConvert.SerializeObject(atualizado));

        return RedirectToAction("Dashboard","Dashboard");
    }

    public IActionResult DeleteProdutor()
    {
        int id = HttpContext.Session.GetInt32("idProdutor") ?? 0;
        var user = JsonConvert.DeserializeObject<ProdutorModel>(
            HttpContext.Session.GetString("UserProdutor")
        );

        if (user == null || string.IsNullOrEmpty(user.Cnpj))
            return RedirectToAction("LoginProdutor", "Auth");

        _produtorContext.Delete(id);
        HttpContext.Session.Remove("UserProdutor");
        HttpContext.Session.Remove("idProdutor");
        return RedirectToAction("Index","Home");
    }

    public IActionResult DeleteCliente()
    {
        int id = HttpContext.Session.GetInt32("idCliente") ?? 0;
        var user = JsonConvert.DeserializeObject<ClienteModel>(
            HttpContext.Session.GetString("UserCliente")
        );

        if (user == null || string.IsNullOrEmpty(user.Cpf))
            return RedirectToAction("LoginCliente", "Auth");

        _userContext.Delete(id);
        HttpContext.Session.Remove("UserCliente");
        HttpContext.Session.Remove("idCliente");
        return RedirectToAction("Index","Home");
    }

    public IActionResult LoginArea()
    {
        var uc = HttpContext.Session.GetString("UserCliente");
        var up = HttpContext.Session.GetString("UserProdutor");
        Console.WriteLine(up);
        if(!string.IsNullOrEmpty(uc)) return RedirectToAction("Reserve","Dashboard");
        else if(!string.IsNullOrEmpty(up)) return RedirectToAction("Dashboard","Dashboard");
        return View();
    }

    public IActionResult LoginCliente()
    {
        var uc = HttpContext.Session.GetString("UserCliente");
        if(!string.IsNullOrEmpty(uc)) return RedirectToAction("Reserve","Dashboard");
        return View();
    }

    public IActionResult LoginProdutor()
    {
        
        var up = HttpContext.Session.GetString("UserProdutor");
        
        if(!string.IsNullOrEmpty(up)) return RedirectToAction("Dashboard","Dashboard");
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
                    HttpContext.Session.SetString("UserCliente", JsonConvert.SerializeObject(cliente));
                    HttpContext.Session.SetInt32("idCliente", cliente.IdCliente);
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
        if (ModelState.IsValid)
        {

            ProdutorModel produtor = _produtorContext.SearchByCNPJ(model.Cnpj);
            if (produtor != null)
            {
                if (produtor.SenhaValida(model.Senha))
                {
                    HttpContext.Session.SetString("UserProdutor", JsonConvert.SerializeObject(produtor));
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
                Senha = model.Senha
            };

            _produtorContext.Add(produtor);

            return RedirectToAction("LoginArea");
        }
        return View(model);
    }


    public IActionResult Sair(){
        HttpContext.Session.Remove("UserProdutor");
        HttpContext.Session.Remove("UserCliente");
        return RedirectToAction("Index","Home");
    }



}