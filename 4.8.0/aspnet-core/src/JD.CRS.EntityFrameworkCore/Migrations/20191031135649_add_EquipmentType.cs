using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JD.CRS.Migrations
{
    public partial class add_EquipmentType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Courses");

            migrationBuilder.CreateTable(
                name: "EquipmentType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    Code = table.Column<string>(maxLength: 64, nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    BriefName = table.Column<string>(maxLength: 64, nullable: true),
                    Remark = table.Column<string>(maxLength: 512, nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentType", x => x.Id);
                    table.UniqueConstraint("UK_EquipmentType_Code", x => x.Code);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquipmentType");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Courses",
                nullable: true);
        }
    }
}
