using MyMVCDashboard.CustomAuthentication;
using MyMVCDashboard.DataAccess;
using MyMVCDashboard.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MyMVCDashboard.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login(string returnUrl = "")
        {
            if(User.Identity.IsAuthenticated)
            {
               return LogOut();
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpGet]
        public ActionResult LogOut()
        {
            HttpCookie cookie = new HttpCookie("Cookie1", "");
            cookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie);

            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account", null);
        }
        [HttpPost]
        public ActionResult Login(LoginView loginView, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(loginView.UserName, loginView.Password))
                {
                    var user = (CustomMembershipUser)Membership.GetUser(loginView.UserName, true);

                    if (user != null)
                    {
                        CustomSerializeModel userModel = new CustomSerializeModel()
                        {
                            UserId = user.UserId,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            RoleName = user.Roles.Select(r => r.RoleName).ToList()
                        };

                        string userData = JsonConvert.SerializeObject(userModel);

                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, loginView.UserName, DateTime.Now, DateTime.Now.AddMinutes(15), false, userData);

                        string enTicket = FormsAuthentication.Encrypt(authTicket);

                        HttpCookie faCookie = new HttpCookie("Cookie1", enTicket);

                        Response.Cookies.Add(faCookie);

                    }

                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index","Home");
                    }

                }
            }
            ModelState.AddModelError("", "Something is wrong: Username or Password invalid");
            return View(loginView);
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(RegistrationView registraionView)
        {
            var statusRegistration = false;
            var messageRegistration = string.Empty;

            if (ModelState.IsValid)
            {
                string userName = Membership.GetUserNameByEmail(registraionView.Email);
                if (!string.IsNullOrEmpty(userName))
                {
                    ModelState.AddModelError("Warning Email", "Sorry: Email is already exists.");
                    return View(registraionView);
                }

                using (AuthenticationDB adb = new AuthenticationDB())
                {
                    var user = new User()
                    {
                        UserName = registraionView.UserName,
                        FirstName = registraionView.FirstName,
                        LastName = registraionView.FirstName,
                        Email = registraionView.Email,
                        Password = registraionView.Password,
                        ActivationCode = Guid.NewGuid()
                    };

                    registraionView.ActivationCode = user.ActivationCode;

                    adb.Users.Add(user);
                    adb.SaveChanges();
                }

                VerificationEmail(registraionView.Email, registraionView.ActivationCode.ToString());
                messageRegistration = "Your account has been created successfully.";
                statusRegistration = true;

            }
            else
            {
                messageRegistration = "something is wrong";
            }
            ViewBag.Message = messageRegistration;
            ViewBag.Status = statusRegistration;

            return View(registraionView);
        }

        [HttpGet]
        public ActionResult ActivationAccount(string id)
        {
            bool statusAccount = false;
            using (AuthenticationDB adb = new AuthenticationDB())
            {
                var userAccount = adb.Users.Where(u => u.ActivationCode.ToString().Equals(id)).FirstOrDefault();

                if (userAccount != null)
                {
                    userAccount.IsActive = true;
                    adb.SaveChanges();
                    statusAccount = true;
                }
                else
                {
                    ViewBag.Message = "Something is Wrong!";
                }

            }
            ViewBag.Status = statusAccount;
            return View();
        }

        [NonAction]
        private void VerificationEmail(string email, string activationCode)
        {
            var url = string.Format("/Account/ActivationAccount/{0}", activationCode);
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, url);

            var fromMail = new MailAddress("prashant.nalawade21@gmail.com", "ActivationAccount");
            var toMail = new MailAddress(email);

            var fromMailpassword = "mypsmsno1";
            string subject = "Activation of account";

            string body = "<br/> Please click on following link to activate your account" + "<br/><a href='" + link + "'>Activation Account!</a>";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromMail.Address, fromMailpassword)
            };

            using (var message = new MailMessage(fromMail, toMail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);


        }

    }
}