using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;

namespace Assignment1
{
    public class MainClass
    {
        //ONCE EVERYTHING IS DONE, BREAK THIS UP INTO CLASS LIBRARIES TO USE
      
        static void Main(string[] args)
        {
            //Item currentItem = new Item(null, 0);

            //productPrint(currentItem);
            SqlConnection sqlOp = new SqlConnection("server=wdt2018.australiaeast.cloudapp.azure.com;uid=s3609685;database=s3609685;pwd=abc123");

            mainMenu();           
        }

        public static void connection()
        {
            Console.WriteLine("Connecting to database");

            SqlConnection sqlOp = new SqlConnection("server=wdt2018.australiaeast.cloudapp.azure.com;uid=s3609685;database=s3609685;pwd=abc123");

            sqlOp.Open();

            //DataSet setProducts = new DataSet();
            //SqlDataAdapter adapProducts = new SqlDataAdapter("select * from Product", sqlOp);

            //SqlCommandBuilder cmdBldr = new SqlCommandBuilder(adapProducts);

            //adapProducts.Fill(setProducts, "Product");

            //string updateString = @"
            //update Product
            //set Name = 'Did it work?'
            //where Name = 'Test'";

            var newQuant = 10;
            var selectedID = 7;

            var sqlText = @"UPDATE StoreInventory
                SET StockLevel = @price
                WHERE ProductID = @inventoryID";
            
            SqlCommand dbCommand = new SqlCommand(sqlText, sqlOp);
            dbCommand.Parameters.AddWithValue("price", newQuant);
            dbCommand.Parameters.AddWithValue("inventoryID",selectedID );

            dbCommand.Connection = sqlOp;

            dbCommand.ExecuteNonQuery();

                            //string updateString = @"
                            //         update StoreInventory
                            //         set StockLevel = '10'
                            //         where ProductID = '3'";

                            //// 1. Instantiate a new command with command text only
                            //SqlCommand cmd = new SqlCommand(updateString);

                            //// 2. Set the Connection property
                            //cmd.Connection = sqlOp;

                            //// 3. Call ExecuteNonQuery to send command
                            //cmd.ExecuteNonQuery();
            sqlOp.Close();

        }

