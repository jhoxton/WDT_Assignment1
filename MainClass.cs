﻿using System;
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
            //Item currentItem = new Item(null, 0);

            //productPrint(currentItem);
            mainMenu();           
        }
        static void mainMenu()  //The main menu method
        {
            //Item test = new Item("Demo Item");

            try
            {
                int choice = 0;
                while (true)
                {
                    Console.WriteLine("Welcome to Marvelous Magic\n==============");
                    Console.WriteLine("1. Owner");
                    Console.WriteLine("2. Franchise Holder");
                    Console.WriteLine("3. Customer");
                    Console.WriteLine("4. Quit");

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
                            ownerMenu();
                            break;
                        case 2:
                            choice = 2;
                            franchiseMenu();
                            break;
                        case 3:
                            choice = 3;
                            customerMenu();
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

        static void ownerMenu()
        {
            try
            {

                int choice = 0;
                while (true)
                {
                    Console.WriteLine("Welcome to Marvelous Magic(Owner)\n==============");
                    Console.WriteLine("1. Display All Stock Requests");
                    Console.WriteLine("2. Display Owner Inventory ");
                    Console.WriteLine("3. Reset Inventory Item Stock");
                    Console.WriteLine("4. Return to main menu");

                    string userinput = Console.ReadLine();

                    //validation here
                    int userChoice = 0;
                    if (int.TryParse(userinput, out userChoice))
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
                            //TODO
                            break;
                        case 2:
                            choice = 2;
                            //TODO
                            break;
                        case 3:
                            choice = 3;
                            //TODO
                            break;
                        case 4:
                            mainMenu();
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



        } // end of ownerMenu

        public static void franchiseMenu()
        //Prompt for Store ID here first
        {
            //List<Item> storeInventory = new List<Item>();

            Dictionary<int, int> storeInventory = new Dictionary<int, int>();


            Store currentStore = new Store(null, 0);

            currentStore.storePrint();

            populateInventory(storeInventory, currentStore);

            //foreach (KeyValuePair<int, int> item in storeInventory)
            //{
            //    Console.WriteLine("Key: {0}, Value: {1}", item.Key, item.Value);
            //}
          

            try
            {
                int choice = 0;
                while (true)
                {
                    Console.WriteLine("Welcome to Marvelous Magic(Franchise Holder - " + currentStore.getName() +  ")\n ============== ");
                    Console.WriteLine("1. Display Inventory");
                    Console.WriteLine("2. Stock Request(Threshold)");
                    Console.WriteLine("3. Add New Inventory Item");
                    Console.WriteLine("4. Return to main menu");

                    string userinput = Console.ReadLine();

                    //validation here
                    int userChoice = 0;
                    if (int.TryParse(userinput, out userChoice))
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
                            //TODO
                            break;
                        case 2:
                            choice = 2;
                            //TODO
                            break;
                        case 3:
                            choice = 3;
                            //productPrint();
                            //TODO
                            break;
                        case 4:
                            mainMenu();
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
        }//end of franchiseMenu

        private static Dictionary<int, int> populateInventory(Dictionary<int, int> storeInventory, Store currentStore)
        {
            //Loop over SQL StoreInventory and get each db.ProdcutID and related db.StockLevel that matches 
            //currentStore's db.StoreID. 
            //Add this the the populateInventory Dicionary

            int searchID = currentStore.getId();
            //Console.WriteLine(searchID + " is searchID");

            using (var connection = new SqlConnection("server=wdt2018.australiaeast.cloudapp.azure.com;uid=s3609685;database=s3609685;pwd=abc123"))
            {
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText = "select * from StoreInventory"; //Sets the text for the command

                var table = new DataTable();
                var adapter = new SqlDataAdapter(command);

                adapter.Fill(table);

                foreach (var row in table.Select())
                {
                    int dbId = ((int)row["StoreId"]);

                    if(searchID == dbId) {
                        //Console.WriteLine("Matched ID for each item is: " + dbId);

                        int prodID = ((int)row["ProductID"]);
                        int stock = ((int)row["StockLevel"]);

                        //Console.WriteLine("ID is " + prodID);
                        //Console.WriteLine("Stock level is " +stock);
                        //command.CommandText = "select * from "
                        storeInventory.Add(prodID, stock);
                    }

                }

            }



            return storeInventory;

        }//End of populate method



        static void customerMenu()
        {
            //Prompt for store ID here
            Item currentItem = new Item(null, 0);

            try
            {
                int choice = 0;
                while (true)
                {
                    Console.WriteLine("Welcome to Marvelous Magic(Retail)\n==============");
                    Console.WriteLine("1. Display Products");
                    Console.WriteLine("4. Return to main menu");

                    string userinput = Console.ReadLine();

                    //validation here
                    int userChoice = 0;
                    if (int.TryParse(userinput, out userChoice))
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
                            //TODO
                            currentItem.productPrint();

                            break;

                        case 4:
                            mainMenu();
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
        }//end of customerMenu


       

    } //class



} //namespace