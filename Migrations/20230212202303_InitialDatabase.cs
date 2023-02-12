using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FoodOrderAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: false),
                    IsReady = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreatedDateUTC = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsReady = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateUTC = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateUTC = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Total = table.Column<int>(type: "int", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TableId = table.Column<int>(type: "int", nullable: false),
                    CreatedDateUTC = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Tables_TableId",
                        column: x => x.TableId,
                        principalTable: "Tables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderActivities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDateUTC = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderActivities_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriceSnapshot = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => new { x.OrderId, x.ItemId });
                    table.ForeignKey(
                        name: "FK_OrderItem_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItem_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "CreatedBy", "CreatedDateUTC", "IsDeleted", "IsReady", "Name", "Price", "Type" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2023, 2, 12, 20, 23, 3, 271, DateTimeKind.Utc).AddTicks(297), false, true, "Spicy Crispy Chicken Sandwich", 35000, 0 },
                    { 2, null, new DateTime(2023, 2, 12, 20, 23, 3, 271, DateTimeKind.Utc).AddTicks(301), false, true, "Big Mac", 41000, 0 },
                    { 3, null, new DateTime(2023, 2, 12, 20, 23, 3, 271, DateTimeKind.Utc).AddTicks(302), false, true, "Sausage Burrito", 38000, 0 },
                    { 4, null, new DateTime(2023, 2, 12, 20, 23, 3, 271, DateTimeKind.Utc).AddTicks(303), false, true, "Ordinary Fries", 21000, 0 },
                    { 5, null, new DateTime(2023, 2, 12, 20, 23, 3, 271, DateTimeKind.Utc).AddTicks(304), false, false, "Pizza", 80000, 0 },
                    { 6, null, new DateTime(2023, 2, 12, 20, 23, 3, 271, DateTimeKind.Utc).AddTicks(304), false, true, "Sprite", 21000, 1 },
                    { 7, null, new DateTime(2023, 2, 12, 20, 23, 3, 271, DateTimeKind.Utc).AddTicks(305), false, true, "Cola-Cola", 21000, 1 },
                    { 8, null, new DateTime(2023, 2, 12, 20, 23, 3, 271, DateTimeKind.Utc).AddTicks(306), false, true, "Caramel Macchiato", 35000, 1 },
                    { 9, null, new DateTime(2023, 2, 12, 20, 23, 3, 271, DateTimeKind.Utc).AddTicks(307), false, false, "Cappuccino", 30000, 1 },
                    { 10, null, new DateTime(2023, 2, 12, 20, 23, 3, 271, DateTimeKind.Utc).AddTicks(307), false, false, "Caramel Cappuccino", 33000, 1 }
                });

            migrationBuilder.InsertData(
                table: "Tables",
                columns: new[] { "Id", "CreatedBy", "CreatedDateUTC", "IsDeleted", "IsReady", "Name" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2023, 2, 12, 20, 23, 3, 271, DateTimeKind.Utc).AddTicks(322), false, true, "Table 1" },
                    { 2, null, new DateTime(2023, 2, 12, 20, 23, 3, 271, DateTimeKind.Utc).AddTicks(326), false, true, "Table 2" },
                    { 3, null, new DateTime(2023, 2, 12, 20, 23, 3, 271, DateTimeKind.Utc).AddTicks(326), false, true, "Table 3" },
                    { 4, null, new DateTime(2023, 2, 12, 20, 23, 3, 271, DateTimeKind.Utc).AddTicks(327), false, true, "Table 4" },
                    { 5, null, new DateTime(2023, 2, 12, 20, 23, 3, 271, DateTimeKind.Utc).AddTicks(328), false, true, "Table 5" },
                    { 6, null, new DateTime(2023, 2, 12, 20, 23, 3, 271, DateTimeKind.Utc).AddTicks(328), false, true, "Table 6" },
                    { 7, null, new DateTime(2023, 2, 12, 20, 23, 3, 271, DateTimeKind.Utc).AddTicks(329), false, true, "Table 7" },
                    { 8, null, new DateTime(2023, 2, 12, 20, 23, 3, 271, DateTimeKind.Utc).AddTicks(330), false, true, "Table 8" },
                    { 9, null, new DateTime(2023, 2, 12, 20, 23, 3, 271, DateTimeKind.Utc).AddTicks(330), false, true, "Table 9" },
                    { 10, null, new DateTime(2023, 2, 12, 20, 23, 3, 271, DateTimeKind.Utc).AddTicks(331), false, true, "Table 10" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedDateUTC", "IsDeleted", "PasswordHash", "PasswordSalt", "Role", "Username" },
                values: new object[,]
                {
                    { new Guid("00279385-b518-4ec2-acbf-a180389f0b1e"), null, new DateTime(2023, 2, 12, 20, 23, 3, 271, DateTimeKind.Utc).AddTicks(285), false, new byte[] { 46, 39, 252, 94, 93, 208, 13, 11, 69, 174, 40, 89, 37, 93, 250, 217, 116, 110, 111, 4, 54, 185, 175, 20, 106, 3, 188, 215, 214, 180, 45, 50, 26, 194, 4, 45, 249, 140, 155, 50, 17, 108, 46, 209, 28, 47, 24, 110, 132, 186, 193, 167, 197, 186, 238, 160, 92, 45, 79, 67, 43, 136, 152, 251 }, new byte[] { 52, 8, 227, 237, 141, 118, 191, 175, 33, 105, 246, 231, 181, 197, 215, 126, 32, 143, 6, 140, 102, 93, 248, 73, 218, 22, 74, 227, 45, 34, 54, 192, 153, 81, 240, 117, 128, 110, 65, 121, 235, 140, 240, 89, 165, 247, 116, 30, 219, 127, 52, 219, 228, 56, 61, 121, 200, 234, 21, 156, 127, 147, 127, 75, 93, 243, 215, 199, 62, 30, 81, 35, 30, 3, 240, 94, 231, 151, 87, 89, 56, 53, 34, 52, 233, 29, 11, 49, 113, 171, 28, 146, 72, 76, 44, 83, 205, 124, 178, 116, 121, 103, 70, 38, 45, 131, 196, 88, 60, 18, 88, 65, 30, 63, 95, 76, 253, 227, 212, 56, 42, 241, 117, 232, 206, 128, 120, 133 }, "Waiter", "waiter2" },
                    { new Guid("32148306-c28b-4e81-8a5f-67dc9a5b7799"), null, new DateTime(2023, 2, 12, 20, 23, 3, 271, DateTimeKind.Utc).AddTicks(154), false, new byte[] { 12, 133, 88, 76, 183, 225, 169, 254, 147, 125, 223, 167, 121, 131, 40, 83, 32, 53, 126, 190, 162, 180, 213, 163, 181, 64, 77, 220, 78, 59, 147, 31, 26, 248, 146, 209, 128, 48, 104, 49, 142, 49, 144, 148, 18, 147, 158, 186, 105, 141, 240, 159, 109, 238, 21, 162, 52, 236, 223, 116, 93, 237, 26, 81 }, new byte[] { 184, 81, 34, 26, 59, 89, 225, 23, 186, 237, 86, 91, 134, 22, 180, 199, 186, 193, 179, 0, 209, 151, 23, 86, 52, 104, 70, 61, 35, 92, 221, 101, 44, 61, 242, 108, 28, 198, 190, 100, 255, 5, 10, 12, 111, 97, 69, 243, 200, 144, 168, 67, 67, 87, 108, 119, 91, 102, 91, 100, 54, 33, 25, 182, 88, 217, 161, 16, 171, 242, 95, 83, 161, 153, 24, 130, 76, 31, 93, 172, 236, 223, 219, 251, 23, 160, 81, 78, 174, 21, 22, 104, 117, 136, 204, 31, 128, 216, 179, 45, 244, 38, 238, 39, 12, 42, 223, 176, 201, 51, 157, 130, 239, 166, 39, 74, 210, 150, 102, 237, 57, 88, 140, 246, 197, 160, 250, 13 }, "Admin", "admin" },
                    { new Guid("6685b81c-94a0-4f02-8550-45f81a8d1446"), null, new DateTime(2023, 2, 12, 20, 23, 3, 271, DateTimeKind.Utc).AddTicks(253), false, new byte[] { 0, 232, 159, 36, 147, 145, 19, 136, 238, 74, 245, 129, 187, 72, 171, 242, 136, 102, 51, 237, 156, 108, 44, 58, 23, 49, 5, 198, 238, 66, 191, 214, 176, 211, 174, 243, 136, 17, 201, 23, 130, 240, 13, 16, 11, 101, 217, 193, 29, 66, 48, 196, 80, 32, 121, 159, 19, 134, 74, 149, 47, 116, 15, 92 }, new byte[] { 137, 255, 49, 129, 91, 49, 50, 229, 4, 80, 174, 53, 56, 148, 181, 177, 33, 185, 33, 215, 213, 53, 139, 101, 174, 167, 133, 100, 181, 91, 19, 75, 218, 117, 105, 184, 109, 61, 217, 191, 254, 244, 212, 222, 247, 241, 69, 119, 142, 211, 180, 40, 161, 150, 152, 228, 76, 178, 243, 127, 75, 105, 77, 46, 111, 110, 1, 124, 30, 3, 61, 254, 95, 3, 209, 168, 212, 252, 19, 231, 223, 91, 64, 197, 226, 241, 0, 231, 168, 216, 20, 48, 243, 4, 1, 241, 29, 44, 67, 240, 29, 16, 194, 218, 132, 71, 181, 241, 91, 128, 222, 188, 6, 113, 48, 210, 152, 218, 253, 36, 143, 123, 248, 31, 12, 230, 187, 155 }, "Waiter", "waiter1" },
                    { new Guid("a9bd7abd-9d95-4a4c-8aa4-06ea85dd0870"), null, new DateTime(2023, 2, 12, 20, 23, 3, 271, DateTimeKind.Utc).AddTicks(220), false, new byte[] { 250, 145, 160, 93, 195, 8, 244, 242, 69, 234, 223, 131, 38, 168, 36, 231, 13, 149, 3, 121, 250, 228, 109, 44, 228, 116, 176, 99, 72, 208, 85, 121, 16, 139, 216, 166, 81, 109, 101, 203, 93, 217, 94, 97, 162, 121, 83, 115, 246, 60, 57, 233, 186, 35, 23, 235, 49, 36, 22, 173, 147, 218, 213, 104 }, new byte[] { 101, 245, 96, 187, 229, 97, 252, 133, 253, 128, 187, 198, 205, 13, 171, 231, 235, 179, 69, 234, 217, 189, 8, 120, 25, 1, 138, 54, 4, 221, 40, 192, 8, 160, 2, 29, 108, 255, 80, 139, 48, 26, 206, 0, 62, 133, 90, 138, 98, 105, 221, 249, 245, 118, 111, 218, 192, 112, 180, 93, 199, 38, 136, 119, 90, 166, 238, 93, 171, 30, 60, 216, 5, 174, 112, 218, 36, 6, 99, 180, 15, 61, 93, 78, 105, 199, 184, 66, 107, 11, 208, 60, 145, 119, 198, 169, 89, 61, 238, 6, 55, 130, 80, 23, 165, 192, 212, 185, 126, 79, 18, 227, 107, 98, 133, 50, 22, 229, 48, 85, 231, 139, 219, 221, 252, 232, 211, 59 }, "Cashier", "cashier" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderActivities_OrderId",
                table: "OrderActivities",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ItemId",
                table: "OrderItem",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderNumber",
                table: "Orders",
                column: "OrderNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TableId",
                table: "Orders",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderActivities");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Tables");
        }
    }
}
