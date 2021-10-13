using CompetitiveRankingWeb.Models;
using CompetitiveRankingWeb.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;

namespace CompetitiveRankingWeb.Controllers
{
    public class PlaythroughController : Controller
    {
        private RankingEntities db = new RankingEntities();

        public ActionResult Index()
        {
            return View(db.Playthroughs.OrderBy(p => p.CompletedDate));
        }

        public ActionResult Edit(int id)
        {
            var playthrough = db.Playthroughs.SingleOrDefault(g => g.PlaythroughID == id);

            if (playthrough == null)
            {
                throw new Exception();
            }

            var platforms = db.Platforms.Select(x =>
                                  new SelectListItem()
                                  {
                                      Value = x.PlatformID.ToString(),
                                      Text = x.Name,
                                      Selected = x.PlatformID == playthrough.Platform_ID
                                  }).ToList();

            return View(new PlaythroughViewModel { Playthrough = playthrough, Platforms = platforms });
        }

        public ActionResult Save (Playthrough playthrough, string platform)
        {
            var platformID = int.Parse(platform);
            playthrough.Platform_ID = platformID;

            var existingPlaythrough = db.Playthroughs.SingleOrDefault(g => g.PlaythroughID == playthrough.PlaythroughID);

            if (existingPlaythrough == null)
            {
                db.Playthroughs.Add(playthrough);
                db.SaveChanges();
            }
            else
            {
                existingPlaythrough.Game_ID = playthrough.Game_ID;
                existingPlaythrough.Platform_ID = playthrough.Platform_ID;
                existingPlaythrough.CompletedDate = playthrough.CompletedDate;
                existingPlaythrough.Time = playthrough.Time;
                db.SaveChanges();
            }

            return View("Edit", playthrough.PlaythroughID + 1);
        }
    }
}