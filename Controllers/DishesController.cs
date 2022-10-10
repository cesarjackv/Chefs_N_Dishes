using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Chef_N__Dishes.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDelicious.Controllers;

public class DishesController : Controller
{
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
    private MyContext _context;

    public DishesController(MyContext context)
    {
        _context = context;
    }
    
    [HttpGet("/dishes")]
    public IActionResult AllDishes(){
        // if(!loggedIn){
        //     return RedirectToAction("Index", "Chefs");
        // }

        // Get all Dishes
        List<Dish> All_Dishes = _context.Dishes.Include(pain => pain.ChefName).ToList();
        ViewBag.Pain = _context.Chefs.ToList();
        return View("AllDishes", All_Dishes);
    }

    [HttpGet("/dishes/new")]
    public IActionResult New(){
        if(!loggedIn){
            return RedirectToAction("Index", "Chefs");
        }
        return View();
    }

    [HttpPost("/dishes/new")]
    public IActionResult CreateDish(Dish newDish)
    {
        if(!loggedIn){
            return RedirectToAction("Index", "Chefs");
        }

        if(ModelState.IsValid){
            Chef? AddDish = _context.Chefs.FirstOrDefault(d => d.ChefId == newDish.ChefId);
            // if(AddDish == null){
            //     return View("New");
            // }
            AddDish.ChefDishes.Add(newDish);


            // We can take the Dish object created from a form submission
            // And pass this object to the .Add() method
            _context.Dishes.Add(newDish);
            // OR _context.Dishes.Add(newDish);
            _context.SaveChanges();
            // Other code
            return View("AllDishes");
        }
        //return RedirectToAction("New");
        return View("New");
    }

    // [HttpGet("/dishes/{DishId}")]
    // public IActionResult One(int DishId)
    // {
    //     if(!loggedIn){
    //         return RedirectToAction("Index", "Chefs");
    //     }
        
    //     Dish? oneDish = _context.Dishes.FirstOrDefault(dish => dish.DishId == DishId);
    //     // Other code
    //     if(oneDish == null){
    //         return View("AllDishes");
    //     }
    //     return View("One", oneDish);
    // }

    // [HttpGet("/dishes/{DishId}/edit")]
    // public IActionResult Update(int DishId){
    //     if(!loggedIn){
    //         return RedirectToAction("Index", "Chefs");
    //     }

    //     Dish? editDish = _context.Dishes.FirstOrDefault(dish => dish.DishId == DishId);
    //     // Other code
    //     if(editDish == null){
    //         return View("AllDishes");
    //     }
    //     return View("Update", editDish);
    // }

    // [HttpPost("/dishes/{DishId}/edit")]
    // public IActionResult UpdateDish(int DishId, Dish editedDish)
    // {
    //     if(!loggedIn){
    //         return RedirectToAction("Index", "Chefs");
    //     }

    //     if(ModelState.IsValid){
    //         // We must first Query for a single Dish from our Context object to track changes.
    //         Dish RetrievedDish = _context.Dishes
    //             .FirstOrDefault(Dish => Dish.DishId == DishId);

    //         // Then we may modify properties of this tracked model object
    //         RetrievedDish.ChefName = editedDish.ChefName;
    //         RetrievedDish.DishName = editedDish.DishName;
    //         RetrievedDish.Calories = editedDish.Calories;
    //         RetrievedDish.Tastiness = editedDish.Tastiness;
    //         //RetrievedDish.Description = editedDish.Description;
    //         RetrievedDish.UpdatedAt = DateTime.Now;
            
    //         // Finally, .SaveChanges() will update the DB with these new values
    //         _context.SaveChanges();
            
    //         // Other code
    //         return RedirectToAction("AllDishes");
    //     }
    //     return View("Update", DishId);
    // }

    // [HttpGet("delete/{DishId}")]
    // public IActionResult DeleteDish(int DishId)
    // {
    //     if(!loggedIn){
    //         return RedirectToAction("Index", "Chefs");
    //     }

    //     // Like Update, we will need to query for a single Dish from our Context object
    //     Dish RetrievedDish = _context.Dishes
    //         .SingleOrDefault(Dish => Dish.DishId == DishId);
        
    //     // Then pass the object we queried for to .Remove() on Dishs
    //     _context.Dishes.Remove(RetrievedDish);
        
    //     // Finally, .SaveChanges() will remove the corresponding row representing this Dish from DB 
    //     _context.SaveChanges();
    //     // Other code
    //     return RedirectToAction("AllDishes");
    // }
}