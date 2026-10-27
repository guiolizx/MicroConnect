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
