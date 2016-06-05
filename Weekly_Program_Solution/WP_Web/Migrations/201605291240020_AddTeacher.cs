namespace WP_Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTeacher : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        TeacherId = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        AcademicYearId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Job = c.String(),
                    })
                .PrimaryKey(t => t.TeacherId)
                .ForeignKey("dbo.AcademicYears", t => t.AcademicYearId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => new { t.Name, t.AcademicYearId }, unique: true, name: "IX_NameAndAcademicYear");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teachers", "UserId", "dbo.Users");
            DropForeignKey("dbo.Teachers", "AcademicYearId", "dbo.AcademicYears");
            DropIndex("dbo.Teachers", "IX_NameAndAcademicYear");
            DropIndex("dbo.Teachers", new[] { "UserId" });
            DropTable("dbo.Teachers");
        }
    }
}
