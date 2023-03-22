using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PRN211_ShoesStore.Migrations
{
    public partial class ShoesStoreEnityCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Color",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    updateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Color", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    image = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    createBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    lastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    roleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    roleDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    creatDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Sale",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    discount = table.Column<double>(type: "float", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sale", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Shoes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    price = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    shoesDetails = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    launchDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    quantity = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shoes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Size",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sizeNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Size", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    roleId = table.Column<int>(type: "int", nullable: false),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                    table.ForeignKey(
                        name: "FK_User_Role_roleId",
                        column: x => x.roleId,
                        principalTable: "Role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryShoes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    categoryId = table.Column<int>(type: "int", nullable: false),
                    shoesId = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryShoes", x => x.id);
                    table.ForeignKey(
                        name: "FK_CategoryShoes_Category_categoryId",
                        column: x => x.categoryId,
                        principalTable: "Category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryShoes_Shoes_shoesId",
                        column: x => x.shoesId,
                        principalTable: "Shoes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoesColor",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    shoesId = table.Column<int>(type: "int", nullable: false),
                    colorId = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoesColor", x => x.id);
                    table.ForeignKey(
                        name: "FK_ShoesColor_Color_colorId",
                        column: x => x.colorId,
                        principalTable: "Color",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoesColor_Shoes_shoesId",
                        column: x => x.shoesId,
                        principalTable: "Shoes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoesImage",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    imageId = table.Column<int>(type: "int", nullable: false),
                    shoesId = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoesImage", x => x.id);
                    table.ForeignKey(
                        name: "FK_ShoesImage_Image_imageId",
                        column: x => x.imageId,
                        principalTable: "Image",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoesImage_Shoes_shoesId",
                        column: x => x.shoesId,
                        principalTable: "Shoes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoesSpecifically",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price = table.Column<decimal>(type: "Money", nullable: false),
                    quantity = table.Column<long>(type: "bigint", nullable: false),
                    createDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    shoesId = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoesSpecifically", x => x.id);
                    table.ForeignKey(
                        name: "FK_ShoesSpecifically_Shoes_shoesId",
                        column: x => x.shoesId,
                        principalTable: "Shoes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItem_User_userId",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    orderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<decimal>(type: "Money", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    createDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.orderId);
                    table.ForeignKey(
                        name: "FK_Order_User_userId",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ColorSpecificallyShoes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    specificallyShoesId = table.Column<int>(type: "int", nullable: false),
                    colorId = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColorSpecificallyShoes", x => x.id);
                    table.ForeignKey(
                        name: "FK_ColorSpecificallyShoes_Color_colorId",
                        column: x => x.colorId,
                        principalTable: "Color",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ColorSpecificallyShoes_ShoesSpecifically_specificallyShoesId",
                        column: x => x.specificallyShoesId,
                        principalTable: "ShoesSpecifically",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpecificallyShoesSale   ",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    specificallyShoesId = table.Column<int>(type: "int", nullable: false),
                    saleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecificallyShoesSale   ", x => x.id);
                    table.ForeignKey(
                        name: "FK_SpecificallyShoesSale   _Sale_saleId",
                        column: x => x.saleId,
                        principalTable: "Sale",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpecificallyShoesSale   _ShoesSpecifically_specificallyShoesId",
                        column: x => x.specificallyShoesId,
                        principalTable: "ShoesSpecifically",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpecificallyShoesSize",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    specificallyShoesId = table.Column<int>(type: "int", nullable: false),
                    sizeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecificallyShoesSize", x => x.id);
                    table.ForeignKey(
                        name: "FK_SpecificallyShoesSize_ShoesSpecifically_specificallyShoesId",
                        column: x => x.specificallyShoesId,
                        principalTable: "ShoesSpecifically",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpecificallyShoesSize_Size_sizeId",
                        column: x => x.sizeId,
                        principalTable: "Size",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItemDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cartItemId = table.Column<int>(type: "int", nullable: true),
                    specificallyShoesId = table.Column<int>(type: "int", nullable: true),
                    ShoesName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShoesImg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItemDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItemDetails_CartItem_cartItemId",
                        column: x => x.cartItemId,
                        principalTable: "CartItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CartItemDetails_ShoesSpecifically_specificallyShoesId",
                        column: x => x.specificallyShoesId,
                        principalTable: "ShoesSpecifically",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    quantity = table.Column<long>(type: "bigint", nullable: false),
                    price = table.Column<double>(type: "float", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    specificallyShoesId = table.Column<int>(type: "int", nullable: false),
                    orderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => x.id);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Order_orderId",
                        column: x => x.orderId,
                        principalTable: "Order",
                        principalColumn: "orderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetail_ShoesSpecifically_specificallyShoesId",
                        column: x => x.specificallyShoesId,
                        principalTable: "ShoesSpecifically",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_userId",
                table: "CartItem",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItemDetails_cartItemId",
                table: "CartItemDetails",
                column: "cartItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItemDetails_specificallyShoesId",
                table: "CartItemDetails",
                column: "specificallyShoesId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryShoes_categoryId",
                table: "CategoryShoes",
                column: "categoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryShoes_shoesId",
                table: "CategoryShoes",
                column: "shoesId");

            migrationBuilder.CreateIndex(
                name: "IX_ColorSpecificallyShoes_colorId",
                table: "ColorSpecificallyShoes",
                column: "colorId");

            migrationBuilder.CreateIndex(
                name: "IX_ColorSpecificallyShoes_specificallyShoesId",
                table: "ColorSpecificallyShoes",
                column: "specificallyShoesId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_userId",
                table: "Order",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_orderId",
                table: "OrderDetail",
                column: "orderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_specificallyShoesId",
                table: "OrderDetail",
                column: "specificallyShoesId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoesColor_colorId",
                table: "ShoesColor",
                column: "colorId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoesColor_shoesId",
                table: "ShoesColor",
                column: "shoesId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoesImage_imageId",
                table: "ShoesImage",
                column: "imageId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoesImage_shoesId",
                table: "ShoesImage",
                column: "shoesId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoesSpecifically_shoesId",
                table: "ShoesSpecifically",
                column: "shoesId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecificallyShoesSale   _saleId",
                table: "SpecificallyShoesSale   ",
                column: "saleId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecificallyShoesSale   _specificallyShoesId",
                table: "SpecificallyShoesSale   ",
                column: "specificallyShoesId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecificallyShoesSize_sizeId",
                table: "SpecificallyShoesSize",
                column: "sizeId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecificallyShoesSize_specificallyShoesId",
                table: "SpecificallyShoesSize",
                column: "specificallyShoesId");

            migrationBuilder.CreateIndex(
                name: "IX_User_roleId",
                table: "User",
                column: "roleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItemDetails");

            migrationBuilder.DropTable(
                name: "CategoryShoes");

            migrationBuilder.DropTable(
                name: "ColorSpecificallyShoes");

            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "ShoesColor");

            migrationBuilder.DropTable(
                name: "ShoesImage");

            migrationBuilder.DropTable(
                name: "SpecificallyShoesSale   ");

            migrationBuilder.DropTable(
                name: "SpecificallyShoesSize");

            migrationBuilder.DropTable(
                name: "CartItem");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Color");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "Sale");

            migrationBuilder.DropTable(
                name: "ShoesSpecifically");

            migrationBuilder.DropTable(
                name: "Size");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Shoes");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
