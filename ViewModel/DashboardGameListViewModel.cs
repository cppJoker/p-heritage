﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projet_Heritage.ViewModel
{
    public class DashboardGameListViewModel
    {
        public List<Models.Game> Games { get; set; }
        public DateTime HandInDateTime { get; set; }
        public int? Realization { get; set; } = null;
        public List<int> RealizationList { get; set; } = new List<int>();

    }
}