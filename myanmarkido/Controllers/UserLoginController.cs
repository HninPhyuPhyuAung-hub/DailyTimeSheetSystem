using myanmarkido.Models;
using myanmarkido.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace myanmarkido.Controllers
{
    public class UserLoginController : Controller
    {
        // GET: UserLogin
        AccountRepository accrepo = new AccountRepository();
        // GET: UserLogin
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel login)
        {
            dailysheetcontext context = new dailysheetcontext();
            if (ModelState.IsValid)
            {
                Account result = result = context.Accountset.Where(a => a.UserName == login.Email && a.Password == login.Password && a.IsDeleted != true).FirstOrDefault();
                if (result != null)
                {
                    FormsAuthentication.SetAuthCookie(login.Email, true);
                    SetCookie("dailyCookie", result.EmpId, result.Name, result.UserName,result.Role,result.Position);
                    if (result.Role == "Staff")
                    {
                        return RedirectToAction("AddDailySheet", "DailySheet");
                    }
                    else
                    {
                        return RedirectToAction("AdminJob", "DailySheet");
                    }
                }
                else
                {
                    ViewBag.Unauthorize = "Please enter valid user name or email and password";
                }
                return View();
            }
            return View();
        }

        public void SetCookie(string CookieName, int EmpId, string UserName, string Email,string Role,string Position)
        {
            HttpCookie myCookie = HttpContext.Request.Cookies[CookieName] ?? new HttpCookie(CookieName);
            myCookie.Values["EmpId"] = EmpId.ToString();
            //myCookie.Values["Name"] = HttpUtility.UrlEncode(Name);
            myCookie.Values["UserName"] = UserName;
            myCookie.Values["Email"] = Email;
            myCookie.Values["Position"] = Position;
            myCookie.Values["Role"] = Role;
           
            myCookie.Expires = DateTime.Now.AddDays(30);
            HttpContext.Response.Cookies.Add(myCookie);
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            RemoveCookie("dailyCookie");
            Response.Cookies["dailyCookie"].Expires = DateTime.Now.AddYears(-30);
            Session.Clear();
            return RedirectToAction("Login", "UserLogin");
        }
        public bool RemoveCookie(String CookieName)
        {
            if (HttpContext.Request.Cookies[CookieName] != null)
            {
                HttpCookie myCookie = HttpContext.Request.Cookies[CookieName];
                myCookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Response.Cookies.Add(myCookie);
                return true;
            }
            return false;
        }
        public ActionResult Register(int? EmpId)
        {
            if(EmpId!=null)
            {
                dailysheetcontext context = new dailysheetcontext();
                var result = context.Accountset.Where(a => a.EmpId == EmpId).ToList();
                
                RegisterViewModel model = new RegisterViewModel();
                model.UserName = result.Select(a => a.Name).FirstOrDefault();
                model.Email = result.Select(a => a.UserName).FirstOrDefault();
                model.Password = result.Select(a => a.Password).FirstOrDefault();
                model.Role = result.Select(a => a.Role).FirstOrDefault();
                model.Position = result.Select(a => a.Position).FirstOrDefault();
                model.EmpId = result.Select(a => a.EmpId).FirstOrDefault();
                return View(model);
            }
            else
            {

                return View(new RegisterViewModel());
            }
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(RegisterViewModel register)
        {
            dailysheetcontext context = new dailysheetcontext();
            Account result = new Account();
            result.Name = register.UserName;
            result.Position = register.Position;
            result.UserName = register.Email;
            result.Password = register.Password;
            result.Role = register.Role;
            result.IsDeleted = false;
        
            if (register.EmpId!=0)
            {
                var result1 = context.Accountset.Where(e => e.UserName == register.Email).FirstOrDefault();

                if (result1 == null)
                {

                    var data = accrepo.Update(result);
                    FormsAuthentication.SetAuthCookie(register.Email, true);
                    SetCookie("dailyCookie", data.EmpId, data.Name, data.UserName, data.Role, data.Position);
                    return RedirectToAction("EmployeeList", "DailySheet");
                }

                else
                {
                    ViewBag.Message = "This email is already registerred!";
                    return View(register);
                }
            }
            else
            {
                var result1 = context.Accountset.Where(e => e.UserName == register.Email).FirstOrDefault();

                if (result1 == null)
                {

                    var data = accrepo.Add(result);
                    FormsAuthentication.SetAuthCookie(register.Email, true);
                    SetCookie("dailyCookie", data.EmpId, data.Name, data.UserName, data.Role, data.Position);
                    return RedirectToAction("EmployeeList", "DailySheet");
                }

                else
                {
                    ViewBag.Message = "This email is already registerred!";
                    return View(register);
                }
            }
           
            // If we got this far, something failed, redisplay form

        }
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult ChangePassword(ChangePassword change)
        {
            dailysheetcontext context = new dailysheetcontext();

            Account member = null;
            int memberID = Convert.ToInt32(Request.Cookies["dailyCookie"]["EmpId"]);
            string password = change.CurrentPassword;
            Account result = context.Accountset.Where(e => e.EmpId == memberID && e.Password == password).SingleOrDefault();
            if (change.NewPassword != change.ConfirmPassword)
            {
                return View();
            }

            else if (result != null)
            {
                result.Password = change.NewPassword;

                member = accrepo.Update(result);
                if (member != null)
                {
                    return RedirectToAction("LogOut", "UserLogin");
                }
                else
                {
                    ViewBag.Unauthorize = "You can't change password!!";
                }
            }
            else
            {
                ViewBag.Unauthorize = "Your current password is not correct !!";
            }
            return View();
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        //public ActionResult ForgotPassword(ForgotPasswordViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        dailysheetcontext context = new dailysheetcontext();
        //        Account result = context.Accountset.Where(e => e.UserName == model.Email).FirstOrDefault();
        //        if (result == null)
        //        {
        //            return View("ForgotPasswordConfirmation");
        //        }
        //        else
        //        {
        //            string To = result.UserName, UserID, Password, SMTPPort, Host;
        //            CryptLib cl = new CryptLib();
        //            String key = CryptLib.getHashSha256("hrkey", 31); //32 bytes = 256 bit
        //            String em = CryptLib.GenerateRandomIV(16); //16 bytes = 128 bit
        //            string timeout = DateTime.Now.AddMinutes(30).ToString();
        //            string emailtimeout = model.Email + "," + timeout;
        //            try
        //            {
        //                emailtimeout = cl.encrypt(emailtimeout, key, em);
        //            }
        //            catch
        //            {
        //                emailtimeout = "";
        //            }
        //            //Create URL with above token  
        //            var lnkHref = "<a href='" + Url.Action("ResetPassword", "UserLogin", new { param = emailtimeout, IV = em }, "http") + "'>Reset Password</a>";
        //            //var lnkHref = "<a href='"+ Url.Action("ResetPassword", "UserLogin", new { param = emailtimeout, IV = em }) + "'>Reset Password</a>";
        //            //HTML Template for Send email  
        //            string subject = "Your changed password";
        //            string body = "<html><body><b>You can reset your password here. </b><br/>" + lnkHref + "</body></html>";
        //            //Get and set the AppSettings using configuration manager.  
        //            AppSettings(out UserID, out Password, out SMTPPort, out Host);
        //            //Call send email methods.  
        //            SendEmail(UserID, subject, body, To, UserID, Password, SMTPPort, Host);
        //        }
        //    }
        //    ViewBag.sendmessage = "Reset password link has been sent to your email.";
        //    return View();
        //}
        public static void AppSettings(out string UserID, out string Password, out string SMTPPort, out string Host)
        {
            UserID = ConfigurationManager.AppSettings.Get("UserID");
            Password = ConfigurationManager.AppSettings.Get("Password");
            SMTPPort = ConfigurationManager.AppSettings.Get("SMTPPort");
            Host = ConfigurationManager.AppSettings.Get("Host");
        }

        public static void SendEmail(string From, string Subject, string Body, string To, string UserID, string Password, string SMTPPort, string Host)
        {
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(To);
            mail.From = new MailAddress(From);
            mail.IsBodyHtml = true;
            mail.Subject = Subject;
            mail.Body = Body;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = Host;
            smtp.Port = Convert.ToInt16(SMTPPort);
            smtp.Credentials = new NetworkCredential(UserID, Password);
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }
        //public ActionResult ResetPassword(string param = null, string IV = null)
        //{
        //    if (param != null)
        //    {
        //        ResetPasswordViewModel result = new ResetPasswordViewModel();
        //        CryptLib cryptLib = new CryptLib();
        //        string key = CryptLib.getHashSha256("hrkey", 31);
        //        string emailtimeout = ""; string email = ""; string timeout = "";
        //        try
        //        {
        //            emailtimeout = cryptLib.decrypt(param, key, IV);
        //            string[] paramlist = emailtimeout.Split(',');
        //            email = paramlist[0];
        //            timeout = paramlist[1];
        //            DateTime expiredate = Convert.ToDateTime(timeout);
        //            if (expiredate > DateTime.Now)
        //            {
        //                result.Email = email;
        //            }
        //            else
        //            {
        //                return View("Error");
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            return View("Error");
        //        }
        //        return View(result);
        //    }
        //    else
        //    {
        //        return View("Error");
        //    }
        //}
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        //public ActionResult ResetPassword(ResetPasswordViewModel model)
        //{
        //    DailySheetContext context = new DailySheetContext();
        //    Account result = null;
        //    var data = context.AccountSet.Where(e => e.Email == model.Email).FirstOrDefault();
        //    if (model.Password != model.ConfirmPassword)
        //    {
        //        ViewBag.errormsg = "New Password and Confirmed Password are not same!";
        //        return View();
        //    }
        //    else
        //    {
        //        data.Password = model.ConfirmPassword;
        //        result = accrepo.Update(data);
        //    }

        //    if (result != null)
        //    {
        //        return RedirectToAction("Login");
        //    }
        //    else
        //    {
        //        ViewBag.errormsg = "Fail";
        //        return View();
        //    }
        //}

        public ActionResult Error()
        {
            return View();
        }
    }
}
