using System.Web;
using System.Web.Security;

namespace Zhigly.Code
{
    public class Authentication
    {
        public static bool IsActive()
        {
            HttpContext context = HttpContext.Current;

            if (context == null)
            {
                return false;
            }

            if (!context.User.Identity.IsAuthenticated)
            {
                return false;
            }

            if (context.User.Identity.Name.Length < 1)
            {
                return false;
            }

            return true;
        }

        public static void LogOut()
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }

        public static string GetEmail()
        {
            return HttpContext.Current.User.Identity.Name;
        }
    }
}