using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Projet_Heritage.Models;

namespace Projet_Heritage.ViewModel
{
    public class GameListViewModel
    {
        public enum YearContent
        {
            Both,
            Four,
            Five
        }
        public enum ModuleContent
        {
            All,
            French,
            Math,
            Physics,
            Chemistry,
            // ReSharper disable once InconsistentNaming
            ELA,
            History,
            Finance
        }

        public int Group { get; set; } = 0;
        public string Research { get; set; } = "";
        public YearContent YearCont { get; set; } = YearContent.Both;
        public ModuleContent ModuleCont { get; set; } = ModuleContent.All;
        public List<Game> Games  { get; set; }
        public int? Realization { get; set; } = null;
        public List<int> RealizationList { get; set; } = new List<int>();
    }
}