// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Wolf.API.Infrastructure;

namespace Wolf.API.Infrastructure.migrations
{
    [DbContext(typeof(DomainDbContext))]
    [Migration("20220501025859_InitDb")]
    partial class InitDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.15")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("QuanLySoTaiChinh.API.Entity.Sys_AuthToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AccessToken")
                        .HasMaxLength(800)
                        .HasColumnType("nvarchar(800)");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTimeOffset>("CreatedDateTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("LoginName")
                        .HasMaxLength(55)
                        .HasColumnType("nvarchar(55)");

                    b.Property<string>("RefeshToken")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTimeOffset?>("UpdatedDateTime")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.ToTable("Sys_Authtokens");
                });

            modelBuilder.Entity("QuanLySoTaiChinh.API.Entity.Sys_Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .HasMaxLength(55)
                        .HasColumnType("nvarchar(55)");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTimeOffset>("CreatedDateTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .HasMaxLength(55)
                        .HasColumnType("nvarchar(55)");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTimeOffset?>("UpdatedDateTime")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.ToTable("Sys_Categories");
                });

            modelBuilder.Entity("QuanLySoTaiChinh.API.Entity.Sys_Config", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .HasMaxLength(55)
                        .HasColumnType("nvarchar(55)");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTimeOffset>("CreatedDateTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTimeOffset?>("UpdatedDateTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Value")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.ToTable("Sys_Configs");
                });

            modelBuilder.Entity("QuanLySoTaiChinh.API.Entity.Sys_File", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTimeOffset>("CreatedDateTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Extension")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ObjectId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ObjectType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Path")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTimeOffset?>("UpdatedDateTime")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.ToTable("Sys_Files");
                });

            modelBuilder.Entity("QuanLySoTaiChinh.API.Entity.Sys_Organization", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .HasMaxLength(55)
                        .HasColumnType("nvarchar(55)");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTimeOffset>("CreatedDateTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .HasMaxLength(55)
                        .HasColumnType("nvarchar(55)");

                    b.Property<Guid>("ParentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTimeOffset?>("UpdatedDateTime")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.ToTable("Sys_Organizations");
                });

            modelBuilder.Entity("QuanLySoTaiChinh.API.Entity.Sys_Permission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTimeOffset>("CreatedDateTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsFunc")
                        .HasColumnType("bit");

                    b.Property<Guid>("ResourceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTimeOffset?>("UpdatedDateTime")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.ToTable("Sys_Permissions");
                });

            modelBuilder.Entity("QuanLySoTaiChinh.API.Entity.Sys_Resource", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .HasMaxLength(55)
                        .HasColumnType("nvarchar(55)");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTimeOffset>("CreatedDateTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .HasMaxLength(55)
                        .HasColumnType("nvarchar(55)");

                    b.Property<Guid>("ParentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTimeOffset?>("UpdatedDateTime")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.ToTable("Sys_Resources");
                });

            modelBuilder.Entity("QuanLySoTaiChinh.API.Entity.Sys_Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .HasMaxLength(55)
                        .HasColumnType("nvarchar(55)");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTimeOffset>("CreatedDateTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .HasMaxLength(55)
                        .HasColumnType("nvarchar(55)");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTimeOffset?>("UpdatedDateTime")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.ToTable("Sys_Roles");
                });

            modelBuilder.Entity("QuanLySoTaiChinh.API.Entity.Sys_User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTimeOffset>("CreatedDateTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Email")
                        .HasMaxLength(55)
                        .HasColumnType("nvarchar(55)");

                    b.Property<string>("FullName")
                        .HasMaxLength(55)
                        .HasColumnType("nvarchar(55)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSystem")
                        .HasColumnType("bit");

                    b.Property<string>("LoginName")
                        .HasMaxLength(55)
                        .HasColumnType("nvarchar(55)");

                    b.Property<string>("PassWord")
                        .HasMaxLength(55)
                        .HasColumnType("nvarchar(55)");

                    b.Property<string>("Phone")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTimeOffset?>("UpdatedDateTime")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.ToTable("Sys_Users");
                });

            modelBuilder.Entity("QuanLySoTaiChinh.API.Entity.Sys_User_Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("bit");

                    b.Property<Guid>("OrganId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Sys_Users_Roles");
                });

            modelBuilder.Entity("QuanLySoTaiChinh.API.Models.VDT_DU_AN", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .HasMaxLength(55)
                        .HasColumnType("nvarchar(55)");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTimeOffset>("CreatedDateTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .HasMaxLength(55)
                        .HasColumnType("nvarchar(55)");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTimeOffset?>("UpdatedDateTime")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.ToTable("VDT_DU_AN");
                });

            modelBuilder.Entity("QuanLySoTaiChinh.API.Models.VDT_N_QT_09_0101", b =>
                {
                    b.Property<Guid>("H1")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("A1")
                        .HasColumnType("int");

                    b.Property<string>("A2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("A3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("A4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("A5")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("A6")
                        .HasColumnType("int");

                    b.Property<string>("H2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("H3")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("H4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("H5")
                        .HasColumnType("int");

                    b.Property<int?>("R1")
                        .HasColumnType("int");

                    b.Property<int?>("R2")
                        .HasColumnType("int");

                    b.Property<string>("R3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("R4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("R5")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("R6")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("H1");

                    b.ToTable("VDT_N_QT_09_0101");
                });

            modelBuilder.Entity("QuanLySoTaiChinh.API.Models.VDT_N_QT_09_0102", b =>
                {
                    b.Property<Guid>("H1")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("A1")
                        .HasColumnType("int");

                    b.Property<string>("A2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("A3")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("A4")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("A5")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("A6")
                        .HasColumnType("int");

                    b.Property<string>("H2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("H3")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("H4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("H5")
                        .HasColumnType("int");

                    b.Property<int?>("R1")
                        .HasColumnType("int");

                    b.Property<int?>("R2")
                        .HasColumnType("int");

                    b.Property<string>("R3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("R4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("R5")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("R6")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("H1");

                    b.ToTable("VDT_N_QT_09_0102");
                });

            modelBuilder.Entity("QuanLySoTaiChinh.API.Models.VDT_N_QT_09_0103", b =>
                {
                    b.Property<Guid>("H1")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("A1")
                        .HasColumnType("int");

                    b.Property<string>("A2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("A3")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("A4")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("A5")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("A6")
                        .HasColumnType("int");

                    b.Property<string>("H2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("H3")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("H4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("H5")
                        .HasColumnType("int");

                    b.Property<int?>("R1")
                        .HasColumnType("int");

                    b.Property<int?>("R2")
                        .HasColumnType("int");

                    b.Property<string>("R3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("R4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("R5")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("R6")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("H1");

                    b.ToTable("VDT_N_QT_09_0103");
                });

            modelBuilder.Entity("QuanLySoTaiChinh.API.Models.VDT_N_QT_09_0104", b =>
                {
                    b.Property<Guid>("H1")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("A1")
                        .HasColumnType("int");

                    b.Property<string>("A2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("A3")
                        .HasColumnType("int");

                    b.Property<decimal?>("A4")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("A5")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("A6")
                        .HasColumnType("int");

                    b.Property<string>("H2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("H3")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("H4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("H5")
                        .HasColumnType("int");

                    b.Property<decimal?>("R1")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("R2")
                        .HasColumnType("int");

                    b.Property<string>("R3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("R4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("R5")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("R6")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("H1");

                    b.ToTable("VDT_N_QT_09_0104");
                });

            modelBuilder.Entity("QuanLySoTaiChinh.API.Models.VDT_N_QT_09_0105", b =>
                {
                    b.Property<Guid>("H1")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("A1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("A2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("A3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("A4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("A5")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("A6")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("H2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("H3")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("H4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("H5")
                        .HasColumnType("int");

                    b.HasKey("H1");

                    b.ToTable("VDT_N_QT_09_0105");
                });
#pragma warning restore 612, 618
        }
    }
}
