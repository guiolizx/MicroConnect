using System;
using System.ComponentModel.DataAnnotations;

namespace pi.Models;

public class LoginProdutorViewModel
{
    
    [Required(ErrorMessage = "O CNPJ e obrigatorio")]
    public string Cnpj {get;set;}

    [Required(ErrorMessage = "A Senha e obrigatoria")]
    [DataType(DataType.Password)]
    public string Senha {get;set;}
}
