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
