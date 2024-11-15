using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommerceAPI_IOON.Migrations
{
    /// <inheritdoc />
    public partial class SeedStates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insertar los valores predeterminados en la tabla States
            migrationBuilder.InsertData(
                table: "States",
                columns: new[] { "StateId", "StateName" },
                values: new object[,]
                {
                { Guid.NewGuid(), "Active" },
                { Guid.NewGuid(), "Inactive" },
                { Guid.NewGuid(), "Pending" },
                { Guid.NewGuid(), "Completed" },
                { Guid.NewGuid(), "Cancelled" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Eliminar los valores insertados si se revierte la migración
            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "StateName",
                keyValues: new object[] { "Active", "Inactive", "Pending", "Completed", "Cancelled" });
        }
    }
}
