namespace WP_Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_WeeklyProgram : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WeeklyPrograms",
                c => new
                    {
                        WeeklyProgramId = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        AcademicYearId = c.Int(nullable: false),
                        ClassId = c.Int(nullable: false),
                        DayOfWeek = c.Int(nullable: false),
                        RingNumber = c.Int(nullable: false),
                        HalfRingNumber = c.Int(nullable: false),
                        TeacherId = c.Int(nullable: false),
                        LessonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.WeeklyProgramId)
                .ForeignKey("dbo.AcademicYears", t => t.AcademicYearId, cascadeDelete: true)
                .ForeignKey("dbo.Classes", t => t.ClassId, cascadeDelete: false)
                .ForeignKey("dbo.Lessons", t => t.LessonId, cascadeDelete: true)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.AcademicYearId)
                .Index(t => t.ClassId)
                .Index(t => t.TeacherId)
                .Index(t => t.LessonId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WeeklyPrograms", "UserId", "dbo.Users");
            DropForeignKey("dbo.WeeklyPrograms", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.WeeklyPrograms", "LessonId", "dbo.Lessons");
            DropForeignKey("dbo.WeeklyPrograms", "ClassId", "dbo.Classes");
            DropForeignKey("dbo.WeeklyPrograms", "AcademicYearId", "dbo.AcademicYears");
            DropIndex("dbo.WeeklyPrograms", new[] { "LessonId" });
            DropIndex("dbo.WeeklyPrograms", new[] { "TeacherId" });
            DropIndex("dbo.WeeklyPrograms", new[] { "ClassId" });
            DropIndex("dbo.WeeklyPrograms", new[] { "AcademicYearId" });
            DropIndex("dbo.WeeklyPrograms", new[] { "UserId" });
            DropTable("dbo.WeeklyPrograms");
        }
    }
}
