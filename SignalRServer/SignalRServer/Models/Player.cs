using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace SignalRServer.Models
{
    public class Player
    {
        [Key]
        [MaxLength(25),MinLength(2)]
        public string Username { get; set; }

        public ICollection<PlayerMessage> SentMessages { get; set; }
    }

    public class PlayerMessage
    {
        [Key]
        public int ID { get; set; }
        
        //public string PlayerUsername;

        [Required]
        [ForeignKey("Player")]
        [MaxLength(25), MinLength(2)]
        public string Username { get; set; }
        [Required]
        [MaxLength(300)]
        public string MessageSent { get; set; }
        [Required]
        public DateTime DateSent { get; set; }

        public Player Player { get; set; }
    }

    public class GameContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerMessage> Messages { get; set; }

        public GameContext() : base("ChatDB")
        {

        }
    }
}