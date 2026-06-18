using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFinance.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AlterAccountBalanceToDecimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                ALTER TABLE "Account"
                ALTER COLUMN "Balance" TYPE numeric(18,2)
                USING "Balance"::numeric(18,2);
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                ALTER TABLE "Account"
                ALTER COLUMN "Balance" TYPE double precision
                USING "Balance"::double precision;
                """);
        }
    }
}
