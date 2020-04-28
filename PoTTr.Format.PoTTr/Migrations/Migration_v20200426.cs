using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PoTTr.Format.PoTTr.Migrations
{
    public partial class Migration_v20200426 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Metadata",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Copyright = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metadata", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectData",
                columns: table => new
                {
                    DataKey = table.Column<string>(maxLength: 50, nullable: false),
                    DataValue = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectData", x => x.DataKey);
                });

            migrationBuilder.CreateTable(
                name: "Agents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AgentType = table.Column<int>(nullable: false),
                    MetadataId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Agents_Metadata_MetadataId",
                        column: x => x.MetadataId,
                        principalTable: "Metadata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContentNode",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MetadataId = table.Column<int>(nullable: false),
                    Begin = table.Column<TimeSpan>(nullable: true),
                    End = table.Column<TimeSpan>(nullable: true),
                    NodeType = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    Order = table.Column<uint>(nullable: false),
                    ParentId = table.Column<int>(nullable: true),
                    AgentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentNode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContentNode_Agents_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Agents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContentNode_Metadata_MetadataId",
                        column: x => x.MetadataId,
                        principalTable: "Metadata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContentNode_ContentNode_ParentId",
                        column: x => x.ParentId,
                        principalTable: "ContentNode",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Names",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NameType = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: false),
                    AgentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Names", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Names_Agents_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Agents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Metadata",
                columns: new[] { "Id", "Copyright", "Description", "Title" },
                values: new object?[] { 1, null, null, null });

            migrationBuilder.InsertData(
                table: "ProjectData",
                columns: new[] { "DataKey", "DataValue" },
                values: new object[] { "ProjectName", "" });

            migrationBuilder.InsertData(
                table: "ProjectData",
                columns: new[] { "DataKey", "DataValue" },
                values: new object[] { "ProjectDate", "" });

            migrationBuilder.InsertData(
                table: "ProjectData",
                columns: new[] { "DataKey", "DataValue" },
                values: new object[] { "ProjectAuthor", "" });

            migrationBuilder.InsertData(
                table: "ContentNode",
                columns: new[] { "Id", "AgentId", "Begin", "End", "MetadataId", "NodeType", "Order", "ParentId", "Value" },
                values: new object?[] { 1, null, null, null, 1, 5, 0u, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_Agents_MetadataId",
                table: "Agents",
                column: "MetadataId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentNode_AgentId",
                table: "ContentNode",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentNode_MetadataId",
                table: "ContentNode",
                column: "MetadataId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentNode_ParentId",
                table: "ContentNode",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Names_AgentId",
                table: "Names",
                column: "AgentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContentNode");

            migrationBuilder.DropTable(
                name: "Names");

            migrationBuilder.DropTable(
                name: "ProjectData");

            migrationBuilder.DropTable(
                name: "Agents");

            migrationBuilder.DropTable(
                name: "Metadata");
        }
    }
}
