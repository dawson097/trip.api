using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trip.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialOrderTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "CartLineItems",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    OrderState = table.Column<int>(type: "integer", nullable: false),
                    CreateTimeUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TransactionMetadata = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartLineItems_OrderId",
                table: "CartLineItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartLineItems_Orders_OrderId",
                table: "CartLineItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartLineItems_Orders_OrderId",
                table: "CartLineItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_CartLineItems_OrderId",
                table: "CartLineItems");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "CartLineItems");
        }
    }
}
