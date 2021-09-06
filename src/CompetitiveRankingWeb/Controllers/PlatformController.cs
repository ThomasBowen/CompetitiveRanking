using CompetitiveRankingWeb.Models;
using System.Linq;
using System.Web.Mvc;

namespace CompetitiveRankingWeb.Controllers
{
    public class PlatformController : Controller
    {
        private RankingEntities db = new RankingEntities();

        public ActionResult Index()
        {
            return View(db.Platforms);
        }

        public ActionResult Details(int id)
        {
            var existingPlatform = db.Platforms.SingleOrDefault(g => g.PlatformID == id);

            if (existingPlatform == null)
            {
                throw new System.Exception();
            }

            return View(existingPlatform);
        }
    }
}