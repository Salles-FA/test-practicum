using System;
using System.Collections.Generic;
using System.Linq;

namespace Application
{
    public class DishManager : IDishManager
    {
        /// <summary>
        /// Takes an Order object, sorts the orders and builds a list of dishes to be returned. 
        /// </summary>
        /// <param name="order"></param>
        /// <returns>Return a list of dishes</returns>
        public List<Dish> GetDishes(Order order)
        {
            var dishes = new List<Dish>();
            order.DishIds.Sort();
            foreach (var dishId in order.DishIds)
            {
                AddOrderToDishes(order.MealName, dishId, dishes);
            }
            return dishes;
        }

        /// <summary>
        /// Takes a string and an int, representing an order type, tries to find it in the list.
        /// If the dish does not exist, add it and set count to 1
        /// If the dish exists, check if multiples are allowed and increment that instances count by one
        /// else throw error
        /// </summary>
        /// <param name="mealName">string, represents the meal name</param>
        /// <param name="dishId">int, represents a dish id</param>
        /// <param name="orderedDishes">a list of dishes, - get appended to or changed </param>
        private static void AddOrderToDishes(string mealName, int dishId, List<Dish> orderedDishes)
        {
            Dish dish = GetDishByKey($"{mealName}{dishId}");
            var orderedDish = orderedDishes.SingleOrDefault(x => x.Name == dish.Name);
            if (orderedDish == null)
            {
                dish.Count = 1;
                orderedDishes.Add(dish);
            }
            else if (dish.IsMultipleAllowed)
            {
                orderedDish.Count++;
            }
            else
            {
                throw new ApplicationException($"Multiple {dish.Name}(s) not allowed");
            }
        }

        /// <summary>
        /// Takes a string, representing a dish key, tries to find it in the Dictionary.
        /// If the dish exists return it
        /// If the dish does not exist throw error
        /// </summary>
        /// <param name="dishKey">string, represents the dish key</param>
        private static Dish GetDishByKey(string dishKey)
        {
            // This data may be loaded from various sources, such as database, API, message-broker, etc...
            Dictionary<string, Dish> existingDishes = new Dictionary<string, Dish>()
            {
                { "evening1", new Dish { Id = 1, MealName = "evening", Name = "steak", IsMultipleAllowed = false } },
                { "evening2", new Dish { Id = 2, MealName = "evening", Name = "potato", IsMultipleAllowed = true } },
                { "evening3", new Dish { Id = 3, MealName = "evening", Name = "wine", IsMultipleAllowed = false } },
                { "evening4", new Dish { Id = 4, MealName = "evening", Name = "cake", IsMultipleAllowed = false } },
            };
            return existingDishes.ContainsKey(dishKey)
                        ? existingDishes[dishKey]
                        : throw new ApplicationException("Dish does not exist");
        }
    }
}