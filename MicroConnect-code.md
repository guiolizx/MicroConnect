

## Controllers/HomeController.cs

```cs
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
        var lista = new HomeProdutosModel
        {
            produtos = _produtoRepository.BuscarTodos()
        };
        lista.Limpar();
        return View(lista);
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

```

## Data/BancoContext.cs

```cs
using Microsoft.EntityFrameworkCore;
using pi.Models;

namespace pi.Data;

public class BancoContext : DbContext
{
    public BancoContext(DbContextOptions<BancoContext> options) : base(options) { }

    public DbSet<ClienteModel> Cliente { get; set; }
    public DbSet<ProdutorModel> Produtor { get; set; }
    public DbSet<ProdutoModel> Produto { get; set; }
    public DbSet<ProdutoProdutorModel> ProdutoProdutor { get; set; }
}
```

## Enums/Safras.cs

```cs
namespace pi.Enums;

public enum Safras
{
    Verao = 1,
    Outono = 2,
    Inverno = 3,
    Primavera = 4,
}

```

## Models/HomeProdutosModel.cs

```cs

namespace pi.Models;

class HomeProdutosModel
{
    public List<ProdutoModel> produtos;

    public void Limpar()
    {
        var old = produtos;
        produtos = new List<ProdutoModel>();
        foreach(var oitem in old)
        {
            if(!produtos.Any(item => item.Nome == oitem.Nome))
                produtos.Add(oitem);
        }
    }
}
```

## Models/ProdutoModel.cs

```cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using pi.Enums;

namespace pi.Models;

[Table("Produtos")]
public class ProdutoModel
{
    [Key]
    [Column("id_produto")]
    public int IdProduto {get;set;}
    [Required]
    [Column("nm_produto")]
    [StringLength(40)]
    public string Nome {get;set;}

    [Required]
    [Column("ds_safra")]
    public Safras Safra{get;set;}
}

```

## Models/ProdutoProdutorModel.cs

```cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pi.Models;

[Table("Produtos_produtor")]
public class ProdutoProdutorModel
{
    [Key]
    [Column("id_produto_produtor")]
    public int Id {get;set;}

    [Required]
    [Column("nr_qntd_produtos")]
    public int Quantidade {get;set;}

    [Column("id_produtor")]
    public int IdProdutor {get;set;}

    [ForeignKey("IdProdutor")]
    public virtual ProdutorModel? Produtor {get;set;}

    [Column("id_produto")]
    public int IdProduto {get;set;}

    [ForeignKey("IdProduto")]
    public virtual ProdutoModel? Produto {get;set;}
}

```

## Models/ProdutorModel.cs

```cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using teste.Helper;

namespace pi.Models;

[Table("Produtores")]
public class ProdutorModel
{
    [Key]
    [Column("id_produtor")]
    public int idProdutor {get;set;}

    [Required]
    [Column("nr_cnpj")]
    [StringLength(14)]
    public string Cnpj {get;set;}

    [Required]
    [Column("nm_produtor")]
    [StringLength(150)]
    public string Nome {set;get;}

    [Required]
    [Column("nr_tel_contato")]
    [StringLength(11)]
    public string Telefone {get;set;}

    [Required]
    [Column("ds_email")]
    [StringLength(100)]
    [EmailAddress]
    public string Email {get;set;}

    [Required]
    [StringLength(150)]
    [Column("ds_endereco")]
    public string Endereco {get;set;}

    [Required]
    [Column("pw_senha_produtor")]
    public string Senha {get;set;}

    public void setHash()
    {
        Senha = Senha.genHash();
    }

    public bool SenhaValida(string password)
    {
        return Senha == password.genHash();
    }
}

```

## Repository/IProdutoRepository.cs

```cs
using System;
using pi.Models;

namespace pi.Repository;

public interface IProdutoRepository
{
    void Add(ProdutoModel produto,int idProdutor,int quantidade);
    List<ProdutoProdutorModel> BuscarPorProdutor(int idProdutor);
    ProdutoModel BuscarPorId(int idProduto);
    void Edit(ProdutoModel produto,
    ProdutoProdutorModel pprodutor);
    List<ProdutoModel> BuscarTodos();

    ProdutoProdutorModel BuscarRelacionamento(int idProduto, int idProdutor);
}

```

## Repository/IProdutorRepository.cs

```cs

using pi.Models;

namespace pi.Repository;

public interface IProdutorRepository
{
    ProdutorModel Add(ProdutorModel cliente);
    ProdutorModel SearchByCNPJ(string cnpj);
}
```

## Views/Home/Index.cshtml

```cshtml
﻿@model HomeProdutosModel;

@{
    ViewData["Title"] = "MicroConnect - Seja bem-vindo(a)";
}

<header class="slideshow">
    <div>
        <img class="headerImage active" src="./assets/images/foto_destaque3.jpeg" alt="Imagem Destaque 3">
        <img class="headerImage" src="./assets/images/foto_destaque1.jpg" alt="Imagem Destaque 1">
        <img class="headerImage" src="./assets/images/foto_destaque2.jpeg" alt="Imagem Destaque 2">
    </div>

    
</header>

<div class="banner">
    <div class="buttonsHighlights">

        <button class="ballButton selected"></button>
        <button class="ballButton"></button>
        <button class="ballButton"></button>

    </div>
    <div class="btns">
        <a asp-controller="Auth" asp-action="RegisterCliente" class="RCButton">Seja Cliente</a>
        <a asp-controller="Auth" asp-action="RegisterProdutor" class="RPButton">Torne-se Produtor</a>
    </div>
</div>

<h2 class="title">Destaques da Estação</h2>

@if(Model.produtos == null){
<p class="noRegister">Não há nenhum produto cadastrado no momento.</p>
}

<div>
    @foreach(var item in Model.produtos)
    {
        <h1>@item.Nome</h1>
        <h1>@item.Safra</h1>
    }
</div>

<a class="seeMore">VER MAIS</a>

@section Scripts {
    <script src="~/js/slideshow.js"></script>
}
```