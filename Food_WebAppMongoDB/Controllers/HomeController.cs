using Food_WebAppMongoDB.Entities;
using Food_WebAppMongoDB.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Diagnostics;

namespace Food_WebAppMongoDB.Controllers
{
    public class HomeController : Controller
    {
        
        //Mongo sunucuya bağlanma
        MongoClient client;

        //Mongodaki veri tabanına bağlanma
        IMongoDatabase database;

        //Veri tabanındaki koleksiyona bağlanma
        IMongoCollection<Foods> foodCollection;


        public HomeController()
        {
            client =new MongoClient("mongodb://localhost:27017");
            database = client.GetDatabase("FoodDB");
            foodCollection = database.GetCollection<Foods>("Foods");

        }

        public IActionResult Index()
        {
           return View(foodCollection.AsQueryable().ToList());
        }

        public IActionResult Create()
        {
            
            return View();
        }


        [HttpPost]

        public IActionResult Create(FoodCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                Foods foods = new Foods();
                foods.Id=ObjectId.GenerateNewId();
                foods.FoodName = model.FoodName;
                foods.Company=model.Company;
                foods.Category=model.Category;
                foods.Price=model.Price;

                foodCollection.InsertOne(foods);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }


        public IActionResult Edit(string id)
        {
            ObjectId objectId = ObjectId.Parse(id);
            Foods foods = foodCollection.AsQueryable().FirstOrDefault(x => x.Id == objectId);

            FoodEditViewModel model = new FoodEditViewModel
            {
                FoodName=foods.FoodName,
                Company=foods.Company,
                Category=foods.Category,
                Price=foods.Price,
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(FoodEditViewModel model,string id)
        {
            if (ModelState.IsValid)
            {
                ObjectId objectId = ObjectId.Parse(id);
                Foods foods = foodCollection.AsQueryable().FirstOrDefault(x => x.Id == objectId);

                foods.FoodName=model.FoodName;
                foods.Company= model.Company;
                foods.Category = model.Category;
                foods.Price=model.Price;

                foodCollection.ReplaceOne(x => x.Id == objectId, foods);

                return RedirectToAction(nameof(Index));

            }
            return View(model);
        }

        public IActionResult Delete(string id)
        {
            ObjectId objectId = ObjectId.Parse(id);

            foodCollection.DeleteOne(x=>x.Id == objectId);

            return RedirectToAction(nameof(Index));
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
}