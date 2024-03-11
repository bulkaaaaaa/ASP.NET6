using Microsoft.AspNetCore.Mvc;
using WebAppMVC.Models;
using System;
using System.Collections.Generic;

namespace WebAppMVC.Controllers
{
    public class PizzaOrderController : Controller
    {
        public IActionResult UserRegister()
        {
            return View();
        }

        [HttpPost]
        public IActionResult OrderConfirm(User user)
        {
            if (user.Age > 16)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult ProcessOrder(int productQuantity)
        {
            var pizzas = new List<Product>();

            for (int i = 0; i < productQuantity; i++)
            {
                var pizza = new Product
                {
                    Name = Request.Form[$"pizzas[{i}].Name"].ToString(),
                    Price = Convert.ToDecimal(Request.Form[$"pizzas[{i}].Price"])
                };
                pizzas.Add(pizza);
            }

            if (pizzas == null || pizzas.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                return View("PizzasDisplay", pizzas.ToArray());
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
