namespace GeneralStore.MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class one : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reviews", "ReviewTitle", c => c.String(maxLength: 300));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reviews", "ReviewTitle", c => c.String());
        }
    }
}
