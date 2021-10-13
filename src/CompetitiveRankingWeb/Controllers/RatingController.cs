using CompetitiveRankingWeb.Models;
using CompetitiveRankingWeb.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;

namespace CompetitiveRankingWeb.Controllers
{
    public class RatingController : Controller
    {
        private RankingEntities db = new RankingEntities();

        public ActionResult Index()
        {
            var gotys = db.Games.GroupBy(g => g.ReleaseDate.Value.Year).Select(grp => 
                grp.OrderByDescending(x => x.Rating).FirstOrDefault()
            ).ToList().Select(g => new Tuple<int, Game>(g.ReleaseDate.Value.Year, g)).ToList();

            var gotds = db.Games.GroupBy(g => g.ReleaseDate.Value.Year / 10 * 10).Select(grp =>
                grp.OrderByDescending(x => x.Rating).FirstOrDefault()
            ).ToList().Select(g => new Tuple<int, Game>(g.ReleaseDate.Value.Year / 10 * 10, g)).ToList();

            return View(new GOTYViewModel
            {
                GOTYS = gotys,
                GOTDS = gotds,
                GOAT = db.Games.OrderByDescending(x => x.Rating).FirstOrDefault(),
                AllGames = db.Games.OrderByDescending(g => g.Rating).ToList()
            });
        }

        [HttpPost]
        public JsonResult Update(RatingUpdate update)
        {
            var winner = db.Games.Single(g => g.GameID == update.WinningID);
            var loser = db.Games.Single(g => g.GameID == update.LosingID);

            var winnerRating = winner.Rating;
            var loserRating = loser.Rating;

            CalculateELO(ref winnerRating, ref loserRating, GameOutcome.Win);

            winner.Rating = winnerRating;
            loser.Rating = loserRating;

            winner.CompetedIn++;
            loser.CompetedIn++;

            db.SaveChanges();

            return Json(true);
        }

        private static double ExpectationToWin(int playerOneRating, int playerTwoRating)
        {
            return 1 / (1 + Math.Pow(10, (playerTwoRating - playerOneRating) / 400.0));
        }

        private static void CalculateELO(ref int playerOneRating, ref int playerTwoRating, GameOutcome outcome)
        {
            int eloK = 25;

            int delta = (int)(eloK * ((int)outcome - ExpectationToWin(playerOneRating, playerTwoRating)));

            playerOneRating += delta;
            playerTwoRating -= delta;
        }
    }
}