using System;
using System.ComponentModel.DataAnnotations;

namespace pi.Models;

public class RegisterProdutorViewModel
{
    [Required(ErrorMessage = "O Nome e obrigatorio")]
    public string Nome {get;set;}

    [Required(ErrorMessage = "O CNPJ e obrigatorio")]
    public string Cnpj {get;set;}

    [Required(ErrorMessage = "O Telefone e obrigatorio")]
    public string Telefone {get;set;}
    [Required(ErrorMessage = "O Endereco e obrigatorio")]
    public string Endereco {get;set;}

    [Required(ErrorMessage = "O Email e obrigatorio")]
    [EmailAddress(ErrorMessage = "Email Invalido")]
    public string Email {get;set;}

    [Required(ErrorMessage = "A Senha e obrigatoria")]
    [DataType(DataType.Password)]
    public string Senha {get;set;}
}
