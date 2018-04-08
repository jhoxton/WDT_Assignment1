using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;

namespace Assignment1
{
    public class MainClass
    {
        static void Main(string[] args)
        {
            mainMenu();
        }

        public static void mainMenu()  //The main menu method
        {
            try
            {
                int choice = 0;
                while (true)
                {
                    Console.WriteLine("==============\nWelcome to Marvelous Magic\n==============");
                    Console.WriteLine("1. Owner");
                    Console.WriteLine("2. Franchise Holder");
                    Console.WriteLine("3. Customer");
                    Console.WriteLine("4. Quit\n");

                    string userinput = Console.ReadLine();

                    //User input validation
                    int userChoice = 0;
                    if (int.TryParse(userinput, out userChoice))//Turns user inout from a string to an int
                    {
                        choice = userChoice;
                    }
                    else
                    {
                        Console.WriteLine("Please input number only between 1 and 4");
                        choice = 0;
                    }

                    switch (choice)
                    {
                        case 1:
                            choice = 1;
                            Owner.ownerMenu();
                            break;
                        case 2:
                            choice = 2;
                            Franchise.franchiseMenu();
                            break;
                        case 3:
                            choice = 3;
                            Customer.customerMenu();
                            break;
                        case 4:
                            Environment.Exit(0);
                            break;
                        default:
                            break;
                    } //End Switch

                } // end of while

            }
            catch (Exception e)
            {
                Console.WriteLine("System Exception : " + e.Message);
            }
        } // end of mainMenu

    } //class

} //namespace