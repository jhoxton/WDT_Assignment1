using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Assignment1
{
    public class Customer
    {
        public static void customerMenu()
        {
            Store currentStore = new Store(null, 0); //Empty store object
            int prodInput = 0;
            int storeSelect = 0;

            storeSelect = currentStore.storePrint(storeSelect); //Selects store from database, via StoreID . Writes store info to local store object
            try
            {
                int choice = 0;
                while (true)
                {
                    Console.WriteLine("Welcome to Marvelous Magic(Retail - " + currentStore.getName() + ")\n ============== ");
                    Console.WriteLine("1. Display Products");
                    Console.WriteLine("2. Return to main menu");

                    string userinput = Console.ReadLine();

                    int userChoice = 0;
                    if (int.TryParse(userinput, out userChoice))
                    {
                        choice = userChoice;
                    }
                    else
                    {
                        Console.WriteLine("Please input number 1 or 2");
                        choice = 0;
                    }

                    switch (choice)
                    {
                        case 1:
                            choice = 1;
                            customerProductPrint(prodInput, currentStore); 
                            //Populates the local stores List of items (localStoreInventory)
                            //Prints the available items then returns the users selected items and quantity for purchase
                            return;
                        case 2:
                            return;
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

        public static void customerProductPrint(int prodInput, Store currentStore)
        {
            List<int> storeItemsIntID = new List<int>();//List to locally store items selected stores inventory, via ItemID
            currentStore.getStoreInv(storeItemsIntID); 
            //Accesses the StoreInventory db to get items and qunatity in store         
            // Then creates new items (id, name and quantity) and adds them to the local store objects inventory

            Console.WriteLine("{0,-5}  {1,-22} {2,-30}", "ID", "Product", "Current Stock");

            const int pageSize = 3;
            var pageOffset = 0;
            try
            {
                int choice = 0;
                while (choice == 0)
                {
                    int userChoice = 0;
                    foreach (Item i in currentStore.localStoreInventory.Skip(pageOffset).Take(pageSize).ToList())
                    {
                        Console.WriteLine("{0,-5}  {1,-22} {2,-30}", i.getId(), i.getName(), i.getStock());
                    }
                    Console.WriteLine("\nLegend: 'N' Next Page | 'R' Return to Menu\n\nEnter product ID purchase or function:");
                    Console.WriteLine();

                    var input = Console.ReadLine();

                    if (input.ToUpper() == "N") //Copied directly from Matthews example in tute
                    {
                        pageOffset += pageSize;
                        if (pageOffset >= currentStore.localStoreInventory.Count)
                        {
                            pageOffset = 0;
                        }
                    }
                    else if (input.ToUpper() == "R")
                    {
                        customerMenu();
                    }

                    else if (int.TryParse(input, out userChoice))
                    {
                        int storeSelect = currentStore.getId();
                        choice = userChoice;
                        prodInput = choice;
                        purcharse(prodInput, storeSelect, currentStore);
                        MainClass.mainMenu();//After purchasing an item, user is sent back to the main menu
                    }
                    else
                    {
                        Console.WriteLine("Please input a product ID or command");
                        choice = 0;
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("System Exception : " + e.Message);
            }

        }//End of productPrint

        public static void purcharse(int prodInput, int storeSelect, Store currentStore) //Writes the items purchased back to database 
        {
            bool canBuy = false;

            string name = null;
            int oldQuant = 0;
            Console.WriteLine("Enter quantity to purchase: ");

            int newQuant = Convert.ToInt32(Console.ReadLine());

            foreach (Item purchasedItem in currentStore.localStoreInventory)
            {
                if (purchasedItem.getId() == prodInput)
                {
                    name = purchasedItem.getName();
                    oldQuant = purchasedItem.getStock();
                    canBuy = true;
                }
            }

            if (newQuant > oldQuant)
            {
                Console.WriteLine("{0} doesn't have enough stock to fulfil the purchase", name);
                canBuy = false;
                return;
            }
            if (canBuy == true)
            {
                using (var sqlOp = new SqlConnection("server=wdt2018.australiaeast.cloudapp.azure.com;uid=s3609685;database=s3609685;pwd=abc123"))

                {
                    sqlOp.Open();
                    var selectedProduct = prodInput;
                    var storeInvToAccess = storeSelect;

                    //Gets the current stock from the Store inventory and subtracts the StockRequest
                    SqlCommand OwnerUpdateInv = new SqlCommand();
                    OwnerUpdateInv.CommandType = CommandType.Text;

                    OwnerUpdateInv.Parameters.Add("prodID", SqlDbType.VarChar).Value = selectedProduct;
                    OwnerUpdateInv.Parameters.Add("storeID", SqlDbType.VarChar).Value = storeInvToAccess;

                    OwnerUpdateInv.CommandText = "SELECT StockLevel FROM StoreInventory WHERE ProductID = @prodID AND StoreID = @storeID";
                    OwnerUpdateInv.Connection = sqlOp;
                    Int32 storeQuant = (Int32)OwnerUpdateInv.ExecuteScalar();

                    storeQuant = storeQuant - newQuant;

                    var sqlText = @"UPDATE StoreInventory
                                   SET StockLevel = @quantity
                                   WHERE ProductID = @inventoryID
                                   AND StoreID = @storeID";

                    SqlCommand dbCommand = new SqlCommand(sqlText, sqlOp);
                    dbCommand.Parameters.AddWithValue("quantity", storeQuant);
                    dbCommand.Parameters.AddWithValue("inventoryID", selectedProduct);
                    dbCommand.Parameters.AddWithValue("storeID", storeSelect);

                    dbCommand.Connection = sqlOp;

                    dbCommand.ExecuteNonQuery();

                    sqlOp.Close();
                    Console.WriteLine("Purchased {0} of {1}", newQuant, name);
                    Console.WriteLine("\n======================\n");
                    return;
                }
            }
            else if (canBuy == false)
            {
                customerMenu();
            }
        } //end of purcharse()

        //

        public static List<int> getStoreInv(List<int> storeItems, int storeSelect)
        {
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
                    int itemInStoreInv = (int)row["ProductID"];
                    storeItems.Add(itemInStoreInv);
                }
                connection.Close();
            }

            return storeItems;
        }//END OF getStoreInv 

        public static int storePrint(int storeSelect)
        {
            using (var connection = new SqlConnection("server=wdt2018.australiaeast.cloudapp.azure.com;uid=s3609685;database=s3609685;pwd=abc123"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "select * from Store"; 

                var table = new DataTable();//Creates a datatable object to store what has been retrieved from the db
                var adapter = new SqlDataAdapter(command); //Creats a new SqlDataAdapter object with the above command

                adapter.Fill(table);//Fills the DataTable (table) obeject with items from the SqlDataAdapter

                Console.WriteLine("{0,-10}  {1,-10} ", "ID", "Name");

                foreach (var row in table.Select())//Loops over the table like a List to print
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
    }
}
