using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projet_Heritage.ViewModel
{
    public class DashboardGameListViewModel
    {
        public List<Models.Game> Games { get; set; }
        public DateTime HandInDateTime { get; set; }
    }
}