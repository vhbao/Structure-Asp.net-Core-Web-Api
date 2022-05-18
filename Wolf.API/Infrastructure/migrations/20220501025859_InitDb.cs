using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wolf.API.Infrastructure.migrations
{
    public partial class InitDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sys_Authtokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginName = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: true),
                    AccessToken = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    RefeshToken = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Authtokens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Configs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: true),
                    Value = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Configs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Files",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Path = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ObjectId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ObjectType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Files", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Organizations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Organizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Permissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsFunc = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Resources",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Resources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: true),
                    LoginName = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: true),
                    PassWord = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsSystem = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Users_Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Users_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_DU_AN",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_DU_AN", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_N_QT_09_0101",
                columns: table => new
                {
                    H1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    H2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    H3 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    H4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    H5 = table.Column<int>(type: "int", nullable: true),
                    A1 = table.Column<int>(type: "int", nullable: true),
                    A2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    A3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    A4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    A5 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    A6 = table.Column<int>(type: "int", nullable: true),
                    R1 = table.Column<int>(type: "int", nullable: true),
                    R2 = table.Column<int>(type: "int", nullable: true),
                    R3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    R4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    R5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    R6 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_N_QT_09_0101", x => x.H1);
                });

            migrationBuilder.CreateTable(
                name: "VDT_N_QT_09_0102",
                columns: table => new
                {
                    H1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    H2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    H3 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    H4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    H5 = table.Column<int>(type: "int", nullable: true),
                    A1 = table.Column<int>(type: "int", nullable: true),
                    A2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    A3 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    A4 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    A5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    A6 = table.Column<int>(type: "int", nullable: true),
                    R1 = table.Column<int>(type: "int", nullable: true),
                    R2 = table.Column<int>(type: "int", nullable: true),
                    R3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    R4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    R5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    R6 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_N_QT_09_0102", x => x.H1);
                });

            migrationBuilder.CreateTable(
                name: "VDT_N_QT_09_0103",
                columns: table => new
                {
                    H1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    H2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    H3 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    H4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    H5 = table.Column<int>(type: "int", nullable: true),
                    A1 = table.Column<int>(type: "int", nullable: true),
                    A2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    A3 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    A4 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    A5 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    A6 = table.Column<int>(type: "int", nullable: true),
                    R1 = table.Column<int>(type: "int", nullable: true),
                    R2 = table.Column<int>(type: "int", nullable: true),
                    R3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    R4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    R5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    R6 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_N_QT_09_0103", x => x.H1);
                });

            migrationBuilder.CreateTable(
                name: "VDT_N_QT_09_0104",
                columns: table => new
                {
                    H1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    H2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    H3 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    H4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    H5 = table.Column<int>(type: "int", nullable: true),
                    A1 = table.Column<int>(type: "int", nullable: true),
                    A2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    A3 = table.Column<int>(type: "int", nullable: true),
                    A4 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    A5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    A6 = table.Column<int>(type: "int", nullable: true),
                    R1 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    R2 = table.Column<int>(type: "int", nullable: true),
                    R3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    R4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    R5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    R6 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_N_QT_09_0104", x => x.H1);
                });

            migrationBuilder.CreateTable(
                name: "VDT_N_QT_09_0105",
                columns: table => new
                {
                    H1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    H2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    H3 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    H4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    H5 = table.Column<int>(type: "int", nullable: true),
                    A1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    A2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    A3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    A4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    A5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    A6 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_N_QT_09_0105", x => x.H1);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sys_Authtokens");

            migrationBuilder.DropTable(
                name: "Sys_Categories");

            migrationBuilder.DropTable(
                name: "Sys_Configs");

            migrationBuilder.DropTable(
                name: "Sys_Files");

            migrationBuilder.DropTable(
                name: "Sys_Organizations");

            migrationBuilder.DropTable(
                name: "Sys_Permissions");

            migrationBuilder.DropTable(
                name: "Sys_Resources");

            migrationBuilder.DropTable(
                name: "Sys_Roles");

            migrationBuilder.DropTable(
                name: "Sys_Users");

            migrationBuilder.DropTable(
                name: "Sys_Users_Roles");

            migrationBuilder.DropTable(
                name: "VDT_DU_AN");

            migrationBuilder.DropTable(
                name: "VDT_N_QT_09_0101");

            migrationBuilder.DropTable(
                name: "VDT_N_QT_09_0102");

            migrationBuilder.DropTable(
                name: "VDT_N_QT_09_0103");

            migrationBuilder.DropTable(
                name: "VDT_N_QT_09_0104");

            migrationBuilder.DropTable(
                name: "VDT_N_QT_09_0105");
        }
    }
}
