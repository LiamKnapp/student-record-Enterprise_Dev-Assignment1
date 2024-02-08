using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudentRecordApp.Migrations
{
    /// <inheritdoc />
    public partial class StudentMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Programs",
                columns: table => new
                {
                    ProgramId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programs", x => x.ProgramId);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentBirthDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentGPA = table.Column<double>(type: "float", nullable: false),
                    ProgramId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_Students_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Programs",
                        principalColumn: "ProgramId");
                });

            migrationBuilder.InsertData(
                table: "Programs",
                columns: new[] { "ProgramId", "Name" },
                values: new object[,]
                {
                    { "CSI", "Computer Science" },
                    { "ENG", "English" },
                    { "FRE", "French" },
                    { "HIS", "History" },
                    { "STA", "Statistics" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentId", "ProgramId", "StudentBirthDate", "StudentFirstName", "StudentGPA", "StudentLastName" },
                values: new object[,]
                {
                    { 1, "FRE", "1/01/2000", "John", 3.7999999999999998, "Doe" },
                    { 2, "STA", "15/05/1999", "Jane", 3.5, "Smith" },
                    { 3, "HIS", "20/08/2001", "Alice", 4.0, "Johnson" },
                    { 4, "ENG", "10/03/2002", "Bob", 3.2000000000000002, "Williams" },
                    { 5, "CSI", "11/08/2003", "Liam", 3.2000000000000002, "Knapp" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_ProgramId",
                table: "Students",
                column: "ProgramId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Programs");
        }
    }
}
