using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projet_Heritage.Models
{
    public class Game
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Nom du jeu")]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Thème")]
        public string Theme { get; set; }

        [Display(Name = "Matière")]
        public string Modules { get; set; }

        [Required]
        [Display(Name = "Membres de l'équipe (separé avec virgule)")]
        public string TeamMembers { get; set; }

        [Display(Name = "Groupe")]
        [Range(31, 34)]
        public int Group { get; set; }

        [Required]
        [Display(Name = "Année dont le contenu est presenté")]
        public string yearContent { get; set; }

        [Required]
        [Display(Name = "Lien vers le projet (doit commencer par https://)")]
        public string Link { get; set; }

        [Display(Name = "Lien vers le sondage (optionnel, doit commencer par https://)")]
        public string FormLink { get; set; }

        [Required]
        [Display(Name = "Lien vers le solutionnaire (doit commencer par https://)")]
        public string SolutionLink { get; set; }

        [Display(Name = "Lien vers le mode d'emploi (optionnel, doit commencer par https://)")]
        public string GuideLink { get; set; }

        public int Stars { get; set; }
        
        public string imagePath { get; set; }
        public string largeImagePath { get; set; }
        public string resourcePath { get; set; }
        public DateTime DatePublished { get; set; }

    }
}