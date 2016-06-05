namespace WP_Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_CanTeaches : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CanTeaches",
                c => new
                    {
                        CanTeachId = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        TeacherId = c.Int(nullable: false),
                        LessonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CanTeachId)
                .ForeignKey("dbo.Lessons", t => t.LessonId, cascadeDelete: true)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.TeacherId)
                .Index(t => t.LessonId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CanTeaches", "UserId", "dbo.Users");
            DropForeignKey("dbo.CanTeaches", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.CanTeaches", "LessonId", "dbo.Lessons");
            DropIndex("dbo.CanTeaches", new[] { "LessonId" });
            DropIndex("dbo.CanTeaches", new[] { "TeacherId" });
            DropIndex("dbo.CanTeaches", new[] { "UserId" });
            DropTable("dbo.CanTeaches");
        }
    }
}
