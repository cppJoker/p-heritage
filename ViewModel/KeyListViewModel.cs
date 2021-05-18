using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Projet_Heritage.Models;

namespace Projet_Heritage.ViewModel
{
    public class KeyListViewModel
    {
        public Dictionary<SerialKey, Game> keyGamePair = new Dictionary<SerialKey, Game>();
        public int? Realization { get; set; } = null;
        public List<int> RealizationList { get; set; } = new List<int>();
    }
}