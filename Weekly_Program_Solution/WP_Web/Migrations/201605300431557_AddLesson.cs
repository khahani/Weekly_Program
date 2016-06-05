namespace WP_Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLesson : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Lessons",
                c => new
                    {
                        LessonId = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        AcademicYearId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.LessonId)
                .ForeignKey("dbo.AcademicYears", t => t.AcademicYearId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.AcademicYearId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lessons", "UserId", "dbo.Users");
            DropForeignKey("dbo.Lessons", "AcademicYearId", "dbo.AcademicYears");
            DropIndex("dbo.Lessons", new[] { "AcademicYearId" });
            DropIndex("dbo.Lessons", new[] { "UserId" });
            DropTable("dbo.Lessons");
        }
    }
}
