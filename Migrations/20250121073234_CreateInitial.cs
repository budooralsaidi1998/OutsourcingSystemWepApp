using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OutsourcingSystemWepApp.Migrations
{
    /// <inheritdoc />
    public partial class CreateInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    SkillID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.SkillID);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TeamCapacity = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    HourlyRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Rating = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CompletedProjects = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UID);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    ClientID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UID = table.Column<int>(type: "int", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Industry = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CommitmentRating = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.ClientID);
                    table.ForeignKey(
                        name: "FK_Client_Users_UID",
                        column: x => x.UID,
                        principalTable: "Users",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Developer",
                columns: table => new
                {
                    DeveloperID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    DeveloperName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearsOfExperience = table.Column<int>(type: "int", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    HourlyRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AvailabilityStatus = table.Column<bool>(type: "bit", nullable: false),
                    CareerSummary = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CompletedProjects = table.Column<int>(type: "int", nullable: false),
                    CanBePartOfTeam = table.Column<bool>(type: "bit", nullable: false),
                    DocumentLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CommitmentRating = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Developer", x => x.DeveloperID);
                    table.ForeignKey(
                        name: "FK_Developer_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientReviewTeam",
                columns: table => new
                {
                    ReviewID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientID = table.Column<int>(type: "int", nullable: false),
                    TeamID = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientReviewTeam", x => x.ReviewID);
                    table.ForeignKey(
                        name: "FK_ClientReviewTeam_Client_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Client",
                        principalColumn: "ClientID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientReviewTeam_Teams_TeamID",
                        column: x => x.TeamID,
                        principalTable: "Teams",
                        principalColumn: "TeamID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientRequestDeveloper",
                columns: table => new
                {
                    RequestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientID = table.Column<int>(type: "int", nullable: false),
                    DeveloperID = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientRequestDeveloper", x => x.RequestID);
                    table.ForeignKey(
                        name: "FK_ClientRequestDeveloper_Client_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Client",
                        principalColumn: "ClientID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientRequestDeveloper_Developer_DeveloperID",
                        column: x => x.DeveloperID,
                        principalTable: "Developer",
                        principalColumn: "DeveloperID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ClientRequestTeam",
                columns: table => new
                {
                    RequestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientID = table.Column<int>(type: "int", nullable: false),
                    TID = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DeveloperID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientRequestTeam", x => x.RequestID);
                    table.ForeignKey(
                        name: "FK_ClientRequestTeam_Client_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Client",
                        principalColumn: "ClientID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientRequestTeam_Developer_DeveloperID",
                        column: x => x.DeveloperID,
                        principalTable: "Developer",
                        principalColumn: "DeveloperID");
                    table.ForeignKey(
                        name: "FK_ClientRequestTeam_Teams_TID",
                        column: x => x.TID,
                        principalTable: "Teams",
                        principalColumn: "TeamID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientReviewDeveloper",
                columns: table => new
                {
                    ReviewID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeveloperID = table.Column<int>(type: "int", nullable: false),
                    ClientID = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientReviewDeveloper", x => x.ReviewID);
                    table.ForeignKey(
                        name: "FK_ClientReviewDeveloper_Client_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Client",
                        principalColumn: "ClientID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientReviewDeveloper_Developer_DeveloperID",
                        column: x => x.DeveloperID,
                        principalTable: "Developer",
                        principalColumn: "DeveloperID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "DeveloperSkill",
                columns: table => new
                {
                    DeveloperID = table.Column<int>(type: "int", nullable: false),
                    SkillID = table.Column<int>(type: "int", nullable: false),
                    Proficiency = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeveloperSkill", x => new { x.DeveloperID, x.SkillID });
                    table.ForeignKey(
                        name: "FK_DeveloperSkill_Developer_DeveloperID",
                        column: x => x.DeveloperID,
                        principalTable: "Developer",
                        principalColumn: "DeveloperID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeveloperSkill_Skills_SkillID",
                        column: x => x.SkillID,
                        principalTable: "Skills",
                        principalColumn: "SkillID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeedbackOnClient",
                columns: table => new
                {
                    FeedbackID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientID = table.Column<int>(type: "int", nullable: false),
                    DeveloperID = table.Column<int>(type: "int", nullable: true),
                    TeamID = table.Column<int>(type: "int", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedbackOnClient", x => x.FeedbackID);
                    table.ForeignKey(
                        name: "FK_FeedbackOnClient_Client_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Client",
                        principalColumn: "ClientID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeedbackOnClient_Developer_DeveloperID",
                        column: x => x.DeveloperID,
                        principalTable: "Developer",
                        principalColumn: "DeveloperID");
                    table.ForeignKey(
                        name: "FK_FeedbackOnClient_Teams_TeamID",
                        column: x => x.TeamID,
                        principalTable: "Teams",
                        principalColumn: "TeamID");
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    ProjectID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientID = table.Column<int>(type: "int", nullable: false),
                    TeamID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    StartAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeveloperID = table.Column<int>(type: "int", nullable: true),
                    DailyHoursNeeded = table.Column<int>(type: "int", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.ProjectID);
                    table.ForeignKey(
                        name: "FK_Project_Client_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Client",
                        principalColumn: "ClientID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Project_Developer_DeveloperID",
                        column: x => x.DeveloperID,
                        principalTable: "Developer",
                        principalColumn: "DeveloperID");
                    table.ForeignKey(
                        name: "FK_Project_Teams_TeamID",
                        column: x => x.TeamID,
                        principalTable: "Teams",
                        principalColumn: "TeamID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamMember",
                columns: table => new
                {
                    TeamID = table.Column<int>(type: "int", nullable: false),
                    DeveloperID = table.Column<int>(type: "int", nullable: false),
                    JoinedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamMember", x => new { x.TeamID, x.DeveloperID });
                    table.ForeignKey(
                        name: "FK_TeamMember_Developer_DeveloperID",
                        column: x => x.DeveloperID,
                        principalTable: "Developer",
                        principalColumn: "DeveloperID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamMember_Teams_TeamID",
                        column: x => x.TeamID,
                        principalTable: "Teams",
                        principalColumn: "TeamID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Client_UID",
                table: "Client",
                column: "UID");

            migrationBuilder.CreateIndex(
                name: "IX_ClientRequestDeveloper_ClientID",
                table: "ClientRequestDeveloper",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_ClientRequestDeveloper_DeveloperID",
                table: "ClientRequestDeveloper",
                column: "DeveloperID");

            migrationBuilder.CreateIndex(
                name: "IX_ClientRequestTeam_ClientID",
                table: "ClientRequestTeam",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_ClientRequestTeam_DeveloperID",
                table: "ClientRequestTeam",
                column: "DeveloperID");

            migrationBuilder.CreateIndex(
                name: "IX_ClientRequestTeam_TID",
                table: "ClientRequestTeam",
                column: "TID");

            migrationBuilder.CreateIndex(
                name: "IX_ClientReviewDeveloper_ClientID",
                table: "ClientReviewDeveloper",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_ClientReviewDeveloper_DeveloperID",
                table: "ClientReviewDeveloper",
                column: "DeveloperID");

            migrationBuilder.CreateIndex(
                name: "IX_ClientReviewTeam_ClientID",
                table: "ClientReviewTeam",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_ClientReviewTeam_TeamID",
                table: "ClientReviewTeam",
                column: "TeamID");

            migrationBuilder.CreateIndex(
                name: "IX_Developer_UserID",
                table: "Developer",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_DeveloperSkill_SkillID",
                table: "DeveloperSkill",
                column: "SkillID");

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackOnClient_ClientID",
                table: "FeedbackOnClient",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackOnClient_DeveloperID",
                table: "FeedbackOnClient",
                column: "DeveloperID");

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackOnClient_TeamID",
                table: "FeedbackOnClient",
                column: "TeamID");

            migrationBuilder.CreateIndex(
                name: "IX_Project_ClientID",
                table: "Project",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_Project_DeveloperID",
                table: "Project",
                column: "DeveloperID");

            migrationBuilder.CreateIndex(
                name: "IX_Project_TeamID",
                table: "Project",
                column: "TeamID");

            migrationBuilder.CreateIndex(
                name: "IX_TeamMember_DeveloperID",
                table: "TeamMember",
                column: "DeveloperID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientRequestDeveloper");

            migrationBuilder.DropTable(
                name: "ClientRequestTeam");

            migrationBuilder.DropTable(
                name: "ClientReviewDeveloper");

            migrationBuilder.DropTable(
                name: "ClientReviewTeam");

            migrationBuilder.DropTable(
                name: "DeveloperSkill");

            migrationBuilder.DropTable(
                name: "FeedbackOnClient");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "TeamMember");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Developer");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
