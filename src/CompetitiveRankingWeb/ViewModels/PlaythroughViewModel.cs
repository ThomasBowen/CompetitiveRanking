using CompetitiveRankingWeb.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CompetitiveRankingWeb.ViewModels
{
    public class PlaythroughViewModel
    {
        public Playthrough Playthrough { get; set; }
        public List<SelectListItem> Platforms { get; set; }
        public string Platform { get; set; }
    }
}