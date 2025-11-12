using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class efeg1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_LogHistories_OrderID",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_LogHistories_Departments_CustomerId",
                table: "LogHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LogHistories",
                table: "LogHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Departments",
                table: "Departments");

            migrationBuilder.RenameTable(
                name: "LogHistories",
                newName: "Orders");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "Departments",
                newName: "Customers");

            migrationBuilder.RenameIndex(
                name: "IX_LogHistories_CustomerId",
                table: "Orders",
                newName: "IX_Orders_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_OrderID",
                table: "Products",
                newName: "IX_Products_OrderID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Orders_OrderID",
                table: "Products",
                column: "OrderID",
                principalTable: "Orders",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Orders_OrderID",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Employees");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "LogHistories");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "Departments");

            migrationBuilder.RenameIndex(
                name: "IX_Products_OrderID",
                table: "Employees",
                newName: "IX_Employees_OrderID");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_CustomerId",
                table: "LogHistories",
                newName: "IX_LogHistories_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LogHistories",
                table: "LogHistories",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Departments",
                table: "Departments",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_LogHistories_OrderID",
                table: "Employees",
                column: "OrderID",
                principalTable: "LogHistories",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_LogHistories_Departments_CustomerId",
                table: "LogHistories",
                column: "CustomerId",
                principalTable: "Departments",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
