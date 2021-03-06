namespace VehicleManagementApp.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeeTableStatusFieldAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "Status");
        }
    }
}
