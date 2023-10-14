using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace StudentManagementApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Country = table.Column<string>(type: "longtext", nullable: false),
                    City = table.Column<string>(type: "longtext", nullable: false),
                    Street = table.Column<string>(type: "longtext", nullable: false),
                    Number = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_address", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "faculty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_faculty", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "student",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(type: "longtext", nullable: false),
                    LastName = table.Column<string>(type: "longtext", nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: false),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: false),
                    AddressModelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_student", x => x.Id);
                    table.ForeignKey(
                        name: "FK_student_address_AddressModelId",
                        column: x => x.AddressModelId,
                        principalTable: "address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "teacher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(type: "longtext", nullable: false),
                    LastName = table.Column<string>(type: "longtext", nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: false),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: false),
                    AddressModelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teacher", x => x.Id);
                    table.ForeignKey(
                        name: "FK_teacher_address_AddressModelId",
                        column: x => x.AddressModelId,
                        principalTable: "address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "course",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FacultyId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "longtext", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    CourseType = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Semester = table.Column<int>(type: "int", nullable: false),
                    CourseHours = table.Column<int>(type: "int", nullable: false),
                    SeminarHours = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    LaboratoryHours = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    ProjectHours = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    PracticeHours = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    ExaminationType = table.Column<int>(type: "int", nullable: false),
                    Credits = table.Column<byte>(type: "tinyint unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_course", x => x.Id);
                    table.ForeignKey(
                        name: "FK_course_faculty_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "faculty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "whatFacultyAStudentAttends",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    FacultyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_whatFacultyAStudentAttends", x => x.Id);
                    table.ForeignKey(
                        name: "FK_whatFacultyAStudentAttends_faculty_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "faculty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_whatFacultyAStudentAttends_student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "facultyCourses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FacultyId = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    Semester = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    Course1Id = table.Column<int>(type: "int", nullable: false),
                    Course2Id = table.Column<int>(type: "int", nullable: false),
                    Course3Id = table.Column<int>(type: "int", nullable: false),
                    Course4Id = table.Column<int>(type: "int", nullable: false),
                    Course5Id = table.Column<int>(type: "int", nullable: false),
                    Course6Id = table.Column<int>(type: "int", nullable: false),
                    Course7Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_facultyCourses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_facultyCourses_course_Course1Id",
                        column: x => x.Course1Id,
                        principalTable: "course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_facultyCourses_course_Course2Id",
                        column: x => x.Course2Id,
                        principalTable: "course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_facultyCourses_course_Course3Id",
                        column: x => x.Course3Id,
                        principalTable: "course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_facultyCourses_course_Course4Id",
                        column: x => x.Course4Id,
                        principalTable: "course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_facultyCourses_course_Course5Id",
                        column: x => x.Course5Id,
                        principalTable: "course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_facultyCourses_course_Course6Id",
                        column: x => x.Course6Id,
                        principalTable: "course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_facultyCourses_course_Course7Id",
                        column: x => x.Course7Id,
                        principalTable: "course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_facultyCourses_faculty_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "faculty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "grade",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    CourseModelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_grade", x => x.Id);
                    table.ForeignKey(
                        name: "FK_grade_course_CourseModelId",
                        column: x => x.CourseModelId,
                        principalTable: "course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_grade_student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_course_FacultyId",
                table: "course",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_facultyCourses_Course1Id",
                table: "facultyCourses",
                column: "Course1Id");

            migrationBuilder.CreateIndex(
                name: "IX_facultyCourses_Course2Id",
                table: "facultyCourses",
                column: "Course2Id");

            migrationBuilder.CreateIndex(
                name: "IX_facultyCourses_Course3Id",
                table: "facultyCourses",
                column: "Course3Id");

            migrationBuilder.CreateIndex(
                name: "IX_facultyCourses_Course4Id",
                table: "facultyCourses",
                column: "Course4Id");

            migrationBuilder.CreateIndex(
                name: "IX_facultyCourses_Course5Id",
                table: "facultyCourses",
                column: "Course5Id");

            migrationBuilder.CreateIndex(
                name: "IX_facultyCourses_Course6Id",
                table: "facultyCourses",
                column: "Course6Id");

            migrationBuilder.CreateIndex(
                name: "IX_facultyCourses_Course7Id",
                table: "facultyCourses",
                column: "Course7Id");

            migrationBuilder.CreateIndex(
                name: "IX_facultyCourses_FacultyId",
                table: "facultyCourses",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_grade_CourseModelId",
                table: "grade",
                column: "CourseModelId");

            migrationBuilder.CreateIndex(
                name: "IX_grade_StudentId",
                table: "grade",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_student_AddressModelId",
                table: "student",
                column: "AddressModelId");

            migrationBuilder.CreateIndex(
                name: "IX_teacher_AddressModelId",
                table: "teacher",
                column: "AddressModelId");

            migrationBuilder.CreateIndex(
                name: "IX_whatFacultyAStudentAttends_FacultyId",
                table: "whatFacultyAStudentAttends",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_whatFacultyAStudentAttends_StudentId",
                table: "whatFacultyAStudentAttends",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "facultyCourses");

            migrationBuilder.DropTable(
                name: "grade");

            migrationBuilder.DropTable(
                name: "teacher");

            migrationBuilder.DropTable(
                name: "whatFacultyAStudentAttends");

            migrationBuilder.DropTable(
                name: "course");

            migrationBuilder.DropTable(
                name: "student");

            migrationBuilder.DropTable(
                name: "faculty");

            migrationBuilder.DropTable(
                name: "address");
        }
    }
}
