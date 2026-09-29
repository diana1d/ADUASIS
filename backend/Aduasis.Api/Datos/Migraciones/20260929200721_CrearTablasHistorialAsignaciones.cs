using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Aduasis.Api.Datos.Migraciones
{
    /// <inheritdoc />
    public partial class CrearTablasHistorialAsignaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "asignaciones",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    activo_id = table.Column<int>(type: "integer", nullable: false),
                    usuario_id = table.Column<int>(type: "integer", nullable: true),
                    fecha_inicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    fecha_fin = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    observaciones = table.Column<string>(type: "text", nullable: true),
                    registrado_por_id = table.Column<int>(type: "integer", nullable: true),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    ActivoId1 = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_asignaciones", x => x.id);
                    table.ForeignKey(
                        name: "FK_asignaciones_activos_ActivoId1",
                        column: x => x.ActivoId1,
                        principalTable: "activos",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_asignaciones_activos_activo_id",
                        column: x => x.activo_id,
                        principalTable: "activos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_asignaciones_usuarios_registrado_por_id",
                        column: x => x.registrado_por_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_asignaciones_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "historial_activos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    activo_id = table.Column<int>(type: "integer", nullable: false),
                    usuario_id = table.Column<int>(type: "integer", nullable: true),
                    tipo_evento = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: false),
                    valor_anterior = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    valor_nuevo = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    es_nota_manual = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    ActivoId1 = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_historial_activos", x => x.id);
                    table.ForeignKey(
                        name: "FK_historial_activos_activos_ActivoId1",
                        column: x => x.ActivoId1,
                        principalTable: "activos",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_historial_activos_activos_activo_id",
                        column: x => x.activo_id,
                        principalTable: "activos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_historial_activos_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "idx_asignaciones_activo_activa",
                table: "asignaciones",
                columns: new[] { "activo_id", "fecha_fin" });

            migrationBuilder.CreateIndex(
                name: "idx_asignaciones_activo_id",
                table: "asignaciones",
                column: "activo_id");

            migrationBuilder.CreateIndex(
                name: "IX_asignaciones_ActivoId1",
                table: "asignaciones",
                column: "ActivoId1");

            migrationBuilder.CreateIndex(
                name: "IX_asignaciones_registrado_por_id",
                table: "asignaciones",
                column: "registrado_por_id");

            migrationBuilder.CreateIndex(
                name: "IX_asignaciones_usuario_id",
                table: "asignaciones",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "idx_historial_activos_activo_id",
                table: "historial_activos",
                column: "activo_id");

            migrationBuilder.CreateIndex(
                name: "idx_historial_activos_creado_en",
                table: "historial_activos",
                column: "creado_en");

            migrationBuilder.CreateIndex(
                name: "IX_historial_activos_ActivoId1",
                table: "historial_activos",
                column: "ActivoId1");

            migrationBuilder.CreateIndex(
                name: "IX_historial_activos_usuario_id",
                table: "historial_activos",
                column: "usuario_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "asignaciones");

            migrationBuilder.DropTable(
                name: "historial_activos");
        }
    }
}
