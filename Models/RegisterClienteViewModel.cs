using System;
using System.ComponentModel.DataAnnotations;

namespace pi.Models;

public class RegisterClienteViewModel
{
    [Required(ErrorMessage = "O Nome e obrigatorio")]
    public string Nome {get;set;}

    [Required(ErrorMessage = "O CPF e obrigatorio")]
    public string Cpf {get;set;}


    [Required(ErrorMessage = "O Email e obrigatorio")]
    [EmailAddress(ErrorMessage = "Email Invalido")]
    public string Email {get;set;}

    [Required(ErrorMessage = "A Senha e obrigatoria")]
    [DataType(DataType.Password)]
    public string Senha {get;set;}
}
