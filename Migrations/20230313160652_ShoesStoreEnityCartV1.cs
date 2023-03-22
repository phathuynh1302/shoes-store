using Microsoft.EntityFrameworkCore.Migrations;

namespace PRN211_ShoesStore.Migrations
{
    public partial class ShoesStoreEnityCartV1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItemDetails_CartItem_cartItemId",
                table: "CartItemDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItemDetails_ShoesSpecifically_specificallyShoesId",
                table: "CartItemDetails");

            migrationBuilder.AlterColumn<int>(
                name: "specificallyShoesId",
                table: "CartItemDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "cartItemId",
                table: "CartItemDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ShoesSize",
                table: "CartItemDetails",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItemDetails_CartItem_cartItemId",
                table: "CartItemDetails",
                column: "cartItemId",
                principalTable: "CartItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItemDetails_ShoesSpecifically_specificallyShoesId",
                table: "CartItemDetails",
                column: "specificallyShoesId",
                principalTable: "ShoesSpecifically",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItemDetails_CartItem_cartItemId",
                table: "CartItemDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItemDetails_ShoesSpecifically_specificallyShoesId",
                table: "CartItemDetails");

            migrationBuilder.DropColumn(
                name: "ShoesSize",
                table: "CartItemDetails");

            migrationBuilder.AlterColumn<int>(
                name: "specificallyShoesId",
                table: "CartItemDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "cartItemId",
                table: "CartItemDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItemDetails_CartItem_cartItemId",
                table: "CartItemDetails",
                column: "cartItemId",
                principalTable: "CartItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItemDetails_ShoesSpecifically_specificallyShoesId",
                table: "CartItemDetails",
                column: "specificallyShoesId",
                principalTable: "ShoesSpecifically",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
