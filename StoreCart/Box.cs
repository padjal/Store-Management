using System.Linq.Expressions;

namespace StoreCart
{
    /// <summary>
    /// This class contains all the fields and properties of Boxes.
    /// </summary>
    public class Box
    {
        public double Mass { get; private set; }
        public double PricePerKg { get; set; }

        public Box(double mass, double price)
        {
            Mass = mass;
            PricePerKg = price;
        }
    }
}