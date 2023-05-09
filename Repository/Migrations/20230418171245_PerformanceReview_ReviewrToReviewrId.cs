using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class PerformanceReview_ReviewrToReviewrId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reviewer",
                table: "PerformanceReviews");

            migrationBuilder.AddColumn<int>(
                name: "ReviewerId",
                table: "PerformanceReviews",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReviewerId",
                table: "PerformanceReviews");

            migrationBuilder.AddColumn<string>(
                name: "Reviewer",
                table: "PerformanceReviews",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
