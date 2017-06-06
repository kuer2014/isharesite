using BetterSite.UI.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace BetterSite.UI.Areas.Admin.Controllers
{
          //[AuthorizeFilter]
    public class AccountController : Controller
    {
        //
        // GET: /Admin/Account/

        public ActionResult Index()
        {
            return View();
        }
        //
        // GET: /Account/Login

        //[AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            var user = System.Web.HttpContext.Current.Request.Cookies["user"];
            if (user != null) { 
            ViewBag.userName = user["userName"];
            ViewBag.password = user["password"];
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
/// <summary>
/// 
/// </summary>
/// <param name="userName"></param>
/// <param name="password"></param>
        /// <param name="isPersistent">如果票证将存储在持久性 Cookie 中（跨浏览器会话保存），则为 true；否则为 false。 如果该票证存储在 URL 中，将忽略此值。 </param>
/// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string userName, string password, string returnUrl,string rememberMe, bool isPersistent = true)
        {
            string _userName = System.Configuration.ConfigurationManager.AppSettings["userName"].ToLower();
            string _password = System.Configuration.ConfigurationManager.AppSettings["password"];
            if (userName== _userName && password== _password)
            {
                //FormsAuthentication.SetAuthCookie(userName, true, FormsAuthentication.FormsCookiePath);
                //string userData = "ApplicationSpecific data for this user.";
                //FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                //  userName,
                //  DateTime.Now,
                //  DateTime.Now.AddMinutes(30),
                //  isPersistent,
                //  userData,
                //  FormsAuthentication.FormsCookiePath);

                //// Encrypt the ticket.
                //string encTicket = FormsAuthentication.Encrypt(ticket);

                //// Create the cookie.
                //Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
                //// Redirect back to original URL.
                ////Response.Redirect(FormsAuthentication.GetRedirectUrl(userName, isPersistent));

                //HttpCookie cookie = new HttpCookie("userName"); 
                //cookie.Value = userName;
                //cookie.Expires = DateTime.Now.AddHours(1); 
                //System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
                //                ///System.Web.HttpContext.Current.Response.AppendCookie(cookie);     

                if (HttpContext.Request.Form["rememberMe"]!=null&&HttpContext.Request.Form["rememberMe"].ToLower()=="on") {
                    HttpCookie cookie = new HttpCookie("user");
                    cookie["userName"] = userName;
                    cookie["password"] = password;
                    cookie.Expires = DateTime.Now.AddDays(10);
                    System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
                }
                Session.Timeout = 10;//10分钟不操作页面即失效
                Session["userName"] = userName;
                return RedirectToLocal(returnUrl);
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "用户名或密码不正确");
            return View();
        }

        //
        // POST: /Account/LogOff

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            //WebSecurity.Logout();
         //   FormsAuthentication.SignOut();
           // FormsAuthentication.RedirectToLoginPage();
            HttpCookie cookie = Request.Cookies["userName"];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-2);
                Response.Cookies.Set(cookie);
            }
            return RedirectToAction("Login", "Account");
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return this.RedirectToAction("Index", "Home");
        }

    }
}
