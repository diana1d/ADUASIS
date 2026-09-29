using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Aduasis.Api.Datos.Migraciones
{
    /// <inheritdoc />
    public partial class CrearTablaUsuariosRolesTokens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    descripcion = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rol_id = table.Column<int>(type: "integer", nullable: false),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    apellido = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    correo = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    nombre_usuario = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    hash_contrasena = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    ultimo_acceso = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    actualizado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.id);
                    table.ForeignKey(
                        name: "FK_usuarios_roles_rol_id",
                        column: x => x.rol_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tokens_refresco",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    usuario_id = table.Column<int>(type: "integer", nullable: false),
                    token = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    expira_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    usado = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    revocado = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    ip_origen = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tokens_refresco", x => x.id);
                    table.ForeignKey(
                        name: "FK_tokens_refresco_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "activo", "creado_en", "descripcion", "nombre" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Acceso total al sistema", "Administrador" },
                    { 2, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Gestión de activos y seguimiento de tickets", "Funcionario" },
                    { 3, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Acceso limitado a consultas y soporte básico", "Pasante" }
                });

            migrationBuilder.CreateIndex(
                name: "idx_roles_nombre",
                table: "roles",
                column: "nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_tokens_refresco_token",
                table: "tokens_refresco",
                column: "token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_tokens_refresco_usuario_id",
                table: "tokens_refresco",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "idx_usuarios_correo",
                table: "usuarios",
                column: "correo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_usuarios_nombre_usuario",
                table: "usuarios",
                column: "nombre_usuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_usuarios_rol_id",
                table: "usuarios",
                column: "rol_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tokens_refresco");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
