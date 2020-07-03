using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Registration.Models;
using Microsoft.AspNetCore.Identity;

namespace Registration.Controllers
{
    public class HomeController : Controller
    {
        private MyContext _context {get;set;}
        private PasswordHasher<Reg> regHasher= new PasswordHasher<Reg> ();
        private PasswordHasher<goUser> logHasher= new PasswordHasher<goUser> ();
        public HomeController(MyContext context)
        {
            _context = context;
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("register")]
        public IActionResult Register(Reg MyReg)
        {
            if(ModelState.IsValid)
            {
                if(_context.Regs.Any( r => r.Email == MyReg.Email))
                {
                    ModelState.AddModelError("Email","Email already in use.");
                    return View("Index");
                }
                string hash = regHasher.HashPassword(MyReg, MyReg.Password);
                MyReg.Password = hash;
                _context.Regs.Add(MyReg);
                _context.SaveChanges();
                HttpContext.Session.SetInt32("UserId", MyReg.UserId);
                return Redirect("/home");
            }
            else
            {
                
                return View("Index");
            }
        }
        [HttpPost("login")]
        public IActionResult Login(goUser lu)
        {
            if(ModelState.IsValid)
            {
                Reg userInDb = _context.Regs.FirstOrDefault(u =>u.Email ==lu.LoginEmail);
                if(userInDb ==null)
                {
                    ModelState.AddModelError("Email","Email not found");
                    return View("Index");
                }
                var result =logHasher.VerifyHashedPassword(lu, userInDb.Password, lu.LoginPassword);
                if(result == 0)
                {
                    ModelState.AddModelError("Password","Invalid password");
                    return View("Index");
                }
                HttpContext.Session.SetInt32("UserId", userInDb.UserId);
                return Redirect("/home");
            }
            else
            {
                return View("Index");
            }

        }
        [HttpGet("home")]
        public IActionResult Home()
        {
            int? userID = HttpContext.Session.GetInt32("UserId");
            if(userID == null)
            {
                return Redirect("/");
            }
            return View("Home");
        }
        [HttpGet ("logout")]
        public IActionResult Logout ()
        {
            HttpContext.Session.Clear ();
            return Redirect ("/");
        }
        
    }
}
