using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace Projet_Heritage.Models
{
    public class PlatformSetting
    {
        public int Id { get; set; }

        [Display(Name = "Titre de la page d'accueil")]
        public string MainTitle { get; set; }
        [Display(Name = "Icône de la page d'accueil")]
        public string MainTitleIcon { get; set; }
        [Display(Name = "Sous-titre de la page d'accueil")]
        public string MainSubTitle { get; set; }
        [Display(Name = "Description de la page d'accueil")]
        public string MainDescription { get; set; }
        [Display(Name = "Date de remise")]
        public DateTime HandInDateTime { get; set; }
    }
}