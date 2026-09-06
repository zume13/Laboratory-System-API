using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class VerifyNoSchemaChangeAfterConfigCleanup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LabResults");

            migrationBuilder.DropTable(
                name: "LabRequests");

            migrationBuilder.DropColumn(
                name: "FulfillingLabRequestId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "TestCategoryId",
                table: "Appointments");

            migrationBuilder.CreateTable(
                name: "AppointmentTest",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AppointmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    TestCategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    isApproved = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentTest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppointmentTest_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LaboratoryRequestOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PatientId = table.Column<Guid>(type: "uuid", nullable: false),
                    AppointmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaboratoryRequestOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LaboratoryRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LabOrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    PatientId = table.Column<Guid>(type: "uuid", nullable: false),
                    TestCategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaboratoryRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LaboratoryRequests_LaboratoryRequestOrders_LabOrderId",
                        column: x => x.LabOrderId,
                        principalTable: "LaboratoryRequestOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LaboratoryResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LaboratoryRequestId = table.Column<Guid>(type: "uuid", nullable: false),
                    UploadedByStaffId = table.Column<Guid>(type: "uuid", nullable: false),
                    PdfPath = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    SampleId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsVoided = table.Column<bool>(type: "boolean", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaboratoryResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LaboratoryResults_LaboratoryRequests_LaboratoryRequestId",
                        column: x => x.LaboratoryRequestId,
                        principalTable: "LaboratoryRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentTest_AppointmentId",
                table: "AppointmentTest",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_LaboratoryRequests_LabOrderId",
                table: "LaboratoryRequests",
                column: "LabOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_LaboratoryResults_LaboratoryRequestId",
                table: "LaboratoryResults",
                column: "LaboratoryRequestId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentTest");

            migrationBuilder.DropTable(
                name: "LaboratoryResults");

            migrationBuilder.DropTable(
                name: "LaboratoryRequests");

            migrationBuilder.DropTable(
                name: "LaboratoryRequestOrders");

            migrationBuilder.AddColumn<Guid>(
                name: "FulfillingLabRequestId",
                table: "Appointments",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TestCategoryId",
                table: "Appointments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "LabRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AppointmentId = table.Column<Guid>(type: "uuid", nullable: true),
                    ClinicalDetails = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PatientId = table.Column<Guid>(type: "uuid", nullable: true),
                    PhysicalPatientId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    TestCategoryId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LabResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsVoided = table.Column<bool>(type: "boolean", nullable: false),
                    LaboratoryRequestId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SampleId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UploadedByStaffId = table.Column<Guid>(type: "uuid", nullable: false),
                    PdfFilePath = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LabResults_LabRequests_LaboratoryRequestId",
                        column: x => x.LaboratoryRequestId,
                        principalTable: "LabRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LabRequests_PhysicalPatientId",
                table: "LabRequests",
                column: "PhysicalPatientId");

            migrationBuilder.CreateIndex(
                name: "IX_LabResults_LaboratoryRequestId",
                table: "LabResults",
                column: "LaboratoryRequestId");
        }
    }
}
