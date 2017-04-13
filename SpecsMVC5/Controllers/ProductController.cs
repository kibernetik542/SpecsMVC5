using SpecsMVC5.Models;
using System.Linq;
using System.Web.Mvc;

namespace SpecsMVC5.Controllers
{
    public class ProductController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();


        // GET: Product
        public ActionResult Index()
        {
            var products = db.Products.ToList();
            return View(products);
        }

        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");

            return View();
        }


        [HttpPost]
        public ActionResult Create(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpPost]
        public void Delete(int id)
        {
            Product product = db.Products.FirstOrDefault(x => x.ProductId == id);
            db.Products.Remove(product);
            db.SaveChanges();
        }

    }
}