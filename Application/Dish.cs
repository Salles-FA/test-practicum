namespace Application
{
    /// <summary>
    /// Contains a dish by name and number of times the dish has been ordered
    /// </summary>
    public class Dish
    {
        public int Id { get; set; }
        public string MealName { get; set; }
        public string Name { get; set; }
        public int Count { get; set; } = 0;
        public bool IsMultipleAllowed { get; set; }
    }
}