using SpecsMVC5.App_Classes;
using System.Web.Mvc;
using System.Web.Security;

namespace SpecsMVC5.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(User user, string remember)
        {
            bool result = Membership.ValidateUser(user.UserName, user.Password);
            if (result)
            {
                if (remember == "on")
                    FormsAuthentication.RedirectFromLoginPage(user.UserName, true);
                else
                    FormsAuthentication.RedirectFromLoginPage(user.UserName, false);


                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid username or password";
                return View();
            }

        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}