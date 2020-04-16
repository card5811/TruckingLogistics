namespace TruckingLogistics.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstmigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssetRoster",
                c => new
                    {
                        RosterId = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        CompanyUserId = c.Int(nullable: false),
                        TruckId = c.Int(nullable: false),
                        TrailerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RosterId)
                .ForeignKey("dbo.TrailerAsset", t => t.TrailerId, cascadeDelete: true)
                .ForeignKey("dbo.TruckAsset", t => t.TruckId, cascadeDelete: true)
                .ForeignKey("dbo.UserProfile", t => t.CompanyUserId, cascadeDelete: true)
                .Index(t => t.CompanyUserId)
                .Index(t => t.TruckId)
                .Index(t => t.TrailerId);
            
            CreateTable(
                "dbo.TrailerAsset",
                c => new
                    {
                        TrailerId = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        TrailerNumber = c.String(nullable: false),
                        TrailerVinNumber = c.String(nullable: false),
                        TrailerType = c.Int(nullable: false),
                        TrailerMileage = c.Int(),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.TrailerId);
            
            CreateTable(
                "dbo.TruckAsset",
                c => new
                    {
                        TruckId = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        TruckNumber = c.String(nullable: false),
                        Make = c.String(nullable: false),
                        Model = c.String(nullable: false),
                        Mileage = c.Int(),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.TruckId);
            
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        CompanyUserId = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        UserName = c.String(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.CompanyUserId);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(),
                        IdentityRole_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ApplicationUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRole", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserLogin", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserClaim", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.AssetRoster", "CompanyUserId", "dbo.UserProfile");
            DropForeignKey("dbo.AssetRoster", "TruckId", "dbo.TruckAsset");
            DropForeignKey("dbo.AssetRoster", "TrailerId", "dbo.TrailerAsset");
            DropIndex("dbo.IdentityUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.AssetRoster", new[] { "TrailerId" });
            DropIndex("dbo.AssetRoster", new[] { "TruckId" });
            DropIndex("dbo.AssetRoster", new[] { "CompanyUserId" });
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityRole");
            DropTable("dbo.UserProfile");
            DropTable("dbo.TruckAsset");
            DropTable("dbo.TrailerAsset");
            DropTable("dbo.AssetRoster");
        }
    }
}
