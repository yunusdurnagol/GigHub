namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class PopulateGenreTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres (Id,Name) VALUES (1,'Blues')");
            Sql("INSERT INTO Genres (Id,Name) VALUES (2,'Jazz')");
            Sql("INSERT INTO Genres (Id,Name) VALUES (3,'Rock')");
            Sql("INSERT INTO Genres (Id,Name) VALUES (4,'Pop')");
        }

        public override void Down()
        {
            Sql("DELETE FROM Genres WHERE Id IN (1,2,3,4)");
        }
    }
}
