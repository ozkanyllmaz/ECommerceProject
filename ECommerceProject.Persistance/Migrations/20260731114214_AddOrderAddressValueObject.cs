using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceProject.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderAddressValueObject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShippingAddress",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "Billing_City",
                table: "Orders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Billing_CompanyName",
                table: "Orders",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Billing_District",
                table: "Orders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Billing_FirstName",
                table: "Orders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Billing_FullAddress",
                table: "Orders",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Billing_InvoiceType",
                table: "Orders",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Billing_LastName",
                table: "Orders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Billing_PhoneNumber",
                table: "Orders",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Billing_TaxNumber",
                table: "Orders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Billing_TaxOffice",
                table: "Orders",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Shipping_City",
                table: "Orders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Shipping_CompanyName",
                table: "Orders",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Shipping_District",
                table: "Orders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Shipping_FirstName",
                table: "Orders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Shipping_FullAddress",
                table: "Orders",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Shipping_InvoiceType",
                table: "Orders",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Shipping_LastName",
                table: "Orders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Shipping_PhoneNumber",
                table: "Orders",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Shipping_TaxNumber",
                table: "Orders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Shipping_TaxOffice",
                table: "Orders",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Billing_City",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Billing_CompanyName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Billing_District",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Billing_FirstName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Billing_FullAddress",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Billing_InvoiceType",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Billing_LastName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Billing_PhoneNumber",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Billing_TaxNumber",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Billing_TaxOffice",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Shipping_City",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Shipping_CompanyName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Shipping_District",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Shipping_FirstName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Shipping_FullAddress",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Shipping_InvoiceType",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Shipping_LastName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Shipping_PhoneNumber",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Shipping_TaxNumber",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Shipping_TaxOffice",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "ShippingAddress",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
