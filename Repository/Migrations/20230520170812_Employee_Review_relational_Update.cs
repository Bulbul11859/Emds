using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class Employee_Review_relational_Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PerformanceReviews_ReviewerId",
                table: "PerformanceReviews",
                column: "ReviewerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PerformanceReviews_Employees_ReviewerId",
                table: "PerformanceReviews",
                column: "ReviewerId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PerformanceReviews_Employees_ReviewerId",
                table: "PerformanceReviews");

            migrationBuilder.DropIndex(
                name: "IX_PerformanceReviews_ReviewerId",
                table: "PerformanceReviews");
        }
    }
}
