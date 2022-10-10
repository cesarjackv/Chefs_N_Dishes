using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Chef_N__Dishes.Models;

namespace Chef_N__Dishes.Controllers;

public class HomeController : Controller
{
    private MyContext _context;
    private readonly ILogger<HomeController> _logger;

    private int? uid
    {
        get{
            return HttpContext.Session.GetInt32("UUID");
        }
    }

    private bool loggedIn
    {
        get{
            return uid != null;
        }
    }

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("/success")]
    public IActionResult Success()
    {
        if(!loggedIn){
            return RedirectToAction("Index", "Chefs");
        }
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
