using SpecsMVC5.App_Classes;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace SpecsMVC5.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            MembershipUserCollection users = Membership.GetAllUsers();
            return View(users);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            MembershipCreateStatus status;
            Membership.CreateUser(user.UserName,
                user.Password,
                user.Email,
                user.SecretQuestion,
                user.SecretAnswer,
                true,
                out status);
            string message = "";
            switch (status)
            {
                case MembershipCreateStatus.DuplicateEmail:
                    message += " Email already in use";
                    break;
                case MembershipCreateStatus.DuplicateProviderUserKey:
                    message += " User key error";
                    break;
                case MembershipCreateStatus.DuplicateUserName:
                    message += " Username already in use";
                    break;
                case MembershipCreateStatus.InvalidAnswer:
                    message += " Answer is not allowed";
                    break;
                case MembershipCreateStatus.InvalidEmail:
                    message += " Wrong Email";
                    break;
                case MembershipCreateStatus.InvalidPassword:
                    message += " Wrong password";
                    break;
                case MembershipCreateStatus.InvalidProviderUserKey:
                    message += " Member key error";
                    break;
                case MembershipCreateStatus.InvalidQuestion:
                    message += " Question is not allowed";
                    break;
                case MembershipCreateStatus.InvalidUserName:
                    message += " Wrong username";
                    break;
                case MembershipCreateStatus.ProviderError:
                    message += " Member management provider error";
                    break;
                case MembershipCreateStatus.Success:
                    break;
                case MembershipCreateStatus.UserRejected:
                    message += " User rejected error";
                    break;
                default:
                    break;
            }
            ViewBag.Message = message;
            if (status == MembershipCreateStatus.Success)
                return RedirectToAction("Index");
            else
                return View();
        }


        public ActionResult CreateRole()
        {
            ViewBag.Roles = Roles.GetAllRoles().ToList();
            ViewBag.Users = Membership.GetAllUsers();
            return View();
        }

        [HttpPost]
        public ActionResult CreateRole(string UserName, string RoleName)
        {
            Roles.AddUserToRole(UserName, RoleName);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public string UserRoles(string userName)
        {
            var roles = Roles.GetRolesForUser(userName).ToList();
            var role = "";
            foreach (string r in roles)
            {
                role += r + "-";
            }

            role = role.Remove(role.Length - 1, 1);
            return role;
        }


    }
}

