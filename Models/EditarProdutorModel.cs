using System.ComponentModel.DataAnnotations;

namespace pi.Models;

public class EditarProdutorModel
{
    [Required(ErrorMessage = "Nome é obrigatorio")]
    public string Nome {get;set;}
    
    [Required(ErrorMessage = "CNPJ Obrigatorio")]
    [StringLength(14,MinimumLength =14,ErrorMessage = "CNPJ Invalido")]
    public string Cnpj {get;set;}

    [Required(ErrorMessage = "Email Obrigatorio")]
    [EmailAddress(ErrorMessage = "Email invalido")]
    public string Email {get;set;}
}