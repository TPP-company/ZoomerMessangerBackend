using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZM.Infrastructure.Persistence.App.Migrations;

/// <inheritdoc />
public partial class init : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.CreateTable(
			name: "Users",
			columns: table => new
			{
				Id = table.Column<Guid>(type: "uuid", nullable: false),
				About = table.Column<string>(type: "text", nullable: false),
				ExternalId = table.Column<Guid>(type: "uuid", nullable: false),
				AvatarId = table.Column<Guid>(type: "uuid", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_Users", x => x.Id);
			});

		migrationBuilder.CreateTable(
			name: "Conversations",
			columns: table => new
			{
				Id = table.Column<Guid>(type: "uuid", nullable: false),
				Message = table.Column<string>(type: "text", nullable: false),
				CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
				IsNew = table.Column<bool>(type: "boolean", nullable: false),
				SenderUserId = table.Column<Guid>(type: "uuid", nullable: false),
				ReceiverUserId = table.Column<Guid>(type: "uuid", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_Conversations", x => x.Id);
				table.ForeignKey(
					name: "FK_Conversations_Users_ReceiverUserId",
					column: x => x.ReceiverUserId,
					principalTable: "Users",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
				table.ForeignKey(
					name: "FK_Conversations_Users_SenderUserId",
					column: x => x.SenderUserId,
					principalTable: "Users",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateIndex(
			name: "IX_Conversations_ReceiverUserId",
			table: "Conversations",
			column: "ReceiverUserId");

		migrationBuilder.CreateIndex(
			name: "IX_Conversations_SenderUserId",
			table: "Conversations",
			column: "SenderUserId");
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(
			name: "Conversations");

		migrationBuilder.DropTable(
			name: "Users");
	}
}
