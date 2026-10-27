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
