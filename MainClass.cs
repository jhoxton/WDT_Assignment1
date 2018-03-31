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



        public static void customerMenu()
        {
            Store currentStore = new Store(null, 0); //Empty store object
            int prodInput = 0;
            int storeSelect = 0;

            storeSelect= currentStore.storePrint(storeSelect); //Selects store from database, via StoreID . Writes store info to local store object

            //Console.WriteLine("Store select is now " + currentStore.getName());



            try
            {
                int choice = 0;
                while (true)
                {
                    Console.WriteLine("Welcome to Marvelous Magic(Retail)\n==============");
                    Console.WriteLine("1. Display Products");
                    Console.WriteLine("4. Return to main menu");

                    string userinput = Console.ReadLine();

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
 
                            customerProductPrint(prodInput, currentStore); //Populates the local stores List of items (localStoreInventory)
                            //Prints the available items then returns the users selected items and quantity for purchase

                            Console.WriteLine("Prod input back in MENU is" + prodInput);
                            //purcharse(prodInput, storeSelect, currentStore);
                            mainMenu();



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

     
        public static int customerProductPrint(int prodInput, Store currentStore)
        {
            
            List<int> storeItemsIntID = new List<int>();//List to locally store items selected stores inventory, via ItemID

            //TODO Need to validate input to this List to avoid duplicates???

            currentStore.getStoreInv(storeItemsIntID); //Accesses the StoreInventory db to get items and qunatity in store         
            // Then creates new items (id, name and quantity) and adds them to the local store objects inventory

            Console.WriteLine("{0,-5}  {1,-22} {2,-30}", "ID", "Product", "Current Stock");

            //loop over current store items here

            foreach (Item i in currentStore.localStoreInventory) //Test print on current stores stock
            {

                Console.WriteLine("{0,-5}  {1,-22} {2,-30}", i.getId(), i.getName(), i.getStock());

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
                    if (int.TryParse(userinput, out userChoice)| userinput != "N")
                    {
                        int storeSelect = currentStore.getId();
                        choice = userChoice;

                        prodInput = choice;
                        //passID = choice;

                        Console.WriteLine("Prod input in PRODUCTPRINT is " + prodInput);
                        purcharse(prodInput, storeSelect, currentStore);



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
         
            Console.WriteLine("Prod input AT END OF PRODUCTPRINT is " + prodInput);

            return prodInput;


        }//End of productPrint

        public static void purcharse(int prodInput, int storeSelect, Store currentStore) //Writes the items purchased back to database 
        {

            bool canBuy = false;
            Console.WriteLine("Prod input in PURCHASE  is " + prodInput);

            string name =null;
            int oldQuant = 0;
            Console.WriteLine("Enter quantity to purchase: ");
  
            int newQuant = Convert.ToInt32(Console.ReadLine());

            //Console.WriteLine("Store ID is " + storeSelect);
            //Console.WriteLine("Prod input is " + prodInput);
            //Console.WriteLine("Quant is " + newQuant);


            foreach(Item purchasedItem in currentStore.localStoreInventory) {
               
                if(purchasedItem.getId() == prodInput) {
                    name = purchasedItem.getName();
                    oldQuant = purchasedItem.getStock();
                    canBuy = true;
                }
            }

            if(newQuant > oldQuant) {
                Console.WriteLine("{0} doesn't have enough stock to fulfil the purchase", name);
                canBuy = false;



                mainMenu();
            }
            if(canBuy == true) {
                var selectedProduct = prodInput;
                var storeInvToAccess = storeSelect;

                prodInput = 0;
                storeSelect = 0;
                currentStore = null;


                SqlConnection sqlOp = new SqlConnection("server=wdt2018.australiaeast.cloudapp.azure.com;uid=s3609685;database=s3609685;pwd=abc123");

                sqlOp.Open();

                var sqlText = @"UPDATE StoreInventory
                SET StockLevel = @quantity
                WHERE ProductID = @inventoryID
                AND StoreID = @storeID";

                SqlCommand dbCommand = new SqlCommand(sqlText, sqlOp);
                dbCommand.Parameters.AddWithValue("quantity", newQuant);
                dbCommand.Parameters.AddWithValue("inventoryID", selectedProduct);
                dbCommand.Parameters.AddWithValue("storeID", storeSelect);

                dbCommand.Connection = sqlOp;

                dbCommand.ExecuteNonQuery();


            } else {
                mainMenu();
            }
           
           
                      


        } //end of purcharse()

        //

        public static List<int> getStoreInv(List<int> storeItems, int storeSelect) {
            
            var selectedID = storeSelect;

            using (var connection = new SqlConnection("server=wdt2018.australiaeast.cloudapp.azure.com;uid=s3609685;database=s3609685;pwd=abc123"))
            //Creates a new SQL connection "object"
            {
                connection.Open();

                var sqlText = "select * from StoreInventory where StoreID = @find";

                SqlCommand dbCommand = new SqlCommand(sqlText, connection);
                dbCommand.Parameters.AddWithValue("find", selectedID);

                dbCommand.Connection = connection;

                dbCommand.ExecuteNonQuery();

                var table = new DataTable();
                var adapter = new SqlDataAdapter(dbCommand); 

                adapter.Fill(table);

                foreach (var row in table.Select())
                {
                    //ADD ITEM ID'S TO LIST HERE
                    int itemInStoreInv = (int)row["ProductID"];
                    //Console.WriteLine("Passing :" +itemInStoreInv);
                    storeItems.Add(itemInStoreInv);
                }
                connection.Close();

            }

            return storeItems;   
        }//END OF getStoreInv 
      
        public static int storePrint(int storeSelect)
        {
            
            using (var connection = new SqlConnection("server=wdt2018.australiaeast.cloudapp.azure.com;uid=s3609685;database=s3609685;pwd=abc123"))
            //Creates a new SQL connection "object"
            {
                connection.Open();
                //Opens said "object"

                var command = connection.CreateCommand();
                //Creates a command
                command.CommandText = "select * from Store"; //Sets the text for the command

                var table = new DataTable();//Creates a datatable object to store what has been retrieved from the db
                var adapter = new SqlDataAdapter(command); //Creats a new SqlDataAdapter object with the above command

                adapter.Fill(table);//Fills the DataTable (table) obeject with items from the SqlDataAdapter

                Console.WriteLine("{0,-10}  {1,-10} ", "ID", "Name");

                foreach (var row in table.Select())
                {

                    Console.WriteLine(
                        "{0,-10}  {1,-10} ", row["StoreID"], row["Name"]);
                }
                Console.WriteLine("Enter the store to use: ");

                string userinput = Console.ReadLine();

                foreach (DataRow row in table.Rows)
                {
                    string StoreID = row["StoreID"].ToString();

                    if (userinput == StoreID)
                    {
                        storeSelect = ((int)row["StoreId"]);

                    }

                }

                connection.Close();

            }

            return storeSelect;
        }//End of store print


        public int validateRange(int target, int range) {
            
            return target;
        }

    } //class



} //namespace