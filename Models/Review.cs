using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projet_Heritage.Models
{
    public class Review
    {
        public int Id { get; set; }
        public SerialKey SerialKey { get; set; }
        public int SerialKeyId { get; set; }

        public Game Game { get; set; }
        public int GameId { get; set; }

        public string Authors { get; set; }

        public DateTime Published { get; set; }

        [Display(Name = "Commentaire")]
        public string Content { get; set; }
        
    }
}