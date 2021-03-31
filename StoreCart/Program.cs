using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace StoreCart
{
    class Program
    {
        /// <summary>
        /// This is the main method of the Program and is what controls all the processes in this project.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //The singleton initiation of the warehouse.
            var warehouse = Warehouse.GetInstance();
            do
            {
                int size;
                double tax;
                Console.WriteLine("Welcome to the market!");
                string input;
                
                //Check for preferred work method.
                do
                {
                    Console.Write("\nWould you like to work with \"console\" or \"files\"? ");
                    input = Console.ReadLine();
                } while (input.ToLower() != "console" && input.ToLower() != "files");

                if (input == "files") WorkWithFiles();
                else
                {
                    do
                    {
                        Console.Write("Enter the number of containers of your warehouse: ");
                    } while (!int.TryParse(Console.ReadLine(), out size) | size <= 0);

                    do
                    {
                        Console.Write("Enter the tax per container of your warehouse: ");
                    } while (!double.TryParse(Console.ReadLine(), out tax));

                    //Maximum number of containers in warehouse and their tax.
                    warehouse.MaxCont = size;
                    warehouse.ContTax = tax;

                    //Initiation of the containers array.
                    warehouse.Containers = new Container[size];
                    
                    //Commands handling.
                    string command = " ";
                    int contIndex = 0;
                    Functions.Help();
                    do
                    {
                        Console.Write("Enter command: ");
                        command = Console.ReadLine();
                        HandleCommands(command, ref contIndex);
                    } while (command.ToLower() != "exit");
                }

                //Farewell with user and restart.
                Console.WriteLine(
                    "Thank you for using our services. If you have any suggestions, please shoot us an email at example@thebestcompany.com");
                Console.Write("To exit the program, press Esc. To restart the program, press any other key...");
            } while (Console.ReadKey().Key != ConsoleKey.Escape);
        }
        
        /// <summary>
        /// This method points to the corresponding locations of the functions and handles some exceptions.
        /// </summary>
        /// <param name="cmd">The command that has been entered.</param>
        /// <param name="index">The index of the Container in the containers array.</param>
        static void HandleCommands(string cmd, ref int index)
        {
            var warehouse = Warehouse.GetInstance();
            switch (cmd)
            {
                case "add":
                    if (index < warehouse.Containers.Length)
                    {
                        if (Functions.Add() != null)
                        {
                            warehouse.Containers[index] = Functions.Add();
                            Console.WriteLine("You have successfully added a container!");
                            index++;
                        }
                        else
                            Console.WriteLine(
                                "This container is not valuable enough to be stored in your warehouse!");
                    }
                    else Console.WriteLine("All the space in the warehouse is taken.");

                    break;
                case "rem":
                    Functions.Remove();
                    index--;
                    break;
                case "help":
                    Console.Clear();
                    Functions.Help();
                    break;
                case "show":
                    Console.Clear();
                    Functions.Show();
                    break;
                case "exit":
                    break;
                default:
                    Console.WriteLine("This command doesn't exist.");
                    break;
            }
        }
        
        /// <summary>
        /// To be honest, I did not have time to finish that.
        /// This method implements the functionality for working with files.
        /// </summary>
        static void WorkWithFiles()
        {
            var warehouse = Warehouse.GetInstance();
            
            //I really wish I had more time :(.
            string input;
            Console.WriteLine("You chose to work with files. Please save your files in \n" +
                              "\\bin\\debug and rename them to \"warehouse.txt\", \"actions.txt\", \"containers.txt\"" +
                              " before proceeding any further.");
            Console.WriteLine("\n\"warehouse.txt\" should contain the number of containers in the first line,\n" +
                              "the price per container on the second line.");
            Console.WriteLine("\n\"containers.txt\" should contain the number of containers in the first line,\n" +
                              "two array of elements for each container, the first array contains the weight of" +
                              "each box and the second contains the price per kilo for the corresponding box.");
            do
            {
                Console.Write("Did you do that? (Y/N)");
                input = Console.ReadLine();
            } while (input.ToLower() != "y" && input.ToLower() != "n");
            if(input.ToLower()=="n") Console.WriteLine("Please follow the instructions and start again.");
            else
            {
                //Read the lines from the file.
                string[] linesWarehouse = new string[File.ReadAllLines("warehouse.txt").Length];
                string[] linesActions = new string[File.ReadAllLines("actions.txt").Length];
                string[] linesContainers = new string[File.ReadAllLines("containers.txt").Length];

                try
                {
                    linesWarehouse = File.ReadAllLines("warehouse.txt");
                    linesActions = File.ReadAllLines("actions.txt");
                    linesContainers = File.ReadAllLines("containers.txt");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                if (int.TryParse(linesWarehouse[0], out int size) &&
                    int.TryParse(linesWarehouse[1], out int tax))
                {
                    //Maximum number of containers in warehouse and their tax.
                    warehouse.MaxCont = size;
                    warehouse.ContTax = tax;
                    
                    //Initiation of the containers array.
                    warehouse.Containers = new Container[size];
                }else Console.WriteLine("\"warehouse.txt\" could not be parsed. Please check teh file again.");

                int i = 0;
                foreach (string line in linesActions)
                {
                    if(line.Split(" ").Length==1)
                        HandleFileCommands(line.Trim(), ref i);
                    else
                    {
                        Console.WriteLine("Wrong commands in \"actions.txt\"");
                        return;
                    }
                }
                
                
                
                
                Console.WriteLine("Unfortunately, I had no time to complete the files part of this assignment.");
            }

        }
        
        /// <summary>
        /// This method points to the corresponding locations of the functions and handles some exceptions for file input.
        /// </summary>
        /// <param name="cmd">The command that has been entered.</param>
        /// <param name="index">The index of the Container in the containers array.</param>
        static void HandleFileCommands(string cmd, ref int index)
        {
            var warehouse = Warehouse.GetInstance();
            switch (cmd)
            {
                case "add":
                    if (index < warehouse.Containers.Length)
                    {
                        if (Functions.AddToFile("containers.txt", index) != null)
                        {
                            warehouse.Containers[index] = Functions.AddToFile("containers.txt", index);
                            index++;
                        }
                        else
                            Console.WriteLine(
                                "This container is not valuable enough to be stored in your warehouse!");
                    }
                    else Console.WriteLine("All the space in the warehouse is taken.");

                    break;
                case "rem":
                    Functions.Remove();
                    index--;
                    break;
                case "help":
                    Console.Clear();
                    Functions.Help();
                    break;
                case "show":
                    Console.Clear();
                    Functions.Show();
                    break;
                case "exit":
                    break;
                default:
                    Console.WriteLine("This command doesn't exist.");
                    break;
            }
        }
    }
}