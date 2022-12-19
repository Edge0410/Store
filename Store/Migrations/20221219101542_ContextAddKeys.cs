using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Migrations
{
    /// <inheritdoc />
    public partial class ContextAddKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderLists",
                table: "OrderLists");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderLists",
                table: "OrderLists",
                columns: new[] { "OrderId", "ProductId", "Id" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderLists",
                table: "OrderLists");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderLists",
                table: "OrderLists",
                columns: new[] { "OrderId", "ProductId" });
        }
    }
}
