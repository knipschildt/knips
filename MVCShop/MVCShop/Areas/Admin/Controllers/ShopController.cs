using MVCShop.Models.Data;
using MVCShop.Models.ViewModels;
using MVCShop.Models.ViewModels.Shop;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
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

        // POST: Admin/Shop/AddNewCategory
        [HttpPost]
        public string AddNewCategory(string catName)
        {
            string id;

            using (Db db = new Db())
            {
                if (db.Categories.Any(x => x.Name == catName))
                    return "titletaken";

                CategoryDTO dto = new CategoryDTO();
                dto.Name = catName;
                dto.Slug = catName.Replace(" ", "-").ToLower();
                dto.Sorting = 100;

                db.Categories.Add(dto);
                db.SaveChanges();

                id = dto.Id.ToString();

                return id;
            }

        }

        [HttpPost]
        public void ReorderCategories(int[] id)
        {
            using (Db db = new Db())
            {
                int count = 1;

                CategoryDTO dto;

                foreach (var categoriId in id)
                {
                    dto = db.Categories.Find(categoriId);
                    dto.Sorting = count;
                    db.SaveChanges();

                    count++;
                }

            }

        }

        public ActionResult DeleteCategory(int id)
        {
            using (Db db = new Db())
            {
                CategoryDTO dto = db.Categories.Find(id);

                db.Categories.Remove(dto);

                db.SaveChanges();
            }
            return RedirectToAction("Categories");
        }

        // POST: Admin/Shop/RenameCategory
        public string RenameCategory(string newCatName, int id)
        {
            using (Db db = new Db())
            {
                if (db.Categories.Any(x => x.Name == newCatName))
                {
                    return "titletaken";
                }
                else
                {
                    CategoryDTO dto = db.Categories.Find(id);
                    dto.Name = newCatName;
                    dto.Slug = newCatName.Replace(" ", "-").ToLower();

                    db.SaveChanges();



                }
                return "ok";
            }


        }

        // GET: Admin/Shop/AddProduct
        [HttpGet]
        public ActionResult AddProduct()
        {
            // Init model
            ProductVM model = new ProductVM();

            // Add select list of categories to model
            using (Db db = new Db())
            {
                model.Categories = new SelectList(db.Categories.ToList(), "Id", "Name");
            }

            // Return view with model
            return View(model);
        }

        [HttpPost]
        public ActionResult AddProduct(ProductVM model, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (Db db = new Db())
            {
                if (db.Products.Any(x => x.Name == model.Name))
                {
                    model.Categories = new SelectList(db.Categories.ToList(), "Id", "Name");
                    ModelState.AddModelError("", "That productname is taken");
                    return View(model);
                }
            }
            int id;

            using (Db db = new Db())
            {
                ProductDTO product = new ProductDTO();
                product.Name = model.Name;
                product.Slug = model.Name.Replace(" ", "-").ToLower();
                product.Description = model.Description;
                product.Price = model.Price;
                product.CategoryId = model.Id;

                CategoryDTO catDTO = db.Categories.FirstOrDefault(x => x.Id == model.CategoryId);
                model.CategoryName = catDTO.Name;

                db.Products.Add(product);
                db.SaveChanges();

                id = product.Id;
            }

            TempData["SM"] = "You have added a product";

            #region Upload image

            var originalDirectory = new DirectoryInfo(string.Format("{0}images\\Uploads", Server.MapPath(@"\")));
            var pathString1 = Path.Combine(originalDirectory.ToString(), "Products");
            var pathString2 = Path.Combine(originalDirectory.ToString(), "Products\\" + id.ToString());
            var pathString3 = Path.Combine(originalDirectory.ToString(), "Products\\" + id.ToString() + "\\Thumbs");
            var pathString4 = Path.Combine(originalDirectory.ToString(), "Products\\" + id.ToString() + "\\Gallery");
            var pathString5 = Path.Combine(originalDirectory.ToString(), "Products\\" + id.ToString() + "\\Gallery\\Thumbs");

            if (!Directory.Exists(pathString1))
                Directory.CreateDirectory(pathString1);
            if (!Directory.Exists(pathString2))
                Directory.CreateDirectory(pathString2);
            if (!Directory.Exists(pathString3))
                Directory.CreateDirectory(pathString3);
            if (!Directory.Exists(pathString4))
                Directory.CreateDirectory(pathString4);
            if (!Directory.Exists(pathString5))
                Directory.CreateDirectory(pathString5);

            if (file != null && file.ContentLength > 0)
            {
                string ext = file.ContentType.ToLower();

                if (ext != "image/jpg" &&
                    ext != "image/jpeg" &&
                    ext != "image/pjpg" &&
                    ext != "image/gif" &&
                    ext != "image/png" &&
                    ext != "image/x-png")
                {
                    using (Db db = new Db())
                    {

                        model.Categories = new SelectList(db.Categories.ToList(), "Id", "Name");
                        ModelState.AddModelError("", "The image was not uploaded - wrong image format");
                        return View(model);

                    }
                }
                string imageName = file.FileName;

                using (Db db = new Db())
                {
                    ProductDTO dto = db.Products.Find(id);
                    dto.Name = imageName;

                    db.SaveChanges();
                }

                var path = string.Format("{0}\\{1}", pathString2, imageName);
                var path2 = string.Format("{0}\\{1}", pathString3, imageName);

                file.SaveAs(path);

                WebImage img = new WebImage(file.InputStream);
                img.Resize(200, 200);
                img.Save(path2);
            }
            #endregion

            return RedirectToAction("AddProduct");
        }
    }
}