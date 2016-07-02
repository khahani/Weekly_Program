namespace WP_Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_TeacherFreeTime : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TeacherFreeTimes",
                c => new
                    {
                        TeacherFreeTimeId = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        AcademicYearId = c.Int(nullable: false),
                        TeacherId = c.Int(nullable: false),
                        DayOfWeek = c.Int(nullable: false),
                        RingNumber = c.Int(nullable: false),
                        FirstHourIsFree = c.Boolean(nullable: false),
                        SecondHourIsFree = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TeacherFreeTimeId)
                .ForeignKey("dbo.AcademicYears", t => t.AcademicYearId, cascadeDelete: true)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.AcademicYearId)
                .Index(t => t.TeacherId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeacherFreeTimes", "UserId", "dbo.Users");
            DropForeignKey("dbo.TeacherFreeTimes", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.TeacherFreeTimes", "AcademicYearId", "dbo.AcademicYears");
            DropIndex("dbo.TeacherFreeTimes", new[] { "TeacherId" });
            DropIndex("dbo.TeacherFreeTimes", new[] { "AcademicYearId" });
            DropIndex("dbo.TeacherFreeTimes", new[] { "UserId" });
            DropTable("dbo.TeacherFreeTimes");
        }
    }
}
