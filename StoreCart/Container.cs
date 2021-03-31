using System;
using System.Collections.Generic;

namespace StoreCart
{
    /// <summary>
    /// This class contains all the information about containers.
    /// </summary>
    public class Container
    {
        Random rand = new Random();
        public Container()
        {
            MaxWeigth = rand.Next(50, 1000);
        }
        
        private Box[] _boxes;
        public Box[] Boxes
        {
            get => _boxes;
            set => _boxes = value;
        }
        public int MaxWeigth { get; private set; }
    }
}