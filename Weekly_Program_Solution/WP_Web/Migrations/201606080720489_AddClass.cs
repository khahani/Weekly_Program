namespace WP_Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Classes",
                c => new
                    {
                        ClassId = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        AcademicYearId = c.Int(nullable: false),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.ClassId)
                .ForeignKey("dbo.AcademicYears", t => t.AcademicYearId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.AcademicYearId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Classes", "UserId", "dbo.Users");
            DropForeignKey("dbo.Classes", "AcademicYearId", "dbo.AcademicYears");
            DropIndex("dbo.Classes", new[] { "AcademicYearId" });
            DropIndex("dbo.Classes", new[] { "UserId" });
            DropTable("dbo.Classes");
        }
    }
}
