using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingApi_with_ReactFrontend.Server.Migrations
{
    /// <inheritdoc />
    public partial class fixing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Convert existing string values to ints
            migrationBuilder.Sql(@"
        UPDATE Transactions
        SET TransactionType =
            CASE TransactionType
                WHEN 'Deposit' THEN 0
                WHEN 'Withdraw' THEN 1
                WHEN 'Transfer' THEN 2
            END;");


            // Change column type to int
            migrationBuilder.AlterColumn<int>(
                name: "TransactionType",
                table: "Transactions",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Rollback: convert ints back to strings
            migrationBuilder.Sql(@"
        UPDATE Transactions
        SET TransactionType =
            CASE TransactionType
                WHEN 0 THEN 'Deposit'
                WHEN 1 THEN 'Withdraw'
                WHEN 2 THEN 'Transfer'
            END;
    ");

            migrationBuilder.AlterColumn<string>(
                name: "TransactionType",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int"
            );
        }
    }
}
