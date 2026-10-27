using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using teste.Helper;

namespace pi.Models;

[Table("Clientes")]
public class ClienteModel
{
    [Key]
    [Column("id_cliente")]
    public int IdCliente {get;set;}

    [Required]
    [Column("nr_cpf")]
    [StringLength(11)]
    public string Cpf {get;set;}

    [Required]
    [Column("nm_cliente")]
    [StringLength(100)]
    public string Nome {get;set;}
    [Required]
    [Column("ds_email")]
    [StringLength(150)]
    public string Email {get;set;}

    [Required]
    [Column("pw_senha_cliente")]
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
