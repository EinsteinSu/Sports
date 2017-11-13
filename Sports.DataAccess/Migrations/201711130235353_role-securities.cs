namespace Sports.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rolesecurities : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SecurityRoles", "Security_Id", "dbo.Securities");
            DropForeignKey("dbo.SecurityRoles", "Role_Id", "dbo.Roles");
            DropIndex("dbo.SecurityRoles", new[] { "Security_Id" });
            DropIndex("dbo.SecurityRoles", new[] { "Role_Id" });
            AddColumn("dbo.Roles", "Securities", c => c.String());
            AlterColumn("dbo.Centrals", "IPAddress", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Displays", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Displays", "IPAddress", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Venues", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Venues", "IPAddress", c => c.String(maxLength: 20));
            AlterColumn("dbo.Venues", "Location", c => c.String(maxLength: 50));
            AlterColumn("dbo.Hardwares", "Usage", c => c.String(maxLength: 200));
            AlterColumn("dbo.SecurityGroups", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Players", "Name", c => c.String(nullable: false, maxLength: 50));
            DropTable("dbo.SecurityRoles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SecurityRoles",
                c => new
                    {
                        Security_Id = c.Int(nullable: false),
                        Role_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Security_Id, t.Role_Id });
            
            AlterColumn("dbo.Players", "Name", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.SecurityGroups", "Name", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Hardwares", "Usage", c => c.String(maxLength: 50));
            AlterColumn("dbo.Venues", "Location", c => c.String(maxLength: 20));
            AlterColumn("dbo.Venues", "IPAddress", c => c.String(maxLength: 11));
            AlterColumn("dbo.Venues", "Name", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Displays", "IPAddress", c => c.String(nullable: false, maxLength: 11));
            AlterColumn("dbo.Displays", "Name", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Centrals", "IPAddress", c => c.String(nullable: false, maxLength: 11));
            DropColumn("dbo.Roles", "Securities");
            CreateIndex("dbo.SecurityRoles", "Role_Id");
            CreateIndex("dbo.SecurityRoles", "Security_Id");
            AddForeignKey("dbo.SecurityRoles", "Role_Id", "dbo.Roles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SecurityRoles", "Security_Id", "dbo.Securities", "Id", cascadeDelete: true);
        }
    }
}
