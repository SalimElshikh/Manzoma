using ElecWarSystem.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace YourNamespace.Controllers
{
    public class ManzomatController : Controller
    {
        // GET: Cards
        public ActionResult Index()
        {
            // إنشاء قائمة من البطاقات
            var cards = new List<Manzomat>
            {

            new Manzomat
            {
                Id = 1,
                ImageUrl = "../Images/LoginLogo.png",
                Title = "منطومة تبادل الملفات ",
                Description = ".هذة منظومة لتبادل الملفات بين الوحدات ",
                ButtonText = "تبادل الملفات",
                LinkUrl = Url.Action("Index", "Email")
            },
            new Manzomat
            {
                Id = 2,
                ImageUrl = "../Images/LoginLogo.png",
                Title = "منظومة التمامات ",
                Description = "هذة المنظومة عبارة عن تمامات الافراد ومذيد من المعلومات ",
                ButtonText = "تمامات الافراد ",
                LinkUrl = Url.Action("LeaderShip", "TmamGathering")
            }
            };

            // إرجاع العرض مع إرسال القائمة كـ Model
            return View(cards);
        }
    }
}
