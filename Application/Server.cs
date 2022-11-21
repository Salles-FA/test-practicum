using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Application
{
    public class Server : IServer
    {
        private readonly IDishManager _dishManager;

        public Server(IDishManager dishManager)
        {
            _dishManager = dishManager;
        }

        public string TakeOrder(string unparsedOrder)
        {
            try
            {
                Order parsedOrder = ParseOrder(unparsedOrder);
                List<Dish> dishes = _dishManager.GetDishes(parsedOrder);
                string formatedDishes = FormatOutput(dishes);
                return formatedDishes;
            }
            catch (ApplicationException)
            {
                return "error";
            }
        }


        private static Order ParseOrder(string unparsedOrder)
        {
            var parsedOrder = new Order
            {
                DishIds = new List<int>()
            };

            string[] orderItems = unparsedOrder.Split(',');
            if (orderItems.Count() <= 1)
            {
                throw new ApplicationException("Order needs to be in the format: meal name, dish 1, dish 2, etc...");
            }
            parsedOrder.MealName = Regex.Replace(orderItems[0].ToLower(), @"\s", "");
            orderItems = orderItems.Skip(1).ToArray();

            foreach (var orderItem in orderItems)
            {
                if (int.TryParse(orderItem, out int parsedOrderId))
                {
                    parsedOrder.DishIds.Add(parsedOrderId);
                }
                else
                {
                    throw new ApplicationException("Order dishes need to be comma separated list of numbers");
                }
            }
            return parsedOrder;
        }

        private static string FormatOutput(List<Dish> dishes)
        {
            var returnValue = "";

            foreach (var dish in dishes)
            {
                returnValue += string.Format(",{0}{1}", dish.Name, GetMultiple(dish.Count));
            }

            if (returnValue.StartsWith(","))
            {
                returnValue = returnValue.TrimStart(',');
            }

            return returnValue;
        }

        private static object GetMultiple(int count)
        {
            if (count > 1)
            {
                return string.Format("(x{0})", count);
            }
            return "";
        }
    }
}
