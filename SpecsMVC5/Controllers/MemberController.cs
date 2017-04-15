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


        [AllowAnonymous]
        public ActionResult ForgottenPassword()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult ForgottenPassword(User user)
        {
            MembershipUser mu = Membership.GetUser(user.UserName);
            if (mu.PasswordQuestion == user.SecretQuestion)
            {
                string pass = mu.ResetPassword(user.SecretAnswer);
                mu.ChangePassword(pass, user.Password);
                return RedirectToAction("Login");
            }

            else
            {
                ViewBag.Result = "Invalid Question or answer";
                return View();
            }



        }

    }
}