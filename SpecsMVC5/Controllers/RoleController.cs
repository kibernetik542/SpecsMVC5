using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace SpecsMVC5.Controllers
{
    public class RoleController : Controller
    {
        // GET: Role
        public ActionResult Index()
        {
            var roles = Roles.GetAllRoles().ToList();
            return View(roles);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string RoleName)
        {
            Roles.CreateRole(RoleName);
            return RedirectToAction("Index");
        }
    }
}