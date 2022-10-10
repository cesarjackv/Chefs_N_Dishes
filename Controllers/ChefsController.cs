using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Chef_N__Dishes.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace Chef_N__Dishes.Controllers;

public class ChefsController : Controller
{
    private MyContext _context;

    public ChefsController(MyContext context)
    {
        _context = context;
    }

    [HttpGet("/chefs/new")]
    public IActionResult Index()
    {
        return View("Index");
    }

    [HttpGet("/")]
    public IActionResult AllChefs(){
        // Get all Chefs
        List<Chef> Chefs_Stats = _context.Chefs.Include(dish => dish.ChefDishes).ToList();
        return View("AllChefs", Chefs_Stats);
    }

    [HttpPost("/register")]
    public IActionResult Register(Chef newUser)
    {
        if(ModelState.IsValid){
            if(_context.Chefs.Any(u => u.Email == newUser.Email)){
                ModelState.AddModelError("Email", "is taken");
            }
        }else{
            return Index();
        }

        PasswordHasher<Chef> hashSlingingSlasher = new PasswordHasher<Chef>();
        newUser.Password = hashSlingingSlasher.HashPassword(newUser, newUser.Password);

        _context.Chefs.Add(newUser);
        _context.SaveChanges();

        HttpContext.Session.SetInt32("UUID", newUser.ChefId);
        HttpContext.Session.SetString("FirstName", newUser.FirstName);
        HttpContext.Session.SetString("LastName", newUser.LastName);
        HttpContext.Session.SetString("FullName", $"{newUser.FirstName} {newUser.LastName}");
        return RedirectToAction("AllChefs");
    }

    [HttpPost("/login")]
    public IActionResult Login(LoginUser loginUser)
    {
        if(!ModelState.IsValid){
            return Index();
        }

        Chef? dbUser = _context.Chefs.FirstOrDefault(u => u.Email == loginUser.LoginEmail); 

        if(dbUser == null){
            ModelState.AddModelError("LoginEmail", "invalid.");
            return Index();
        }

        PasswordHasher<LoginUser> hashSlingingSlasher = new PasswordHasher<LoginUser>();
        PasswordVerificationResult pwCompareResult = hashSlingingSlasher.VerifyHashedPassword(loginUser, dbUser.Password, loginUser.LoginPassword);

        
        if(pwCompareResult == 0){
            ModelState.AddModelError("LoginPassword", "invalid.");
            return Index();
        }

        HttpContext.Session.SetInt32("UUID", dbUser.ChefId);
        HttpContext.Session.SetString("FirstName", dbUser.FirstName);
        HttpContext.Session.SetString("LastName", dbUser.LastName);
        HttpContext.Session.SetString("FullName", $"{dbUser.FirstName} {dbUser.LastName}");
        return RedirectToAction("AllChefs");
    }

    [HttpPost("/logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }
}