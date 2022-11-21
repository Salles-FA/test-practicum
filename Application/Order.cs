using System.Collections.Generic;

namespace Application
{
    public class Order
    {
        public Order()
        {
            DishesId = new List<int>();
        }
        public List<int> DishesId { get; set; }
    }
}