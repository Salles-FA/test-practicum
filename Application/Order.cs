using System.Collections.Generic;

namespace Application
{
    public class Order
    {
        public Order()
        {
            DishIds = new List<int>();
        }
        public string MealName;
        public List<int> DishIds { get; set; }
    }
}