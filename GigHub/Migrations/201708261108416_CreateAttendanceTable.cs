namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class CreateAttendanceTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attendances",
                c => new
                {
                    GigId = c.Int(nullable: false),
                    AttendeeId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.GigId, t.AttendeeId })
                .ForeignKey("dbo.AspNetUsers", t => t.AttendeeId, cascadeDelete: true)
                .ForeignKey("dbo.Gigs", t => t.GigId)
                .Index(t => new { t.GigId, t.AttendeeId }, unique: true, name: "AK_Attendance");

        }

        public override void Down()
        {
            DropForeignKey("dbo.Attendances", "GigId", "dbo.Gigs");
            DropForeignKey("dbo.Attendances", "AttendeeId", "dbo.AspNetUsers");
            DropIndex("dbo.Attendances", "AK_Attendance");
            DropTable("dbo.Attendances");
        }
    }
}
