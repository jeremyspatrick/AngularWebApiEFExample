namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RoleType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RoleId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Session",
                c => new
                    {
                        SessionId = c.String(nullable: false, maxLength: 64),
                        UserId = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LastAccess = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SessionId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 256),
                        Salt = c.String(nullable: false, maxLength: 256),
                        Email = c.String(nullable: false, maxLength: 100),
                        Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UserDetail_UserDetailsId = c.Int(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.UserDetails", t => t.UserDetail_UserDetailsId)
                .Index(t => t.Username, unique: true)
                .Index(t => t.UserDetail_UserDetailsId);
            
            CreateTable(
                "dbo.UserDetails",
                c => new
                    {
                        UserDetailsId = c.Int(nullable: false, identity: true),
                        Firstname = c.String(maxLength: 50),
                        Lastname = c.String(maxLength: 50),
                        Street = c.String(maxLength: 50),
                        City = c.String(maxLength: 50),
                        State = c.String(maxLength: 50),
                        Zip = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.UserDetailsId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Session", "UserId", "dbo.User");
            DropForeignKey("dbo.User", "UserDetail_UserDetailsId", "dbo.UserDetails");
            DropForeignKey("dbo.Roles", "UserId", "dbo.User");
            DropIndex("dbo.User", new[] { "UserDetail_UserDetailsId" });
            DropIndex("dbo.User", new[] { "Username" });
            DropIndex("dbo.Session", new[] { "UserId" });
            DropIndex("dbo.Roles", new[] { "UserId" });
            DropTable("dbo.UserDetails");
            DropTable("dbo.User");
            DropTable("dbo.Session");
            DropTable("dbo.Roles");
        }
    }
}
