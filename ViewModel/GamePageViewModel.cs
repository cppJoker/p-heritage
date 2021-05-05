using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Projet_Heritage.Models;

namespace Projet_Heritage.ViewModel
{
    public class GamePageViewModel
    {
        public Game Game { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public Review Comment { get; set; }
        [Required]
        [Display(Name = "Code d'équipe")]
        public string Key { get; set; }

        public bool HadError = false;
    }
}