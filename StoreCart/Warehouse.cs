using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace StoreCart
{
    /// <summary>
    /// The Warehouse class follows a Singleton codding pattern in order to ensure only one instance
    /// of the object is ever created.
    /// </summary>
    public class Warehouse
    {
        private static Warehouse _instance;
        private Container[] _containers;
        
        public Container[] Containers
        {
            get => _containers;
            set => _containers = value;
        }
        public int MaxCont { get; set; }
        public double ContTax { get; set; }

        //This is an essential part of the Singleton code pattern.
        private Warehouse()
        {
        }

        //Another very vital part of the Singleton. It assures only one object of this type is ever created.
        public static Warehouse GetInstance()
        {
            return _instance ??= new Warehouse();
        }
    }
}