using System;
using System.IO;

namespace StoreCart
{
    /// <summary>
    /// This class contains all the core functionality of this program.
    /// </summary>
    public class Functions
    {
        private static Random rand = new Random();
        private static Warehouse _warehouse = Warehouse.GetInstance();

        /// <summary>
        /// This method handles Adding containers to the warehouse by initiating new Container objects from the user input and adding boxes.
        /// </summary>
        /// <returns>It returns the container that should then be placed in the containers array.</returns>
        public static Container Add()
        {
            var warehouse = Warehouse.GetInstance();
            var newContainer = new Container();
            int n;
            do
            {
                Console.WriteLine("How many boxes would you like to fit in this container?");
            } while (!int.TryParse(Console.ReadLine(), out n));

            newContainer.Boxes = new Box[n];
            
            //This variable keeps track of the monetary value of this container in order to check later if it should
            //be included in the warehouse.
            double sum = 0;

            //Add all the boxes.
            for (int i = 0; i < n; i++)
            {
                double mass;
                double priceKg;
                do
                {
                    Console.Write($"Box[{i}] weight: ");
                } while (!double.TryParse(Console.ReadLine(), out mass));

                do
                {
                    Console.Write($"Box[{i}] price per kilo: ");
                } while (!double.TryParse(Console.ReadLine(), out priceKg));

                newContainer.Boxes[i] = new Box(mass, priceKg);
                sum += mass * priceKg;
            }

            //Check for damage.
            if (sum * (1 - rand.Next(0, 1) / 2) <= warehouse.ContTax)
                newContainer = null;

            return newContainer;
        }

        /// <summary>
        /// This method prints all the useful information to the user when called, like functions of the program,
        /// how to call them, etc.
        /// </summary>
        public static void Help()
        {
            Console.WriteLine("\nType \"add\" to add a new container to the warehouse\n" +
                              "~Type \"rem\" to remove a container from the warehouse.\n" +
                              "~Type \"help\" to display this menu again.\n" +
                              "~Type \"show\" to display all containers in the warehouse.\n" +
                              "~Type \"exit\" to end the warehouse management.\n");
        }

        /// <summary>
        /// This method removes containers from the warehouse.
        /// </summary>
        public static void Remove()
        {
            int i = 0, n;
            foreach (Container container in _warehouse.Containers)
            {
                Console.WriteLine(
                    $"Container #{i,1}| MaxWeight: {container.MaxWeigth,3}| Boxes: {container.Boxes.Length,2}|");
                i++;
            }

            Console.WriteLine("These are all the available containers. Which number would you like to remove?");
            do
            {
                Console.Write("Remove number: ");
            } while (!int.TryParse(Console.ReadLine(), out n));

            //Remove the container.
            _warehouse.Containers[n] = null;

            //This loop shifts all the containers with an index backwards, in order to fill the gap from the
            //removed container.
            for (int j = 0; j < _warehouse.Containers.Length; j++)
            {
                if (_warehouse.Containers[j] == null && j <= _warehouse.Containers.Length)
                    _warehouse.Containers[j] = _warehouse.Containers[j + 1];
            }

            Console.WriteLine($"Container #{n} has been successfully removed.");
        }

        /// <summary>
        /// This method prints to the user all the containers and their corresponding boxes inside.
        /// </summary>
        public static void Show()
        {
            int i = 1;
            foreach (Container cont in _warehouse.Containers)
            {
                if (cont != null)
                {
                    Console.WriteLine($"Container {i}:");

                    int j = 1;
                    if (cont.Boxes.Length != 0)
                    {
                        foreach (Box box in cont.Boxes)
                        {
                            Console.WriteLine($"\tBox{j,3}| Mass: {box.Mass,3}| Price per kilo: {box.PricePerKg,3}|");
                            j++;
                        }
                    }
                }
                else Console.WriteLine($"Container {i} is empty.");

                i++;
            }
        }
        
        /// <summary>
        /// This method handles Adding containers to the warehouse by initiating new Container objects from the files
        /// input and adding boxes.
        /// </summary>
        /// <returns>It returns the container that should then be placed in the containers array.</returns>
        public static Container AddToFile(string path, int i)
        {
           /* var warehouse = Warehouse.GetInstance();
            var newContainer = new Container();
            
            string[] lines = File.ReadAllLines(path);
            foreach (string word in lines[i].Split(" "))
            {
                if (word == "container" && int.= i)
                {
                    n[i] = new Container();
                    n[i].Boxes = new Box[int.Parse(words[2])];
                    int c = 0;
                    for (int j = 3; j < words.Length; j+=2)
                    {
                        n[i].Boxes[c] = new Box(double.Parse(words[j]), 
                            double.Parse(words[j+1]));
                    }

                }
            }
            */
            return new Container();
        }
    }
}