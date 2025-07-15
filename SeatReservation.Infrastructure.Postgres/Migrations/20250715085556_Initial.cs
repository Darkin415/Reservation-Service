using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeatReservation.Infrastructure.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "reservations",
                columns: table => new
                {
                    reservation_id = table.Column<Guid>(type: "uuid", nullable: false),
                    event_id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reservations", x => x.reservation_id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    details = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "venues",
                columns: table => new
                {
                    venue_id = table.Column<Guid>(type: "uuid", nullable: false),
                    SeatsLimit = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    prefix = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_venues", x => x.venue_id);
                });

            migrationBuilder.CreateTable(
                name: "events",
                columns: table => new
                {
                    events_id = table.Column<Guid>(type: "uuid", nullable: false),
                    Venue_id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    EventDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_events", x => x.events_id);
                    table.ForeignKey(
                        name: "FK_events_venues_Venue_id",
                        column: x => x.Venue_id,
                        principalTable: "venues",
                        principalColumn: "venue_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "seats",
                columns: table => new
                {
                    seat_id = table.Column<Guid>(type: "uuid", nullable: false),
                    venue_id = table.Column<Guid>(type: "uuid", nullable: false),
                    RowNumber = table.Column<int>(type: "integer", nullable: false),
                    SeatNumber = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_seats", x => x.seat_id);
                    table.ForeignKey(
                        name: "FK_seats_venues_venue_id",
                        column: x => x.venue_id,
                        principalTable: "venues",
                        principalColumn: "venue_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "event_details",
                columns: table => new
                {
                    event_details_id = table.Column<Guid>(type: "uuid", nullable: false),
                    event_id = table.Column<Guid>(type: "uuid", nullable: false),
                    Capacity = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_event_details", x => x.event_details_id);
                    table.ForeignKey(
                        name: "FK_event_details_events_event_id",
                        column: x => x.event_id,
                        principalTable: "events",
                        principalColumn: "events_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reservation_seats",
                columns: table => new
                {
                    reservation_seats_id = table.Column<Guid>(type: "uuid", nullable: false),
                    reservation_id = table.Column<Guid>(type: "uuid", nullable: false),
                    seat_id = table.Column<Guid>(type: "uuid", nullable: false),
                    ReserveAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reservation_seats", x => x.reservation_seats_id);
                    table.ForeignKey(
                        name: "FK_reservation_seats_reservations_reservation_id",
                        column: x => x.reservation_id,
                        principalTable: "reservations",
                        principalColumn: "reservation_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_reservation_seats_seats_seat_id",
                        column: x => x.seat_id,
                        principalTable: "seats",
                        principalColumn: "seat_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_event_details_event_id",
                table: "event_details",
                column: "event_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_events_Venue_id",
                table: "events",
                column: "Venue_id");

            migrationBuilder.CreateIndex(
                name: "IX_reservation_seats_reservation_id",
                table: "reservation_seats",
                column: "reservation_id");

            migrationBuilder.CreateIndex(
                name: "IX_reservation_seats_seat_id",
                table: "reservation_seats",
                column: "seat_id");

            migrationBuilder.CreateIndex(
                name: "IX_seats_venue_id",
                table: "seats",
                column: "venue_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "event_details");

            migrationBuilder.DropTable(
                name: "reservation_seats");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "events");

            migrationBuilder.DropTable(
                name: "reservations");

            migrationBuilder.DropTable(
                name: "seats");

            migrationBuilder.DropTable(
                name: "venues");
        }
    }
}
