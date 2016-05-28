namespace WP_Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAcademicYears : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AcademicYears",
                c => new
                    {
                        AcademicYearId = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.AcademicYearId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AcademicYears", "UserId", "dbo.Users");
            DropIndex("dbo.AcademicYears", new[] { "UserId" });
            DropTable("dbo.AcademicYears");
        }
    }
}
