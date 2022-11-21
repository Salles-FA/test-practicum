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
            order.DishesId.Sort();
            foreach (var dishId in order.DishesId)
            {
                AddOrderToDishes(dishId, dishes);
            }
            return dishes;
        }

        /// <summary>
        /// Takes an int, representing an order type, tries to find it in the list.
        /// If the dish type does not exist, add it and set count to 1
        /// If the type exists, check if multiples are allowed and increment that instances count by one
        /// else throw error
        /// </summary>
        /// <param name="dishId">int, represents a dish id</param>
        /// <param name="orderedDishes">a list of dishes, - get appended to or changed </param>
        private static void AddOrderToDishes(int dishId, List<Dish> orderedDishes)
        {
            Dish dish = GetDishByKey(dishId.ToString());
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

        private static Dish GetDishByKey(string dishKey)
        {
            // This data may be loaded from various sources, such as database, API, message-broker, etc...
            Dictionary<string, Dish> existingDishes = new Dictionary<string, Dish>()
            {
                { "1", new Dish { Id = 1, Name = "steak", IsMultipleAllowed = false } },
                { "2", new Dish { Id = 2, Name = "potato", IsMultipleAllowed = true } },
                { "3", new Dish { Id = 2, Name = "wine", IsMultipleAllowed = false } },
                { "4", new Dish { Id = 2, Name = "cake", IsMultipleAllowed = false } },
            };
            return existingDishes.ContainsKey(dishKey)
                        ? existingDishes[dishKey]
                        : throw new ApplicationException("Dish does not exist");
        }
    }
}