using MVCShop.Models.Data;
using MVCShop.Models.ViewModels.Pages;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MVCShop.Areas.Admin.Controllers
{
    public class PagesController : Controller
    {
        // GET: Admin/Pages
        public ActionResult Index()
        {
            List<PageVM> PageList;

            using (Db db = new Db())
            {
                PageList = db.Pages.ToArray().OrderBy(x => x.Sorting).Select(x => new PageVM(x)).ToList();
            }

            return View(PageList);
        }

        [HttpGet]
        public ActionResult AddPage()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Addpage(PageVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (Db db = new Db())
            {
                string slug;

                PageDTO dto = new PageDTO();

                dto.Title = model.Title;
                if (string.IsNullOrWhiteSpace(model.Slug))
                {
                    slug = model.Title.Replace(" ", "-").ToLower();
                }
                else
                {
                    slug = model.Slug.Replace(" ", "-").ToLower();
                }

                if (db.Pages.Any(x => x.Title == model.Title) || db.Pages.Any(x => x.Slug == model.Slug))
                {
                    ModelState.AddModelError("", "That Title or Slug already exist!");
                    return View(model);
                }

                dto.Slug = slug;
                dto.Body = model.Body;
                dto.HasSideBar = model.HasSideBar;
                dto.Sorting = 100;

                db.Pages.Add(dto);
                db.SaveChanges();

            }
            TempData["SM"] = "You have added a new page!";

            return RedirectToAction("AddPage");

        }

        [HttpGet]
        public ActionResult EditPage(int id)
        {
            PageVM model;

            using (Db db = new Db())
            {
                PageDTO dto = db.Pages.Find(id);

                if (dto == null)
                {
                    return Content("Siden eksisterer ikke");
                }

                model = new PageVM(dto);

            }
            return View(model);
        }

        [HttpPost]
        public ActionResult EditPage(PageVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (Db db = new Db())
            {

                int id = model.Id;

                string slug = "home";

                PageDTO dto = db.Pages.Find(id);

                dto.Title = model.Title;

                if (model.Slug != "home")
                {
                    if (string.IsNullOrWhiteSpace(model.Slug))
                    {
                        slug = model.Title.Replace(" ", "-").ToLower();

                    }else
                    {
                        slug = model.Slug.Replace(" ","-").ToLower();
                    }

                }
               if(db.Pages.Where(x=> x.Id != id).Any(x=> x.Title == model.Title)||
                    db.Pages.Where(x => x.Id != id).Any(x => x.Slug == slug))
                {
                    ModelState.AddModelError("", "That tiitle or slug already exist");
                    return View(model);
                }

                dto.Slug = slug;
                dto.HasSideBar = model.HasSideBar;
                dto.Body = model.Body;

                db.SaveChanges();

            }

            TempData["SM"] = "You have edited the page";

            return RedirectToAction("EditPage");
        }
    }
}