using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZM.Infrastructure.Persistence.App.Migrations
{
    /// <inheritdoc />
    public partial class RenameChatUsersToMembers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatGroupUser_Users_UsersId",
                table: "ChatGroupUser");

            migrationBuilder.DropForeignKey(
                name: "FK_P2PChatUser_Users_UsersId",
                table: "P2PChatUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_P2PChatUser",
                table: "P2PChatUser");

            migrationBuilder.DropIndex(
                name: "IX_P2PChatUser_UsersId",
                table: "P2PChatUser");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "P2PChatUser",
                newName: "MembersId");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "ChatGroupUser",
                newName: "MembersId");

            migrationBuilder.RenameIndex(
                name: "IX_ChatGroupUser_UsersId",
                table: "ChatGroupUser",
                newName: "IX_ChatGroupUser_MembersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_P2PChatUser",
                table: "P2PChatUser",
                columns: new[] { "MembersId", "P2PChatsId" });

            migrationBuilder.CreateIndex(
                name: "IX_P2PChatUser_P2PChatsId",
                table: "P2PChatUser",
                column: "P2PChatsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatGroupUser_Users_MembersId",
                table: "ChatGroupUser",
                column: "MembersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_P2PChatUser_Users_MembersId",
                table: "P2PChatUser",
                column: "MembersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatGroupUser_Users_MembersId",
                table: "ChatGroupUser");

            migrationBuilder.DropForeignKey(
                name: "FK_P2PChatUser_Users_MembersId",
                table: "P2PChatUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_P2PChatUser",
                table: "P2PChatUser");

            migrationBuilder.DropIndex(
                name: "IX_P2PChatUser_P2PChatsId",
                table: "P2PChatUser");

            migrationBuilder.RenameColumn(
                name: "MembersId",
                table: "P2PChatUser",
                newName: "UsersId");

            migrationBuilder.RenameColumn(
                name: "MembersId",
                table: "ChatGroupUser",
                newName: "UsersId");

            migrationBuilder.RenameIndex(
                name: "IX_ChatGroupUser_MembersId",
                table: "ChatGroupUser",
                newName: "IX_ChatGroupUser_UsersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_P2PChatUser",
                table: "P2PChatUser",
                columns: new[] { "P2PChatsId", "UsersId" });

            migrationBuilder.CreateIndex(
                name: "IX_P2PChatUser_UsersId",
                table: "P2PChatUser",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatGroupUser_Users_UsersId",
                table: "ChatGroupUser",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_P2PChatUser_Users_UsersId",
                table: "P2PChatUser",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
