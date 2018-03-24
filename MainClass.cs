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
            List<Item> storeInventory = new List<Item>();

            Store currentStore = new Store(null, 0);

            currentStore.storePrint();

            //Console.WriteLine(currentStore.getName());

            populateInventory(storeInventory, currentStore);

            //foreach( Item shit in storeInventory) {
            //    Console.WriteLine(shit.getName());
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

        private static List<Item> populateInventory(List<Item> storeInventory, Store currentStore)
        {

            //Item test = new Item("TEST", 0);
            //storeInventory.Add(test);

            using (var connection = new SqlConnection("server=wdt2018.australiaeast.cloudapp.azure.com;uid=s3609685;database=s3609685;pwd=abc123"))
            {
                //int storeID = currentStore.getId();


                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "select * from StoreInventory"; //Sets the text for the command

                var itemTable = new DataTable();
                var adapter = new SqlDataAdapter(command); 

                adapter.Fill(itemTable);

                foreach (DataRow row in itemTable.Rows)
                {

                    int dbStoreId= ((int)row["StoreId"]);

                    //Console.WriteLine(dbStoreId);
                    //List<int> test = new List<int>();

                    if (currentStore.getId() == dbStoreId) {
                     //Console.WriteLine("YYYEEEAAAHAHH");
                        int idsInInvetory =((int)row["ProductID"]);
                       

                        Console.WriteLine("Prod ID in inventory is: "+ idsInInvetory);
                        Item addItem = new Item(null, 0);
                        getItem(idsInInvetory, addItem);

                        storeInventory.Add(addItem);

                        foreach(var shit in storeInventory){
                            Console.WriteLine("Returned items"+shit.getName());
                        }
                      //NEED TO MATCH THE PRODUCT ID FROM THE PRODUCT TABLE


                        //Item addItem = new Item(null, 0);

                    }
                }
            }



            return storeInventory;

        }//End of populate method

        public static Item getItem(int item, Item addItem) {
            using (var connection = new SqlConnection("server=wdt2018.australiaeast.cloudapp.azure.com;uid=s3609685;database=s3609685;pwd=abc123"))
            {
                connection.Open();
                //Opens said "object"

                var command = connection.CreateCommand();
                //Creates a command
                command.CommandText = "select * from Product"; //Sets the text for the command

                var table = new DataTable();//Creates a datatable object to store what has been retrieved from the db
                var adapter = new SqlDataAdapter(command); //Creats a new SqlDataAdapter object with the above command

                adapter.Fill(table);//Fills the DataTable (table) obeject with items from the SqlDataAdapter

                Console.WriteLine("ProductID in new method is: " + item);
                Item tempItem = new Item(null, 0);

             
                    

                foreach (var row in table.Select())
                {
                    //Item addItem = new Item(null, 0);

                    int input = ((int)row["ProductID"]);
                    if (item == input) {
                        Console.WriteLine("FOUND");

                        addItem.setName(row["name"].ToString());
                        addItem.setId((int)row["ProductID"]);

            
                }
                    

                }


                connection.Close();

            }

            return addItem;
        }

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