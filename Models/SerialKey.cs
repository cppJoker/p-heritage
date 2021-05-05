using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projet_Heritage.Models
{
    public class SerialKey
    {
        public int Id { get; set; }
        
        public string Key { get; set; }
        public bool Activated { get; set; }
        public bool CanEdit { get; set; }
        public Game Game { get; set; }
        public int? GameID { get; set; }
    }
}