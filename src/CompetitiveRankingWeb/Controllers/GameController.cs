using CompetitiveRankingWeb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CompetitiveRankingWeb.Controllers
{
    public class GameController : Controller
    {
        private RankingEntities db = new RankingEntities();

        // GET: Game
        public ActionResult Index()
        {
            var games = db.Games.GroupBy(g => g.ReleaseDate.Value.Year).Select(grp =>
                grp.OrderBy(x => x.ReleaseDate)
            ).ToList().Select(g => new Tuple<int, List<Game>>(g.First().ReleaseDate.Value.Year, g.ToList())).ToList();

            return View(games);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Import(Import import)
        {
            if (string.IsNullOrWhiteSpace(import.Data)) return View();

            var games = JsonConvert.DeserializeObject<List<Game>>(import.Data);

            db.Games.AddRange(games);
            db.SaveChanges();

            return View();
        }

        public ActionResult Edit(int id)
        {
            var existingGame = db.Games.SingleOrDefault(g => g.GameID == id);

            if (existingGame == null)
            {
                throw new Exception();
            }

            return View(existingGame);
        }

        public ActionResult Details(int id)
        {
            var existingGame = db.Games.SingleOrDefault(g => g.GameID == id);

            if (existingGame == null)
            {
                throw new Exception();
            }

            return View(existingGame);
        }

        public ActionResult Save(Game game)
        {
            if (string.IsNullOrWhiteSpace(game.Name))
            {
                RedirectToAction("Index", "Home");
            }

            var existingGame = db.Games.SingleOrDefault(g => g.GameID == game.GameID);

            if (existingGame == null)
            {
                db.Games.Add(game);
                db.SaveChanges();
            }
            else
            {
                existingGame.Name = game.Name;
                existingGame.Publisher = game.Publisher;
                existingGame.Developer = game.Developer;
                existingGame.ReleaseDate = game.ReleaseDate;
                existingGame.Rating = game.Rating;
                existingGame.CompetedIn = game.CompetedIn;
                db.SaveChanges();
            }

            return View("Details");
        }
    }
}