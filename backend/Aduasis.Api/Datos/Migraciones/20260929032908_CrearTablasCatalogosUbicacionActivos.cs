using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Aduasis.Api.Datos.Migraciones
{
    /// <inheritdoc />
    public partial class CrearTablasCatalogosUbicacionActivos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "edificios",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    descripcion = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_edificios", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "estados_activo",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    descripcion = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estados_activo", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "marcas",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_marcas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tipos_activo",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    descripcion = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipos_activo", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pisos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    edificio_id = table.Column<int>(type: "integer", nullable: false),
                    nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    orden = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pisos", x => x.id);
                    table.ForeignKey(
                        name: "FK_pisos_edificios_edificio_id",
                        column: x => x.edificio_id,
                        principalTable: "edificios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "modelos_activo",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    marca_id = table.Column<int>(type: "integer", nullable: false),
                    nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_modelos_activo", x => x.id);
                    table.ForeignKey(
                        name: "FK_modelos_activo_marcas_marca_id",
                        column: x => x.marca_id,
                        principalTable: "marcas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "areas",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    piso_id = table.Column<int>(type: "integer", nullable: false),
                    nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    descripcion = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_areas", x => x.id);
                    table.ForeignKey(
                        name: "FK_areas_pisos_piso_id",
                        column: x => x.piso_id,
                        principalTable: "pisos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "espacios",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    piso_id = table.Column<int>(type: "integer", nullable: false),
                    nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    descripcion = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_espacios", x => x.id);
                    table.ForeignKey(
                        name: "FK_espacios_pisos_piso_id",
                        column: x => x.piso_id,
                        principalTable: "pisos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "activos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    codigo_activo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    codigo_qr = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    nombre_descriptivo = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    especificaciones = table.Column<string>(type: "text", nullable: true),
                    color = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    tipo_activo_id = table.Column<int>(type: "integer", nullable: false),
                    marca_id = table.Column<int>(type: "integer", nullable: false),
                    modelo_activo_id = table.Column<int>(type: "integer", nullable: true),
                    estado_activo_id = table.Column<int>(type: "integer", nullable: false),
                    espacio_id = table.Column<int>(type: "integer", nullable: true),
                    area_id = table.Column<int>(type: "integer", nullable: true),
                    usuario_asignado_id = table.Column<int>(type: "integer", nullable: true),
                    direccion_ip = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    fecha_adquisicion = table.Column<DateOnly>(type: "date", nullable: true),
                    observaciones = table.Column<string>(type: "text", nullable: true),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    actualizado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    creado_por_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_activos", x => x.id);
                    table.ForeignKey(
                        name: "FK_activos_areas_area_id",
                        column: x => x.area_id,
                        principalTable: "areas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_activos_espacios_espacio_id",
                        column: x => x.espacio_id,
                        principalTable: "espacios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_activos_estados_activo_estado_activo_id",
                        column: x => x.estado_activo_id,
                        principalTable: "estados_activo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_activos_marcas_marca_id",
                        column: x => x.marca_id,
                        principalTable: "marcas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_activos_modelos_activo_modelo_activo_id",
                        column: x => x.modelo_activo_id,
                        principalTable: "modelos_activo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_activos_tipos_activo_tipo_activo_id",
                        column: x => x.tipo_activo_id,
                        principalTable: "tipos_activo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_activos_usuarios_creado_por_id",
                        column: x => x.creado_por_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_activos_usuarios_usuario_asignado_id",
                        column: x => x.usuario_asignado_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                table: "edificios",
                columns: new[] { "id", "activo", "creado_en", "descripcion", "nombre" },
                values: new object[] { 1, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Edificio principal", "Aduana Nacional - Gerencia Regional La Paz" });

            migrationBuilder.InsertData(
                table: "estados_activo",
                columns: new[] { "id", "activo", "creado_en", "descripcion", "nombre" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "El activo funciona correctamente", "Operativo" },
                    { 2, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "El activo está siendo reparado", "En reparación" },
                    { 3, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "El activo está almacenado sin asignar", "En bodega" },
                    { 4, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "El activo fue retirado del servicio", "Dado de baja" },
                    { 5, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "El activo está prestado temporalmente", "En préstamo" }
                });

            migrationBuilder.InsertData(
                table: "marcas",
                columns: new[] { "id", "activo", "creado_en", "nombre" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Dell" },
                    { 2, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "HP" },
                    { 3, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Cisco" },
                    { 4, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Lenovo" },
                    { 5, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Epson" },
                    { 6, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Samsung" },
                    { 7, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Acer" },
                    { 8, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Asus" },
                    { 9, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "APC" },
                    { 10, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Huawei" },
                    { 11, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "TP-Link" },
                    { 12, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Otra" }
                });

            migrationBuilder.InsertData(
                table: "tipos_activo",
                columns: new[] { "id", "activo", "creado_en", "descripcion", "nombre" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Computadoras de escritorio, laptops y similares", "Equipo de computación" },
                    { 2, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Impresoras láser, de inyección y multifuncionales", "Impresora" },
                    { 3, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Switches de red administrables y no administrables", "Switch" },
                    { 4, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Routers y equipos de enrutamiento", "Router" },
                    { 5, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Sistemas de alimentación ininterrumpida", "UPS" },
                    { 6, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Monitores y pantallas", "Monitor" },
                    { 7, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Teléfonos y dispositivos de telefonía IP", "Teléfono IP" },
                    { 8, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Escáneres documentales y de código de barras", "Escáner" },
                    { 9, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Servidores físicos y de rack", "Servidor" },
                    { 10, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Otros equipos tecnológicos", "Otro" }
                });

            migrationBuilder.InsertData(
                table: "modelos_activo",
                columns: new[] { "id", "activo", "creado_en", "marca_id", "nombre" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "OptiPlex 7070" },
                    { 2, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "OptiPlex 3080" },
                    { 3, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Latitude 5420" },
                    { 4, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "ProDesk 400 G7" },
                    { 5, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "LaserJet Enterprise" },
                    { 6, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "EliteBook 840" }
                });

            migrationBuilder.InsertData(
                table: "pisos",
                columns: new[] { "id", "activo", "creado_en", "edificio_id", "nombre" },
                values: new object[] { 1, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Planta Baja" });

            migrationBuilder.InsertData(
                table: "pisos",
                columns: new[] { "id", "activo", "creado_en", "edificio_id", "nombre", "orden" },
                values: new object[,]
                {
                    { 2, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Piso 1", 1 },
                    { 3, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Exterior", 99 }
                });

            migrationBuilder.CreateIndex(
                name: "idx_activos_area",
                table: "activos",
                column: "area_id");

            migrationBuilder.CreateIndex(
                name: "idx_activos_codigo_activo",
                table: "activos",
                column: "codigo_activo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_activos_codigo_qr",
                table: "activos",
                column: "codigo_qr",
                unique: true,
                filter: "codigo_qr IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "idx_activos_espacio",
                table: "activos",
                column: "espacio_id");

            migrationBuilder.CreateIndex(
                name: "idx_activos_estado",
                table: "activos",
                column: "estado_activo_id");

            migrationBuilder.CreateIndex(
                name: "IX_activos_creado_por_id",
                table: "activos",
                column: "creado_por_id");

            migrationBuilder.CreateIndex(
                name: "IX_activos_marca_id",
                table: "activos",
                column: "marca_id");

            migrationBuilder.CreateIndex(
                name: "IX_activos_modelo_activo_id",
                table: "activos",
                column: "modelo_activo_id");

            migrationBuilder.CreateIndex(
                name: "IX_activos_tipo_activo_id",
                table: "activos",
                column: "tipo_activo_id");

            migrationBuilder.CreateIndex(
                name: "IX_activos_usuario_asignado_id",
                table: "activos",
                column: "usuario_asignado_id");

            migrationBuilder.CreateIndex(
                name: "idx_areas_piso_nombre",
                table: "areas",
                columns: new[] { "piso_id", "nombre" });

            migrationBuilder.CreateIndex(
                name: "idx_edificios_nombre",
                table: "edificios",
                column: "nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_espacios_piso_nombre",
                table: "espacios",
                columns: new[] { "piso_id", "nombre" });

            migrationBuilder.CreateIndex(
                name: "idx_estados_activo_nombre",
                table: "estados_activo",
                column: "nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_marcas_nombre",
                table: "marcas",
                column: "nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_modelos_activo_marca_nombre",
                table: "modelos_activo",
                columns: new[] { "marca_id", "nombre" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_pisos_edificio_nombre",
                table: "pisos",
                columns: new[] { "edificio_id", "nombre" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_tipos_activo_nombre",
                table: "tipos_activo",
                column: "nombre",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "activos");

            migrationBuilder.DropTable(
                name: "areas");

            migrationBuilder.DropTable(
                name: "espacios");

            migrationBuilder.DropTable(
                name: "estados_activo");

            migrationBuilder.DropTable(
                name: "modelos_activo");

            migrationBuilder.DropTable(
                name: "tipos_activo");

            migrationBuilder.DropTable(
                name: "pisos");

            migrationBuilder.DropTable(
                name: "marcas");

            migrationBuilder.DropTable(
                name: "edificios");
        }
    }
}
