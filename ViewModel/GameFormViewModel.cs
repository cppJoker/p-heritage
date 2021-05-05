using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Projet_Heritage.Models;

namespace Projet_Heritage.ViewModel
{
    public class GameFormViewModel
    {
        public Game Game { get; set; }
        public HttpPostedFileBase File { get; set; }
        public HttpPostedFileBase BigFile { get; set; }
        public HttpPostedFileBase ResourceFile { get; set; }
        [Required]
        [Display(Name = "Code d'équipe")]
        public string Key { get; set; }
        public bool HasError { get; set; }
        public string ErrorString { get; set; }
    }
}