using CompetitiveRankingWeb.Models;
using CompetitiveRankingWeb.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;

namespace CompetitiveRankingWeb.Controllers
{
    public class HomeController : Controller
    {
        private RankingEntities db = new RankingEntities();

        public ActionResult Index()
        {
            var games = db.Games.OrderBy(g => g.CompetedIn).ToArray();

            var firstGame = games[0];
            var similarScoreGames = games.Where(g => g.Rating < firstGame.Rating + 20 && g.Rating > firstGame.Rating - 20 && g.GameID != firstGame.GameID).ToArray();

            var rnd = new Random();

            return View(new RankingViewModel
            {
                FirstGame = firstGame,
                SecondGame = similarScoreGames[rnd.Next(similarScoreGames.Count())]
            });
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}