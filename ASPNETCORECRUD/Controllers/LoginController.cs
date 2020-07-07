using ASPNETCORECRUD.Data;
using ASPNETCORECRUD.Data.Entities;
using ASPNETCORECRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Text;

namespace ASPNETCORECRUD.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserInfoContext _context;

        public LoginController(UserInfoContext context)
        {
            _context = context;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                if (_context.Users.Where(x => x.Name.Equals(loginViewModel.Name)).Count() == 0)
                {
                    ViewBag.UserError = "Username does not exists";

                    return View(loginViewModel);
                }

                string encrypted_pass = Convert.ToBase64String(System.Security.Cryptography.SHA256.Create()
                    .ComputeHash(Encoding.UTF8.GetBytes(loginViewModel.Password)));

                if (_context.Users.Where(x => x.Name.Equals(loginViewModel.Name) && x.Password.Equals(encrypted_pass)).Count() == 0)
                {
                    ViewBag.PasswordError = "You entered an incorrect password";

                    return View(loginViewModel);
                }

                User user = _context.Users.Where(m => m.Name == loginViewModel.Name).FirstOrDefault();

                HttpContext.Session.SetString("Name", user.Name);
                HttpContext.Session.SetInt32("Role", user.isAdmin ? 1 : 0);

                return RedirectToAction("ShowAllUsers", "User");
            }

            return View(loginViewModel);
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login");
        }
    }
}
