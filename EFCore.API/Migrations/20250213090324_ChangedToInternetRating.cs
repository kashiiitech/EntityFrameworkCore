using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore.API.Migrations
{
    /// <inheritdoc />
    public partial class ChangedToInternetRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ImdbRating",
                table: "Pictures",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m
                ); // perseve the data

            migrationBuilder.RenameColumn(
                name: "ImdbRating",
                table: "Pictures",
                newName: "InternetRating");
            
            // migrationBuilder.DropColumn(
            //     name: "ImdbRating",
            //     table: "Pictures");
            //
            // migrationBuilder.AddColumn<decimal>(
            //     name: "InternetRating",
            //     table: "Pictures",
            //     type: "decimal(18,2)",
            //     nullable: false,
            //     defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.AlterColumn<int>(
                name: "InternetRating",
                table: "Pictures",
                type: "int",
                nullable: false,
                defaultValue: 0
            );

            migrationBuilder.RenameColumn(
                name: "InternetRating",
                table: "Pictures",
                newName: "ImdbRating");
            
            
            // migrationBuilder.DropColumn(
            //     name: "InternetRating",
            //     table: "Pictures");
            //
            // migrationBuilder.AddColumn<int>(
            //     name: "ImdbRating",
            //     table: "Pictures",
            //     type: "int",
            //     nullable: false,
            //     defaultValue: 0);
        }
    }
}
