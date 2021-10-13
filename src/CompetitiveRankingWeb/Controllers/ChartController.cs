using CompetitiveRankingWeb.Models;
using CompetitiveRankingWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CompetitiveRankingWeb.Controllers
{
    public class ChartController : Controller
    {
        private RankingEntities db = new RankingEntities();

        public ActionResult Index()
        {
            var platformTotals = new List<Tuple<string, int>>();

            foreach (var group in db.Playthroughs.GroupBy(p => p.Platform).OrderBy(g => g.Count()))
            {
                platformTotals.Add(Tuple.Create(group.Key.Name, group.Count()));
            }

            var viewModel = new ChartViewModel
            {
                PlaythroughsPerYear = db.Playthroughs.GroupBy(p => p.CompletedDate.Value.Year).ToList(),
                PlatformTotals = platformTotals
            };

            return View(viewModel);
        }
    }
}