namespace EmployeeRegister.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewConstraints : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "FirstName", c => c.String(maxLength: 32));
            AlterColumn("dbo.Employees", "LastName", c => c.String(maxLength: 32));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "LastName", c => c.String());
            AlterColumn("dbo.Employees", "FirstName", c => c.String());
        }
    }
}
