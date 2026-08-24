using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivityLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    Action = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Target = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Severity = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdministratorProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Permissions = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdministratorProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PatientId = table.Column<Guid>(type: "uuid", nullable: false),
                    AppointmentSlotId = table.Column<Guid>(type: "uuid", nullable: false),
                    TestCategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    FulfillingLabRequestId = table.Column<Guid>(type: "uuid", nullable: true),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    BookingChannel = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConfirmedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppointmentSlots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    TestCategoryId = table.Column<Guid>(type: "uuid", nullable: true),
                    Capacity = table.Column<int>(type: "integer", nullable: false),
                    BookedCount = table.Column<int>(type: "integer", nullable: false),
                    ConfiguredByStaffId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentSlots", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClinicalStaffProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Role = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicalStaffProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LabRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PatientId = table.Column<Guid>(type: "uuid", nullable: true),
                    PhysicalPatientId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    TestCategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    AppointmentId = table.Column<Guid>(type: "uuid", nullable: true),
                    ClinicalDetails = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PatientId = table.Column<Guid>(type: "uuid", nullable: false),
                    LabResultId = table.Column<Guid>(type: "uuid", nullable: true),
                    Channel = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Message = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    SentAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Sex = table.Column<string>(type: "text", nullable: false),
                    PhysicalPatientId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ConsentAccepted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TokenHash = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RevokedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ReplacedByTokenId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SlotCapacityConfigs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TestCategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    MaxDailyBookings = table.Column<int>(type: "integer", nullable: false),
                    MaxPerSlot = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlotCapacityConfigs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SmsGatewayStatuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreditsRemaining = table.Column<int>(type: "integer", nullable: false),
                    ErrorCount24h = table.Column<int>(type: "integer", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmsGatewayStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StorageStatuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StorageType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    UsedGb = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    CapacityGb = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    LastCheckedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemConfigs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Key = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemConfigs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    HashedPassword = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    LastLoginAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppointmentReminders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AppointmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Channel = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ScheduledSendTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentReminders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppointmentReminders_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LabResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LaboratoryRequestId = table.Column<Guid>(type: "uuid", nullable: false),
                    UploadedByStaffId = table.Column<Guid>(type: "uuid", nullable: false),
                    PdfFilePath = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    SampleId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsVoided = table.Column<bool>(type: "boolean", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
                name: "IX_ActivityLogs_Timestamp",
                table: "ActivityLogs",
                column: "Timestamp");

            migrationBuilder.CreateIndex(
                name: "IX_AdministratorProfiles_UserId",
                table: "AdministratorProfiles",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentReminders_AppointmentId",
                table: "AppointmentReminders",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentSlots_Date_TestCategoryId",
                table: "AppointmentSlots",
                columns: new[] { "Date", "TestCategoryId" });

            migrationBuilder.CreateIndex(
                name: "IX_ClinicalStaffProfiles_UserId",
                table: "ClinicalStaffProfiles",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LabRequests_PhysicalPatientId",
                table: "LabRequests",
                column: "PhysicalPatientId");

            migrationBuilder.CreateIndex(
                name: "IX_LabResults_LaboratoryRequestId",
                table: "LabResults",
                column: "LaboratoryRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientProfiles_PhysicalPatientId",
                table: "PatientProfiles",
                column: "PhysicalPatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientProfiles_UserId",
                table: "PatientProfiles",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SlotCapacityConfigs_TestCategoryId",
                table: "SlotCapacityConfigs",
                column: "TestCategoryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SystemConfigs_Key",
                table: "SystemConfigs",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityLogs");

            migrationBuilder.DropTable(
                name: "AdministratorProfiles");

            migrationBuilder.DropTable(
                name: "AppointmentReminders");

            migrationBuilder.DropTable(
                name: "AppointmentSlots");

            migrationBuilder.DropTable(
                name: "ClinicalStaffProfiles");

            migrationBuilder.DropTable(
                name: "LabResults");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "PatientProfiles");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "SlotCapacityConfigs");

            migrationBuilder.DropTable(
                name: "SmsGatewayStatuses");

            migrationBuilder.DropTable(
                name: "StorageStatuses");

            migrationBuilder.DropTable(
                name: "SystemConfigs");

            migrationBuilder.DropTable(
                name: "TestCategories");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "LabRequests");
        }
    }
}
