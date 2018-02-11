using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace NeonCore.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChangeLog",
                columns: table => new
                {
                    ChangeLogID = table.Column<int>(nullable: false),
                    ChangeDtm = table.Column<DateTime>(type: "datetime", nullable: true),
                    ClassNm = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    CurrentValue = table.Column<string>(unicode: false, maxLength: 500, nullable: false),
                    OriginalValue = table.Column<string>(unicode: false, maxLength: 500, nullable: false),
                    PropertyNm = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    UserNm = table.Column<string>(unicode: false, maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeLog", x => x.ChangeLogID);
                });

            migrationBuilder.CreateTable(
                name: "TA_USER",
                columns: table => new
                {
                    USER_IDX = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ADDR1 = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    ADDR2 = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    HP = table.Column<string>(unicode: false, maxLength: 11, nullable: false),
                    PWD = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    REG_ID = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    REG_IP = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    TEL = table.Column<string>(unicode: false, maxLength: 11, nullable: false),
                    USER_ID = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    USER_NM = table.Column<string>(unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TA_USER", x => x.USER_IDX);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChangeLog");

            migrationBuilder.DropTable(
                name: "TA_USER");
        }
    }
}