        static void mainMenu()  //The main menu method
        {

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

            //Item test = new Item(null,0,0);

            //Store currentStore = new Store(null, 0);

            //currentStore.storeInventory.Add(test);



            //foreach (KeyValuePair<int, int> item in storeInventory)
            //{
            //    Console.WriteLine("Key: {0}, Value: {1}", item.Key, item.Value);
            //}
          

            try
            {
                int choice = 0;
                while (true)
                {
                    //Console.WriteLine("Welcome to Marvelous Magic(Franchise Holder - " + currentStore.getName() +  ")\n ============== ");
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



        static void customerMenu()
        {
            Store currentStore = new Store(null, 0);
            int storeSelect = 0;

            storeSelect= currentStore.storePrint(storeSelect); //THIS WILL SEND BACK A STORE ITEM (NOT INCLUDING STORE INVENTORY)

            Console.WriteLine("Store select is now " + currentStore.getName());

            int prodInput = 0;

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

                            //Uses the Store ID to get the inventory from the DB, then loops over the 
                            //Store objects Item List, adding each item(which loops over the DB inventory to add quantity to each item
                            productPrint(currentStore, prodInput);

                            int passedInput = prodInput;

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

        public static int productPrint(Store currentStore, int prodInput)
        {
            List<int> storeItemsIntID = new List<int>();
            Item test = new Item(null,0,0);
           
            //currentStore.storeInventory.Add(test);
            //foreach(Item shit in currentStore.storeInventory) {
            //    Console.WriteLine("AYAYAYAYAYA");
            //}


            //THIS WILL BE A METHOD (MAYBE IN ITEM???) THAT WILL FILL THE STORE INVENTORY LIST FROM THE DATABASE

            currentStore.getStoreInv(storeItemsIntID);


            //getStoreInv(storeSelect);

            using (var connection = new SqlConnection("server=wdt2018.australiaeast.cloudapp.azure.com;uid=s3609685;database=s3609685;pwd=abc123"))
            //Creates a new SQL connection "object"
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "select * from Product";

                var table = new DataTable();
                var adapter = new SqlDataAdapter(command);

                adapter.Fill(table);

                Console.WriteLine("{0,-10}  {1,-10} {2,-10}", "ProductID", "Product", "Quantity");

                    foreach (var row in table.Select())
                    {
                    foreach (int i in storeItemsIntID)
                        // So this prints the int id. Use it with getID for the currentSoteres item List
                        //foreach(Item j in currentStore.storeInventory)



                        {
                        if (i == (int)row["ProductID"])
                            
                            {
                            int fuck = 0;
                                Console.WriteLine(
                                "{0,-10}  {1,-10} {2,-10}", row["ProductID"], row["Name"], fuck);
                                
                            }
                        foreach (Item shit in currentStore.storeInventory)
                        {
                            Console.WriteLine("YAYAYAYAYAYA");
                            Console.WriteLine(currentStore.storeInventory);
                        }
                        }
                    }
                connection.Close();

            }
            Console.WriteLine("\n[Legend: 'N' Next Page | 'R' Return to Menu\n\nEnter product ID purchase or function:");
            Console.WriteLine();

            try {
                int choice = 0;
                while (choice == 0)
                {
                    string userinput = Console.ReadLine();

                    //validation here
                    int userChoice = 0;
                    if (int.TryParse(userinput, out userChoice)| userinput != "N" | userinput != "R")
                    {
                        choice = userChoice;

                        prodInput = userChoice;


                        //Console.WriteLine("1st Input is " + prodInput);
                        purcharse(prodInput, 0);
                        //return prodInput;
                    
                    }
                    else
                    {
                        Console.WriteLine("Please input number only between 1 and 4");
                        choice = 0;
                    }
                }

            } catch (Exception e) {
                Console.WriteLine("System Exception : " + e.Message);
            }
         

            return prodInput;


        }//End of productPrint

        public static void purcharse(int prodInput, int storeSelect)
        {

            //THIS WILL WRITE BACK TO THE DATABASE WITH WHAT EVER THE ITEM PURCHASED WAS

            int newQuant = prodInput;
            Console.WriteLine("Enter quantity to purchase: ");

                                                                string userinput = Console.ReadLine();
                                                                var selectedID = prodInput;
            var storeInvToAccess = storeSelect;
                                                                //NEED INPUT VALIDATION HERE. TRY/CATCH BLOCK

            //string userinput = Console.ReadLine();

            SqlConnection sqlOp = new SqlConnection("server=wdt2018.australiaeast.cloudapp.azure.com;uid=s3609685;database=s3609685;pwd=abc123");

            sqlOp.Open();

            var sqlText = @"UPDATE StoreInventory
                SET StockLevel = @quantity
                WHERE ProductID = @inventoryID
                AND StoreID = @storeID";
            
            SqlCommand dbCommand = new SqlCommand(sqlText, sqlOp);
            dbCommand.Parameters.AddWithValue("quantity", newQuant);
            dbCommand.Parameters.AddWithValue("inventoryID",selectedID );
            dbCommand.Parameters.AddWithValue("storeID", storeSelect);

            dbCommand.Connection = sqlOp;

            dbCommand.ExecuteNonQuery();

                    

                      


        } //end of purcharse()

        ////
       

        //public static List<int> getStoreInv(List<int> storeItems, int storeSelect) {

        //    var selectedID = storeSelect;

        //    //WRITE TO STORE OBJECT HERE AND PASS IT BACK!!!

        //    using (var connection = new SqlConnection("server=wdt2018.australiaeast.cloudapp.azure.com;uid=s3609685;database=s3609685;pwd=abc123"))
        //    //Creates a new SQL connection "object"
        //    {
        //        connection.Open();

        //        var sqlText = "select * from StoreInventory where StoreID = @find";

        //        SqlCommand dbCommand = new SqlCommand(sqlText, connection);
        //        dbCommand.Parameters.AddWithValue("find", selectedID);

        //        dbCommand.Connection = connection;

        //        dbCommand.ExecuteNonQuery();

        //        var table = new DataTable();
        //        var adapter = new SqlDataAdapter(dbCommand); 

        //        adapter.Fill(table);

        //        foreach (var row in table.Select())
        //        {
        //            //ADD ITEM ID'S TO LIST HERE
        //            int itemInStoreInv = (int)row["ProductID"];
        //            //Console.WriteLine("Passing :" +itemInStoreInv);
        //            storeItems.Add(itemInStoreInv);
        //        }
        //        connection.Close();

        //    }

        //    return storeItems;   
        //}//END OF getStoreInv 
      
        //public static int storePrint(int storeSelect)
        //{
        //    using (var connection = new SqlConnection("server=wdt2018.australiaeast.cloudapp.azure.com;uid=s3609685;database=s3609685;pwd=abc123"))
        //    //Creates a new SQL connection "object"
        //    {
        //        connection.Open();
        //        //Opens said "object"

        //        var command = connection.CreateCommand();
        //        //Creates a command
        //        command.CommandText = "select * from Store"; //Sets the text for the command

        //        var table = new DataTable();//Creates a datatable object to store what has been retrieved from the db
        //        var adapter = new SqlDataAdapter(command); //Creats a new SqlDataAdapter object with the above command

        //        adapter.Fill(table);//Fills the DataTable (table) obeject with items from the SqlDataAdapter

        //        Console.WriteLine("{0,-10}  {1,-10} ", "ID", "Name");

        //        foreach (var row in table.Select())
        //        {

        //            Console.WriteLine(
        //                "{0,-10}  {1,-10} ", row["StoreID"], row["Name"]);
        //        }
        //        Console.WriteLine("Enter the store to use: ");

        //        string userinput = Console.ReadLine();

        //        foreach (DataRow row in table.Rows)
        //        {
        //            string StoreID = row["StoreID"].ToString();

        //            if (userinput == StoreID)
        //            {
        //                storeSelect = ((int)row["StoreId"]);

        //            }

        //        }

        //        connection.Close();

        //    }

        //    return storeSelect;
        //}//End of store print


    } //class



} //namespace