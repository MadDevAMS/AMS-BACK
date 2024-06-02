using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AMS.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_Busnise_Entities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupUsers_Groups_Groupid",
                table: "GroupUsers");

            migrationBuilder.RenameColumn(
                name: "Groupid",
                table: "GroupUsers",
                newName: "GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupUsers_Groupid",
                table: "GroupUsers",
                newName: "IX_GroupUsers_GroupId");

            migrationBuilder.RenameColumn(
                name: "GroupName",
                table: "Groups",
                newName: "GroupId");

            migrationBuilder.RenameColumn(
                name: "RolePermissionId",
                table: "GroupPermission",
                newName: "GroupPermissionId");

            migrationBuilder.AddColumn<long>(
                name: "EntidadId",
                table: "Groups",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdEntidad",
                table: "Groups",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Area",
                columns: table => new
                {
                    AreaId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IdParent = table.Column<long>(type: "bigint", nullable: false),
                    IdEntidad = table.Column<long>(type: "bigint", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    AuditCreateUser = table.Column<int>(type: "int", nullable: false),
                    AuditCreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuditUpdateUser = table.Column<int>(type: "int", nullable: true),
                    AuditUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AuditDeleteUser = table.Column<int>(type: "int", nullable: true),
                    AuditDeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Area", x => x.AreaId);
                    table.ForeignKey(
                        name: "FK_Area_Entidad_IdEntidad",
                        column: x => x.IdEntidad,
                        principalTable: "Entidad",
                        principalColumn: "EntidadId");
                });

            migrationBuilder.CreateTable(
                name: "Maquina",
                columns: table => new
                {
                    MaquinaId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdArea = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TipoMaquina = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    AuditCreateUser = table.Column<int>(type: "int", nullable: false),
                    AuditCreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuditUpdateUser = table.Column<int>(type: "int", nullable: true),
                    AuditUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AuditDeleteUser = table.Column<int>(type: "int", nullable: true),
                    AuditDeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maquina", x => x.MaquinaId);
                    table.ForeignKey(
                        name: "FK_Maquina_Area_IdArea",
                        column: x => x.IdArea,
                        principalTable: "Area",
                        principalColumn: "AreaId");
                });

            migrationBuilder.CreateTable(
                name: "Componente",
                columns: table => new
                {
                    ComponenteId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdMaquina = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Potencia = table.Column<int>(type: "int", nullable: false),
                    Velocidad = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    AuditCreateUser = table.Column<int>(type: "int", nullable: false),
                    AuditCreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuditUpdateUser = table.Column<int>(type: "int", nullable: true),
                    AuditUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AuditDeleteUser = table.Column<int>(type: "int", nullable: true),
                    AuditDeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Componente", x => x.ComponenteId);
                    table.ForeignKey(
                        name: "FK_Componente_Maquina_IdMaquina",
                        column: x => x.IdMaquina,
                        principalTable: "Maquina",
                        principalColumn: "MaquinaId");
                });

            migrationBuilder.CreateTable(
                name: "PuntoMonitoreo",
                columns: table => new
                {
                    PuntoMonitoreoId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdComponente = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DireccionMedicion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    AnguloDireccion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DatosMedicion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    AuditCreateUser = table.Column<int>(type: "int", nullable: false),
                    AuditCreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuditUpdateUser = table.Column<int>(type: "int", nullable: true),
                    AuditUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AuditDeleteUser = table.Column<int>(type: "int", nullable: true),
                    AuditDeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PuntoMonitoreo", x => x.PuntoMonitoreoId);
                    table.ForeignKey(
                        name: "FK_PuntoMonitoreo_Componente_IdComponente",
                        column: x => x.IdComponente,
                        principalTable: "Componente",
                        principalColumn: "ComponenteId");
                });

            migrationBuilder.CreateTable(
                name: "Metrica",
                columns: table => new
                {
                    MetricaId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPuntoMonitoreo = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    AuditCreateUser = table.Column<int>(type: "int", nullable: false),
                    AuditCreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuditUpdateUser = table.Column<int>(type: "int", nullable: true),
                    AuditUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AuditDeleteUser = table.Column<int>(type: "int", nullable: true),
                    AuditDeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metrica", x => x.MetricaId);
                    table.ForeignKey(
                        name: "FK_Metrica_PuntoMonitoreo_IdPuntoMonitoreo",
                        column: x => x.IdPuntoMonitoreo,
                        principalTable: "PuntoMonitoreo",
                        principalColumn: "PuntoMonitoreoId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Groups_EntidadId",
                table: "Groups",
                column: "EntidadId");

            migrationBuilder.CreateIndex(
                name: "IX_Area_IdEntidad",
                table: "Area",
                column: "IdEntidad");

            migrationBuilder.CreateIndex(
                name: "IX_Componente_IdMaquina",
                table: "Componente",
                column: "IdMaquina");

            migrationBuilder.CreateIndex(
                name: "IX_Maquina_IdArea",
                table: "Maquina",
                column: "IdArea");

            migrationBuilder.CreateIndex(
                name: "IX_Metrica_IdPuntoMonitoreo",
                table: "Metrica",
                column: "IdPuntoMonitoreo");

            migrationBuilder.CreateIndex(
                name: "IX_PuntoMonitoreo_IdComponente",
                table: "PuntoMonitoreo",
                column: "IdComponente");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Entidad_EntidadId",
                table: "Groups",
                column: "EntidadId",
                principalTable: "Entidad",
                principalColumn: "EntidadId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUsers_Groups_GroupId",
                table: "GroupUsers",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Entidad_EntidadId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupUsers_Groups_GroupId",
                table: "GroupUsers");

            migrationBuilder.DropTable(
                name: "Metrica");

            migrationBuilder.DropTable(
                name: "PuntoMonitoreo");

            migrationBuilder.DropTable(
                name: "Componente");

            migrationBuilder.DropTable(
                name: "Maquina");

            migrationBuilder.DropTable(
                name: "Area");

            migrationBuilder.DropIndex(
                name: "IX_Groups_EntidadId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "EntidadId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "IdEntidad",
                table: "Groups");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "GroupUsers",
                newName: "Groupid");

            migrationBuilder.RenameIndex(
                name: "IX_GroupUsers_GroupId",
                table: "GroupUsers",
                newName: "IX_GroupUsers_Groupid");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "Groups",
                newName: "GroupName");

            migrationBuilder.RenameColumn(
                name: "GroupPermissionId",
                table: "GroupPermission",
                newName: "RolePermissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUsers_Groups_Groupid",
                table: "GroupUsers",
                column: "Groupid",
                principalTable: "Groups",
                principalColumn: "GroupName");
        }
    }
}
