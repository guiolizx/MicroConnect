using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pi.Migrations
{
    /// <inheritdoc />
    public partial class produtorAddEndereco2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ds_endereco",
                table: "Produtores",
                type: "varchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ds_endereco",
                table: "Produtores");
        }
    }
}
