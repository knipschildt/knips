﻿using MVCShop.Models.Data;
using MVCShop.Models.ViewModels.Shop;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MVCShop.Areas.Admin.Controllers
{
    public class ShopController : Controller
    {
        // GET: Admin/Shop
        public ActionResult Categories()
        {
            List<CategoryVM> categoryVMList;

            using (Db db = new Db())
            {
                categoryVMList = db.Categories
                                    .ToArray()
                                    .OrderBy(x => x.Sorting)
                                    .Select(x => new CategoryVM(x))
                                    .ToList();
            }

            return View(categoryVMList);
        }
    }
}