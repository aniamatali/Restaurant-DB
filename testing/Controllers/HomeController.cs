using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using ToDoList;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
      [HttpGet("/")]
      public ActionResult Index()
      {
        return View();
      }

      [HttpGet("/Categories")]
      public ActionResult Results2()
      {
        return View("Results",Cuisine.GetAll());
      }

      [HttpPost("/Categories")]
      public ActionResult Results()
      {
        Cuisine newCuisine = new Cuisine (Request.Form["inputCuisine"]);
        newCuisine.Save();
        return View (Cuisine.GetAll());
      }

      [HttpGet("/Categories/{id}")]
      public ActionResult ResultTask(int id)
      {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Cuisine selectedCuisine = Cuisine.Find(id);
        List<Restaurant> cuisineRestaurants = selectedCuisine.GetRestaurants();
        model.Add("cuisine", selectedCuisine);
        model.Add("restaurants", cuisineRestaurants);
        return View(model);

      }

      [HttpGet("/Categories/{id}/tasks/new")]
      public ActionResult TaskForm(int id)
      {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Cuisine selectedCuisine = Cuisine.Find(id);
        List<Restaurant> allRestaurants = selectedCuisine.GetRestaurants();
        model.Add("cuisine", selectedCuisine);
        model.Add("restaurants", allRestaurants);
        return View(model);
      }

      [HttpPost("/Categories/{id}")]
      public ActionResult ResultTask2(int id)
      {
        string restaurantDescription = Request.Form["inputRestaurant"];
        Restaurant newRestaurant = new Restaurant(restaurantDescription,id,(Request.Form["inputHours"]));
        newRestaurant.Save();

        Dictionary<string, object> model = new Dictionary<string, object>();
        Cuisine selectedCuisine = Cuisine.Find(Int32.Parse(Request.Form["cuisine-id"]));
        List<Restaurant> cuisineRestaurants = selectedCuisine.GetRestaurants();
        model.Add("restaurants", cuisineRestaurants);
        model.Add("cuisine", selectedCuisine);


        return View("ResultTask", model);
      }

      [HttpPost("/Tasks/Delete")]
      public ActionResult DeletePage2()
      {
        Restaurant.DeleteAll();
        return View();
      }

      [HttpPost("/Categories/{id}/Delete")]
      public ActionResult DeleteCuisine(int id)
      {
        Cuisine.DeleteCuisine(id);
        Restaurant.DeleteTasks(id);
        return View("DeletePage3");
      }

      [HttpPost("/Categories/Delete")]
      public ActionResult DeletePage()
      {
        Cuisine.DeleteAll();
        return View();
      }

      [HttpGet("/TaskList")]
      public ActionResult AlphaList()
      {
        return View(Restaurant.GetAlphaList());
      }

    }
  }
