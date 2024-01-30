using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ozds.Data.Migrations
{
    /// <inheritdoc />
    public partial class Representatives : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SchneiderMeasurements_Source_Milliseconds",
                table: "SchneiderMeasurements");

            migrationBuilder.DropIndex(
                name: "IX_SchneiderMeasurements_Source_Timestamp",
                table: "SchneiderMeasurements");

            migrationBuilder.DropIndex(
                name: "IX_AbbMeasurements_Source_Milliseconds",
                table: "AbbMeasurements");

            migrationBuilder.DropIndex(
                name: "IX_AbbMeasurements_Source_Timestamp",
                table: "AbbMeasurements");

            migrationBuilder.DropColumn(
                name: "Milliseconds",
                table: "SchneiderMeasurements");

            migrationBuilder.DropColumn(
                name: "Milliseconds",
                table: "AbbMeasurements");

            migrationBuilder.AlterColumn<float>(
                name: "VoltageL3_V",
                table: "SchneiderMeasurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "VoltageL2_V",
                table: "SchneiderMeasurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "VoltageL1_V",
                table: "SchneiderMeasurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "ReactivePowerTotal_VAR",
                table: "SchneiderMeasurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "ReactiveEnergyImportTotal_VARh",
                table: "SchneiderMeasurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "ReactiveEnergyExportTotal_VARh",
                table: "SchneiderMeasurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "CurrentL3_A",
                table: "SchneiderMeasurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "CurrentL2_A",
                table: "SchneiderMeasurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "CurrentL1_A",
                table: "SchneiderMeasurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "ApparentPowerTotal_VA",
                table: "SchneiderMeasurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "ActivePowerL3_W",
                table: "SchneiderMeasurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "ActivePowerL2_W",
                table: "SchneiderMeasurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "ActivePowerL1_W",
                table: "SchneiderMeasurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "ActiveEnergyImportTotal_Wh",
                table: "SchneiderMeasurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "ActiveEnergyImportL3_Wh",
                table: "SchneiderMeasurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "ActiveEnergyImportL2_Wh",
                table: "SchneiderMeasurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "ActiveEnergyImportL1_Wh",
                table: "SchneiderMeasurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "ActiveEnergyExportTotal_Wh",
                table: "SchneiderMeasurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "VoltageL3_V",
                table: "AbbMeasurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "VoltageL2_V",
                table: "AbbMeasurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "VoltageL1_V",
                table: "AbbMeasurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "ReactivePowerL3_VAR",
                table: "AbbMeasurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "ReactivePowerL2_VAR",
                table: "AbbMeasurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "ReactivePowerL1_VAR",
                table: "AbbMeasurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "ReactivePowerImportL3_VARh",
                table: "AbbMeasurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "ReactivePowerImportL2_VARh",
                table: "AbbMeasurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "ReactivePowerImportL1_VARh",
                table: "AbbMeasurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "ReactivePowerExportL3_VARh",
                table: "AbbMeasurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "ReactivePowerExportL2_VARh",
                table: "AbbMeasurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "ReactivePowerExportL1_VARh",
                table: "AbbMeasurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "ReactiveEnergyImportTotal_VARh",
                table: "AbbMeasurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "ReactiveEnergyExportTotal_VARh",
                table: "AbbMeasurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "CurrentL3_A",
                table: "AbbMeasurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "CurrentL2_A",
                table: "AbbMeasurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "CurrentL1_A",
                table: "AbbMeasurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "ActivePowerL3_W",
                table: "AbbMeasurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "ActivePowerL2_W",
                table: "AbbMeasurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "ActivePowerL1_W",
                table: "AbbMeasurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "ActivePowerImportL3_Wh",
                table: "AbbMeasurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "ActivePowerImportL2_Wh",
                table: "AbbMeasurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "ActivePowerImportL1_Wh",
                table: "AbbMeasurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "ActivePowerExportL3_Wh",
                table: "AbbMeasurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "ActivePowerExportL2_Wh",
                table: "AbbMeasurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "ActivePowerExportL1_Wh",
                table: "AbbMeasurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "ActiveEnergyImportTotal_Wh",
                table: "AbbMeasurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "ActiveEnergyExportTotal_Wh",
                table: "AbbMeasurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.CreateTable(
                name: "Representatives",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    IsOperatorRepresentative = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    SocialSecurityNumber = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    PostalCode = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Representatives", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subnets",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subnets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RepresentativeEntitySubnetEntity",
                columns: table => new
                {
                    RepresentativesId = table.Column<string>(type: "text", nullable: false),
                    SubnetsId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepresentativeEntitySubnetEntity", x => new { x.RepresentativesId, x.SubnetsId });
                    table.ForeignKey(
                        name: "FK_RepresentativeEntitySubnetEntity_Representatives_Representa~",
                        column: x => x.RepresentativesId,
                        principalTable: "Representatives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RepresentativeEntitySubnetEntity_Subnets_SubnetsId",
                        column: x => x.SubnetsId,
                        principalTable: "Subnets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RepresentativeEntityTenantEntity",
                columns: table => new
                {
                    RepresentativesId = table.Column<string>(type: "text", nullable: false),
                    TenantsId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepresentativeEntityTenantEntity", x => new { x.RepresentativesId, x.TenantsId });
                    table.ForeignKey(
                        name: "FK_RepresentativeEntityTenantEntity_Representatives_Representa~",
                        column: x => x.RepresentativesId,
                        principalTable: "Representatives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RepresentativeEntityTenantEntity_Tenants_TenantsId",
                        column: x => x.TenantsId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RepresentativeEntitySubnetEntity_SubnetsId",
                table: "RepresentativeEntitySubnetEntity",
                column: "SubnetsId");

            migrationBuilder.CreateIndex(
                name: "IX_RepresentativeEntityTenantEntity_TenantsId",
                table: "RepresentativeEntityTenantEntity",
                column: "TenantsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RepresentativeEntitySubnetEntity");

            migrationBuilder.DropTable(
                name: "RepresentativeEntityTenantEntity");

            migrationBuilder.DropTable(
                name: "Subnets");

            migrationBuilder.DropTable(
                name: "Representatives");

            migrationBuilder.DropTable(
                name: "Tenants");

            migrationBuilder.AlterColumn<double>(
                name: "VoltageL3_V",
                table: "SchneiderMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "VoltageL2_V",
                table: "SchneiderMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "VoltageL1_V",
                table: "SchneiderMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "ReactivePowerTotal_VAR",
                table: "SchneiderMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "ReactiveEnergyImportTotal_VARh",
                table: "SchneiderMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "ReactiveEnergyExportTotal_VARh",
                table: "SchneiderMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "CurrentL3_A",
                table: "SchneiderMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "CurrentL2_A",
                table: "SchneiderMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "CurrentL1_A",
                table: "SchneiderMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "ApparentPowerTotal_VA",
                table: "SchneiderMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "ActivePowerL3_W",
                table: "SchneiderMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "ActivePowerL2_W",
                table: "SchneiderMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "ActivePowerL1_W",
                table: "SchneiderMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "ActiveEnergyImportTotal_Wh",
                table: "SchneiderMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "ActiveEnergyImportL3_Wh",
                table: "SchneiderMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "ActiveEnergyImportL2_Wh",
                table: "SchneiderMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "ActiveEnergyImportL1_Wh",
                table: "SchneiderMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "ActiveEnergyExportTotal_Wh",
                table: "SchneiderMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<long>(
                name: "Milliseconds",
                table: "SchneiderMeasurements",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<double>(
                name: "VoltageL3_V",
                table: "AbbMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "VoltageL2_V",
                table: "AbbMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "VoltageL1_V",
                table: "AbbMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "ReactivePowerL3_VAR",
                table: "AbbMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "ReactivePowerL2_VAR",
                table: "AbbMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "ReactivePowerL1_VAR",
                table: "AbbMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "ReactivePowerImportL3_VARh",
                table: "AbbMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "ReactivePowerImportL2_VARh",
                table: "AbbMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "ReactivePowerImportL1_VARh",
                table: "AbbMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "ReactivePowerExportL3_VARh",
                table: "AbbMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "ReactivePowerExportL2_VARh",
                table: "AbbMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "ReactivePowerExportL1_VARh",
                table: "AbbMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "ReactiveEnergyImportTotal_VARh",
                table: "AbbMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "ReactiveEnergyExportTotal_VARh",
                table: "AbbMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "CurrentL3_A",
                table: "AbbMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "CurrentL2_A",
                table: "AbbMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "CurrentL1_A",
                table: "AbbMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "ActivePowerL3_W",
                table: "AbbMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "ActivePowerL2_W",
                table: "AbbMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "ActivePowerL1_W",
                table: "AbbMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "ActivePowerImportL3_Wh",
                table: "AbbMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "ActivePowerImportL2_Wh",
                table: "AbbMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "ActivePowerImportL1_Wh",
                table: "AbbMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "ActivePowerExportL3_Wh",
                table: "AbbMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "ActivePowerExportL2_Wh",
                table: "AbbMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "ActivePowerExportL1_Wh",
                table: "AbbMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "ActiveEnergyImportTotal_Wh",
                table: "AbbMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "ActiveEnergyExportTotal_Wh",
                table: "AbbMeasurements",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<long>(
                name: "Milliseconds",
                table: "AbbMeasurements",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_SchneiderMeasurements_Source_Milliseconds",
                table: "SchneiderMeasurements",
                columns: new[] { "Source", "Milliseconds" });

            migrationBuilder.CreateIndex(
                name: "IX_SchneiderMeasurements_Source_Timestamp",
                table: "SchneiderMeasurements",
                columns: new[] { "Source", "Timestamp" });

            migrationBuilder.CreateIndex(
                name: "IX_AbbMeasurements_Source_Milliseconds",
                table: "AbbMeasurements",
                columns: new[] { "Source", "Milliseconds" });

            migrationBuilder.CreateIndex(
                name: "IX_AbbMeasurements_Source_Timestamp",
                table: "AbbMeasurements",
                columns: new[] { "Source", "Timestamp" });
        }
    }
}
