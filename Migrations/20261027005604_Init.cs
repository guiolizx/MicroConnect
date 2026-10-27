using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace pi.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    id_cliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    nr_cpf = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: false),
                    nm_cliente = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    ds_email = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    pw_senha_cliente = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.id_cliente);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Produtores",
                columns: table => new
                {
                    id_produtor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    nr_cnpj = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: false),
                    nm_produtor = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    nr_tel_contato = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: false),
                    ds_email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    pw_senha_produtor = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtores", x => x.id_produtor);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    id_produto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    nm_produto = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    ds_safra = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.id_produto);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Produtos_produtor",
                columns: table => new
                {
                    id_produto_produtor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    nr_qntd_produtos = table.Column<int>(type: "int", nullable: false),
                    id_produtor = table.Column<int>(type: "int", nullable: false),
                    id_produto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos_produtor", x => x.id_produto_produtor);
                    table.ForeignKey(
                        name: "FK_Produtos_produtor_Produtores_id_produtor",
                        column: x => x.id_produtor,
                        principalTable: "Produtores",
                        principalColumn: "id_produtor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Produtos_produtor_Produtos_id_produto",
                        column: x => x.id_produto,
                        principalTable: "Produtos",
                        principalColumn: "id_produto",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_produtor_id_produto",
                table: "Produtos_produtor",
                column: "id_produto");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_produtor_id_produtor",
                table: "Produtos_produtor",
                column: "id_produtor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Produtos_produtor");

            migrationBuilder.DropTable(
                name: "Produtores");

            migrationBuilder.DropTable(
                name: "Produtos");
        }
    }
}
