using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Week5.Migrations
{
    /// <inheritdoc />
    public partial class FixTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    ClassID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.ClassID);
                });

            migrationBuilder.CreateTable(
                name: "Majors",
                columns: table => new
                {
                    MajorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MajorName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Majors", x => x.MajorID);
                });

            migrationBuilder.CreateTable(
                name: "Professors",
                columns: table => new
                {
                    ProfessorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfessorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfessorSurname = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professors", x => x.ProfessorID);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentSurname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfessorID = table.Column<int>(type: "int", nullable: false),
                    MajorID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentID);
                    table.ForeignKey(
                        name: "FK_Students_Majors_MajorID",
                        column: x => x.MajorID,
                        principalTable: "Majors",
                        principalColumn: "MajorID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Students_Professors_ProfessorID",
                        column: x => x.ProfessorID,
                        principalTable: "Professors",
                        principalColumn: "ProfessorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BehaviorScores",
                columns: table => new
                {
                    ScoreID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BehaviorScores", x => x.ScoreID);
                    table.ForeignKey(
                        name: "FK_BehaviorScores_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentClasses",
                columns: table => new
                {
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    ClassID = table.Column<int>(type: "int", nullable: false),
                    MajorID = table.Column<int>(type: "int", nullable: true),
                    ProfessorID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentClasses", x => new { x.StudentID, x.ClassID });
                    table.ForeignKey(
                        name: "FK_StudentClasses_Classes_ClassID",
                        column: x => x.ClassID,
                        principalTable: "Classes",
                        principalColumn: "ClassID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentClasses_Majors_MajorID",
                        column: x => x.MajorID,
                        principalTable: "Majors",
                        principalColumn: "MajorID");
                    table.ForeignKey(
                        name: "FK_StudentClasses_Professors_ProfessorID",
                        column: x => x.ProfessorID,
                        principalTable: "Professors",
                        principalColumn: "ProfessorID");
                    table.ForeignKey(
                        name: "FK_StudentClasses_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BehaviorScores_StudentID",
                table: "BehaviorScores",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentClasses_ClassID",
                table: "StudentClasses",
                column: "ClassID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentClasses_MajorID",
                table: "StudentClasses",
                column: "MajorID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentClasses_ProfessorID",
                table: "StudentClasses",
                column: "ProfessorID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_MajorID",
                table: "Students",
                column: "MajorID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ProfessorID",
                table: "Students",
                column: "ProfessorID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BehaviorScores");

            migrationBuilder.DropTable(
                name: "StudentClasses");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Majors");

            migrationBuilder.DropTable(
                name: "Professors");
        }
    }
}
