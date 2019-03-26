namespace SignalRServer.Migrations
{
    using SignalRServer.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Models.GameContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Models.GameContext context)
        {
            var playerList = new List<Player>()
            {
                new Player() { Username = "Answerer" },
                new Player() { Username = "Locator" }
            };

            context.Players.AddOrUpdate(playerList.ToArray());
        }
    }
}
