using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Webshopt.Bookstore.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class discount_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DiscountApplied",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountApplied",
                table: "Orders");
        }
    }
}
