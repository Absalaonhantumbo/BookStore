using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class updateMigration5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "SupplierTypes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "Suppliers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "PublishingCompanies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "KnowledgeAreas",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "DeweyDecimalClassifications",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "Countries",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "CostumerTypes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "Costumers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "CostumerBuyBooks",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "CompanyTypes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "Books",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "Authors",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "AuthorBooks",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SupplierTypes_CreatedByUserId",
                table: "SupplierTypes",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_CreatedByUserId",
                table: "Suppliers",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PublishingCompanies_CreatedByUserId",
                table: "PublishingCompanies",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_KnowledgeAreas_CreatedByUserId",
                table: "KnowledgeAreas",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DeweyDecimalClassifications_CreatedByUserId",
                table: "DeweyDecimalClassifications",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_CreatedByUserId",
                table: "Countries",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CostumerTypes_CreatedByUserId",
                table: "CostumerTypes",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Costumers_CreatedByUserId",
                table: "Costumers",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CostumerBuyBooks_CreatedByUserId",
                table: "CostumerBuyBooks",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyTypes_CreatedByUserId",
                table: "CompanyTypes",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_CreatedByUserId",
                table: "Books",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_CreatedByUserId",
                table: "Authors",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBooks_CreatedByUserId",
                table: "AuthorBooks",
                column: "CreatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorBooks_AspNetUsers_CreatedByUserId",
                table: "AuthorBooks",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_AspNetUsers_CreatedByUserId",
                table: "Authors",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_AspNetUsers_CreatedByUserId",
                table: "Books",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyTypes_AspNetUsers_CreatedByUserId",
                table: "CompanyTypes",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CostumerBuyBooks_AspNetUsers_CreatedByUserId",
                table: "CostumerBuyBooks",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Costumers_AspNetUsers_CreatedByUserId",
                table: "Costumers",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CostumerTypes_AspNetUsers_CreatedByUserId",
                table: "CostumerTypes",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Countries_AspNetUsers_CreatedByUserId",
                table: "Countries",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeweyDecimalClassifications_AspNetUsers_CreatedByUserId",
                table: "DeweyDecimalClassifications",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KnowledgeAreas_AspNetUsers_CreatedByUserId",
                table: "KnowledgeAreas",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PublishingCompanies_AspNetUsers_CreatedByUserId",
                table: "PublishingCompanies",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_AspNetUsers_CreatedByUserId",
                table: "Suppliers",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierTypes_AspNetUsers_CreatedByUserId",
                table: "SupplierTypes",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorBooks_AspNetUsers_CreatedByUserId",
                table: "AuthorBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_Authors_AspNetUsers_CreatedByUserId",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_AspNetUsers_CreatedByUserId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyTypes_AspNetUsers_CreatedByUserId",
                table: "CompanyTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_CostumerBuyBooks_AspNetUsers_CreatedByUserId",
                table: "CostumerBuyBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_Costumers_AspNetUsers_CreatedByUserId",
                table: "Costumers");

            migrationBuilder.DropForeignKey(
                name: "FK_CostumerTypes_AspNetUsers_CreatedByUserId",
                table: "CostumerTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Countries_AspNetUsers_CreatedByUserId",
                table: "Countries");

            migrationBuilder.DropForeignKey(
                name: "FK_DeweyDecimalClassifications_AspNetUsers_CreatedByUserId",
                table: "DeweyDecimalClassifications");

            migrationBuilder.DropForeignKey(
                name: "FK_KnowledgeAreas_AspNetUsers_CreatedByUserId",
                table: "KnowledgeAreas");

            migrationBuilder.DropForeignKey(
                name: "FK_PublishingCompanies_AspNetUsers_CreatedByUserId",
                table: "PublishingCompanies");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_AspNetUsers_CreatedByUserId",
                table: "Suppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierTypes_AspNetUsers_CreatedByUserId",
                table: "SupplierTypes");

            migrationBuilder.DropIndex(
                name: "IX_SupplierTypes_CreatedByUserId",
                table: "SupplierTypes");

            migrationBuilder.DropIndex(
                name: "IX_Suppliers_CreatedByUserId",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_PublishingCompanies_CreatedByUserId",
                table: "PublishingCompanies");

            migrationBuilder.DropIndex(
                name: "IX_KnowledgeAreas_CreatedByUserId",
                table: "KnowledgeAreas");

            migrationBuilder.DropIndex(
                name: "IX_DeweyDecimalClassifications_CreatedByUserId",
                table: "DeweyDecimalClassifications");

            migrationBuilder.DropIndex(
                name: "IX_Countries_CreatedByUserId",
                table: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_CostumerTypes_CreatedByUserId",
                table: "CostumerTypes");

            migrationBuilder.DropIndex(
                name: "IX_Costumers_CreatedByUserId",
                table: "Costumers");

            migrationBuilder.DropIndex(
                name: "IX_CostumerBuyBooks_CreatedByUserId",
                table: "CostumerBuyBooks");

            migrationBuilder.DropIndex(
                name: "IX_CompanyTypes_CreatedByUserId",
                table: "CompanyTypes");

            migrationBuilder.DropIndex(
                name: "IX_Books_CreatedByUserId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Authors_CreatedByUserId",
                table: "Authors");

            migrationBuilder.DropIndex(
                name: "IX_AuthorBooks_CreatedByUserId",
                table: "AuthorBooks");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "SupplierTypes");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "PublishingCompanies");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "KnowledgeAreas");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "DeweyDecimalClassifications");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "CostumerTypes");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Costumers");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "CostumerBuyBooks");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "CompanyTypes");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "AuthorBooks");
        }
    }
}
