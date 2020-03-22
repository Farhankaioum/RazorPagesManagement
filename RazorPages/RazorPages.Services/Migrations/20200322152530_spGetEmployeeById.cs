using Microsoft.EntityFrameworkCore.Migrations;

namespace RazorPages.Services.Migrations
{
	public partial class spGetEmployeeById : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			string procedure = @"  create proc spGetEmployeeById
								  @Id int
								  as
								  begin
									select * from Employees 
									where Id = @Id
								  end";
			migrationBuilder.Sql(procedure);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			string procedure = @"drop proc spGetEmployeeById ";
			migrationBuilder.Sql(procedure);
		}
	}
}
