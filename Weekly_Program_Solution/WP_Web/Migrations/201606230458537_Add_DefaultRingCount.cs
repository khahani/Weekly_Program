namespace WP_Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_DefaultRingCount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AcademicYears", "DefaultRingCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AcademicYears", "DefaultRingCount");
        }
    }
}
