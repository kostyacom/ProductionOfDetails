using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductionOfDetails;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace ProductionOfDetails.Controllers
{
    public class UsersController : Controller
    {
        private readonly Production_of_detalsContext _context;

        public UsersController(Production_of_detalsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        //Post method for registration
        [HttpPost]
        public IActionResult Registration(string name, string email, string password)
        {
            if (_context.Users.Where(u => u.Email.Equals(email)).Count() == 0)
            {
                _context.Users.Add(new Users { Name = name, Email = email, Password = password });
                _context.SaveChanges();
                return View("Login");
            }
            else
            {
                ViewBag.Message = "User with this email exists, use other email or login with correct data!";
                return View("Error");
            }
        }

        //Get method for login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        //Method for user authentificate
        private void Authentificate(Users user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Name == "Denis Trubenok" ? "admin" : "user")
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        //Post method for login
        [HttpPost]
        public IActionResult Login(string login, string password)
        {
            Users user = _context.Users.FirstOrDefault(u => u.Email == login && u.Password == password);
            if (user != null)
            {
                Authentificate(user);
                return RedirectToAction("Index", "Invoices");
            }
            else
            {
                ViewBag.Message = "No thats user. Try again or choose registration!";
                return View("Error");
            }
        }

        //Get method for login out
        [Authorize]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View("Index");
        }
    }
}
