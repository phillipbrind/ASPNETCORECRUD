using ASPNETCORECRUD.Data;
using ASPNETCORECRUD.Data.Entities;
using ASPNETCORECRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text;

namespace ASPNETCORECRUD.Controllers
{
    public class UserController : Controller
    {
        private readonly UserInfoContext _context;
        private string[] colors = { "Blue", "Red", "Green" };

        public UserController(UserInfoContext context)
        {
            _context = context;
        }

        [HttpGet("ShowAllUsers")]
        public IActionResult ShowAllUsers()
        {
            ViewBag.SessionRole = HttpContext.Session.GetInt32("Role");

            return View(_context.Users.ToList());
        }

        [HttpGet("AddUser")]
        public IActionResult AddUser()
        {
            ViewBag.Colors = new SelectList(colors);

            return View();
        }

        [HttpPost("AddUser")]
        public IActionResult AddUser(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                user.Name = userViewModel.Name;
                user.Gender = userViewModel.Gender;
                user.Color = userViewModel.Color;
                user.Password = Convert.ToBase64String(System.Security.Cryptography.SHA256.Create()
                    .ComputeHash(Encoding.UTF8.GetBytes(userViewModel.Password)));
                user.isAdmin = userViewModel.isAdmin;

                _context.Users.Add(user);
                _context.SaveChanges();

                return RedirectToAction("AddUser");
            }

            ViewBag.Colors = new SelectList(colors);

            return View(userViewModel);
        }

        public IActionResult UpdateUser(int id)
        {
            ViewBag.Colors = new SelectList(colors);
            User user = _context.Users.Find(id);
            UserViewModel userViewModel = new UserViewModel();
            userViewModel.Id = user.Id;
            userViewModel.Name = user.Name;
            userViewModel.Gender = user.Gender;
            userViewModel.Color = user.Color;
            userViewModel.isAdmin = user.isAdmin;

            return View(userViewModel);
        }

        [HttpPost]
        public IActionResult UpdateUser(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                User user = _context.Users.Find(userViewModel.Id);
                user.Name = userViewModel.Name;
                user.Gender = userViewModel.Gender;
                user.Color = userViewModel.Color;
                user.Password = Convert.ToBase64String(System.Security.Cryptography.SHA256.Create()
                    .ComputeHash(Encoding.UTF8.GetBytes(userViewModel.Password)));
                user.isAdmin = userViewModel.isAdmin;

                _context.Entry(user).State = EntityState.Modified;
                _context.SaveChanges();

                return RedirectToAction("ShowAllUsers");
            }

            ViewBag.Colors = new SelectList(colors);

            return View(userViewModel);
        }

        public ActionResult UserDetails(int id)
        {
            ViewBag.SessionRole = HttpContext.Session.GetInt32("Role");

            return View(_context.Users.Find(id));
        }

        [HttpGet("DeleteUser")]
        public ActionResult DeleteUser(int id)
        {
            return View(_context.Users.Find(id));
        }

        [HttpPost("DeleteUser")]
        public ActionResult DeleteUser(int? id)
        {
            _context.Users.Remove(_context.Users.Find(id));
            _context.SaveChanges();

            return RedirectToAction("ShowAllUsers");
        }
    }
}
