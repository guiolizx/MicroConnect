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