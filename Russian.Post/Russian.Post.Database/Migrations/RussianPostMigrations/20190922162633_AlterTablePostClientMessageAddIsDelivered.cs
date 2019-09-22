using Microsoft.EntityFrameworkCore.Migrations;

namespace Russian.Post.Database.Migrations.RussianPostMigrations
{
    public partial class AlterTablePostClientMessageAddIsDelivered : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDelivered",
                table: "PostClientMessages",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelivered",
                table: "PostClientMessages");
        }
    }
}
