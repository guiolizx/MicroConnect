using System;
using System.ComponentModel.DataAnnotations;

namespace pi.Models;

public class LoginClienteViewModel
{
    
    [Required(ErrorMessage = "O CPF e obrigatorio")]
    public string Cpf {get;set;}

    [Required(ErrorMessage = "A Senha e obrigatoria")]
    [DataType(DataType.Password)]
    public string Senha {get;set;}
}
