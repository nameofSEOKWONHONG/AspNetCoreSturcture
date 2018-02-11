using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Neon.Payment.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TA_ORDERS",
                columns: table => new
                {
                    ORD_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ORD_COST = table.Column<decimal>(type: "numeric(18, 0)", nullable: false),
                    ORD_USER_ID = table.Column<int>(nullable: false),
                    REG_DT = table.Column<DateTime>(type: "datetime", nullable: true),
                    REG_ID = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    REG_IP = table.Column<string>(unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TA_ORDERS", x => x.ORD_ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TA_ORDERS");
        }
    }
}
