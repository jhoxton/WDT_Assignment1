using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Assignment1
{
    public class Owner
    {

        public static void ownerMenu()
        {
            Store ownerInventoryStore = new Store(null, 0); //Empty store object

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
                            displayStockRequests(ownerInventoryStore);
                            break;
                        case 2:
                            choice = 2;
                            ownerProductPrint(ownerInventoryStore);
                            break;
                        case 3:
                            choice = 3;
                            resetItemIDSelect(ownerInventoryStore);
                            break;
                        case 4:
                            return;

                        default:
                            break;
                    } //switch
                } // while
            }
            catch (Exception e)
            {
                Console.WriteLine("System Exception : " + e.Message);
            }

        } // end of ownerMenu

        //public static string verifyItemName(){

        //}

        public static void resetItemStock(int resetID, string resetName) //
        {
            //Console.WriteLine("ID IS " + resetID);

            /*
             * NEED TO GET THE ITEM NAME HERE SOMEHOW
             */

            using (var sqlOp = new SqlConnection("server=wdt2018.australiaeast.cloudapp.azure.com;uid=s3609685;database=s3609685;pwd=abc123"))

            {//Updates the Stock item in the db
                var selectedProduct = resetID;
                var setToTwenty = 20;

                sqlOp.Open();

                var sqlText = @"UPDATE OwnerInventory
                SET StockLevel = @quantity
                WHERE ProductID = @inventoryID";

                SqlCommand dbCommand = new SqlCommand(sqlText, sqlOp);
                dbCommand.Parameters.AddWithValue("quantity", setToTwenty);
                dbCommand.Parameters.AddWithValue("inventoryID", selectedProduct);


                dbCommand.Connection = sqlOp;

                dbCommand.ExecuteNonQuery();

                //The below is a hacky way of getting the item name, since the local store inventory has been cleared
                var command = sqlOp.CreateCommand();
                //Creates a command
                command.CommandText = "select * from Product"; //Sets the text for the command

                var table = new DataTable();//Creates a datatable object to store what has been retrieved from the db
                var adapter = new SqlDataAdapter(command); //Creats a new SqlDataAdapter object with the above command

                adapter.Fill(table);//Fills the DataTable (table) obeject with items from the SqlDataAdapter

                foreach (var row in table.Select())
                {
                    int productID = (int)row["ProductID"];

                    if (selectedProduct == productID)
                    {
                        resetName = row["Name"].ToString();
                    }
                }


                sqlOp.Close();


                Console.WriteLine("{0} stock reset to 20", resetName);
                Console.WriteLine("\n======================\n");
                sqlOp.Close();
                return;

            }









        } //End of resetItemStock

        public static void resetItemIDSelect(Store ownerInventoryStore)
        { //Gets an item ID to reset in the database

            ownerProductPrint(ownerInventoryStore);

            Console.WriteLine("Enter ID to reset:\n");
            try
            {

                int choice = 0;
                //while (true)
                //{

                string userinput = Console.ReadLine();

                //validation here
                int userChoice = 0;
                string resetName = null;
                if (int.TryParse(userinput, out userChoice))
                {
                    choice = userChoice;



                    foreach (Item nameLoop in ownerInventoryStore.localStoreInventory)
                    {


                        if (nameLoop.getId() == choice)
                        {
                            resetName = nameLoop.getName();

                        }

                    }
                    resetItemStock(choice, resetName);


                }
                else
                {
                    Console.WriteLine("Please input an item's ID to rest");
                    choice = 0;
                }

                //} 


            }
            catch (Exception e)
            {
                Console.WriteLine("System Exception : " + e.Message);
            }
        }//end of resetItemIDSelect

        public static void ownerProductPrint(Store ownerInventoryStore)
        {
            List<int> ownerItemsIntID = new List<int>();//List to locally store items selected stores inventory, via ItemID

            //TODO Need to validate input to this List to avoid duplicates???
            Console.WriteLine("Owner Inventory\n");

            ownerInventoryStore.getOwnerInv(ownerItemsIntID); //Accesses the OwnerInventory db to get items and qunatity in store         
            // Then creates new items (id, name and quantity) and adds them to the local store objects inventory

            Console.WriteLine("{0,-5}  {1,-22} {2,-30}", "ID", "Product", "Current Stock");

            foreach (Item i in ownerInventoryStore.localStoreInventory) //Test print on current stores stock
            {
                Console.WriteLine("{0,-5}  {1,-22} {2,-30}", i.getId(), i.getName(), i.getStock());
            }

            Console.WriteLine();

            ownerItemsIntID.Clear();
            ownerInventoryStore.localStoreInventory.Clear();
            /*
             * CHECK FOR EFFECTS HERE. THIS CLEAR FUNCTION MAY BE A BIT TOO NUCLEAR
             * 
             */

            //Console.WriteLine("Prod input AT END OF PRODUCTPRINT is " + prodInput);
        }//End of productPrint

        public static void displayStockRequests(Store ownerInventoryStore)
        { //Loops & prints all stock requests from the db, prompts the owner to process one. 
            List<int> ownerItemsIntID = new List<int>();//List to locally store items selected stores inventory, via ItemID



            List<StockRequest> requestList = new List<StockRequest>(); //List to store then print all current stock requests locally


            ownerInventoryStore.getOwnerInv(ownerItemsIntID);

            using (var connection = new SqlConnection("server=wdt2018.australiaeast.cloudapp.azure.com;uid=s3609685;database=s3609685;pwd=abc123"))

            {
                connection.Open();
                //Opens said "object"

                var command = connection.CreateCommand();
                //Creates a command
                command.CommandText = "select * from StockRequest"; //Sets the text for the command

                var table = new DataTable();//Creates a datatable object to store what has been retrieved from the db
                var adapter = new SqlDataAdapter(command); //Creats a new SqlDataAdapter object with the above command

                adapter.Fill(table);//Fills the DataTable (table) obeject with items from the SqlDataAdapter

                foreach (var row in table.Select())
                {

                    int requestID = (int)row["StockRequestID"];
                    int storeID = (int)row["StoreID"];
                    int productID = (int)row["ProductID"];
                    int quantity = (int)row["Quantity"];

                    StockRequest addingRequest = new StockRequest(requestID, storeID, productID, quantity, true, null, null);
                    addingRequest.Quantity = quantity;
                    foreach (Item ownerItem in ownerInventoryStore.localStoreInventory)
                    {

                        if (addingRequest.ProductID == ownerItem.getId())
                        {

                            if (ownerItem.getStock() < quantity) //Checking if stock request can be processed
                            {
                                addingRequest.Available = false;
                            } 
                        }
                    }

                    requestList.Add(addingRequest);

                    connection.Close();
                }
            }
            Console.WriteLine("{0,-5} {1,-17} {2,-18} {3,-15} {4,-10} {5,-26} ", "ID", "Store", "Product", "Quantity", "Current Stock", " Stock Availability");

            foreach (StockRequest test in requestList)
            {
                int ownerStock = 0;
                string productName = null;

                foreach (Item checkItemName in ownerInventoryStore.localStoreInventory)
                {

                    int storeCheckId = test.StoreID;

                    test.findStoreName();//Local stock request method to get store name

                    if (test.ProductID == checkItemName.getId())
                    {
                        productName = checkItemName.getName();
                        ownerStock =checkItemName.getStock(); //Gets the owners store stock for each loop through
                    }
                }

                Console.WriteLine("{0,-5} {1,-17} {2,-18} {3,-15} {4,-10} {5,-26}  ", test.RequestID, test.StoreName, productName, test.Quantity, ownerStock, test.Available);
            }

            Console.WriteLine("\nEnter a request to process");

            //User input
            try
            {

                int choice = 0;
                //while (true)
                //{

                string userinput = Console.ReadLine();

                //validation here
                int userChoice = 0;

                if (int.TryParse(userinput, out userChoice))
                {
                    choice = userChoice;

                    foreach (StockRequest selectedRequest in requestList)
                    {
                        if (choice == selectedRequest.RequestID)
                        {
                            if (selectedRequest.Available == true)
                            {
                                processStockRequest(selectedRequest);

                                ownerItemsIntID.Clear();
                                ownerInventoryStore.localStoreInventory.Clear();
                                /*
                                 * CHECK FOR EFFECTS HERE. THIS CLEAR FUNCTION MAY BE A BIT TOO NUCLEAR
                                 * DO THE SAME, IN THE ELSE BELOW
                                 * 
                                 */
                            }
                            else
                            {
                                Console.WriteLine("Not enough stock in owner inventory to process stock request");

                                ownerItemsIntID.Clear();
                                ownerInventoryStore.localStoreInventory.Clear();

                            }

                        }
                    }



                }
                else
                {
                    Console.WriteLine("Please input a valid stock request to process");
                    ownerItemsIntID.Clear();
                    ownerInventoryStore.localStoreInventory.Clear();
                    choice = 0;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("System Exception : " + e.Message);
            }

        }//end of displaystock request


        public static void processStockRequest(StockRequest selectedRequest)
        { //Assuming everything validated, process the db request


            int stockRequestId = selectedRequest.RequestID;
            int storeId = selectedRequest.StoreID;
            int productId = selectedRequest.ProductID;
            int quantity = selectedRequest.Quantity;


            SqlConnection sqlOps = new SqlConnection("server=wdt2018.australiaeast.cloudapp.azure.com;uid=s3609685;database=s3609685;pwd=abc123");

            string sqlStatement = @"DELETE FROM StockRequest WHERE StockRequestID = @StockRequestID AND StoreID = @StoreID";

            try
            {
                sqlOps.Open();
                SqlCommand cmd = new SqlCommand(sqlStatement, sqlOps);
                cmd.Parameters.AddWithValue("StockRequestID", stockRequestId);
                cmd.Parameters.AddWithValue("StoreID", storeId);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

            }
            finally
            {
                sqlOps.Close();
            }
            //This probably doesn't need to open and close again here. Just a disticion between the steps
            try
            {
                sqlOps.Open();

                //var sqlText1 = "select * from StoreInventory where StoreID = @find";
                ////int checkID = 0;
                //SqlCommand dbCommCheck = new SqlCommand(sqlText1, sqlOps);
                //dbCommCheck.Parameters.AddWithValue("find", storeId);
                ////Console.WriteLine("Check id is " + storeId);


                SqlCommand checkForRow = new SqlCommand("SELECT COUNT(*) FROM StoreInventory WHERE (StoreID = @storeID) AND (ProductID = @productID)", sqlOps);
                checkForRow.Parameters.AddWithValue("@storeID", storeId);
                checkForRow.Parameters.AddWithValue("@productID", productId);


                int rowExists = (int)checkForRow.ExecuteScalar();

                if (rowExists >= 0)
                {
                    //Row exist

                        Console.WriteLine();

                        var sqlText = @"UPDATE StoreInventory
                SET StockLevel = @quantity
                WHERE ProductID = @inventoryID
                AND StoreID = @storeID";



                        SqlCommand dbCommand = new SqlCommand(sqlText, sqlOps);
                        dbCommand.Parameters.AddWithValue("quantity", quantity);
                        dbCommand.Parameters.AddWithValue("inventoryID", productId);
                        dbCommand.Parameters.AddWithValue("storeID", storeId);

                        dbCommand.Connection = sqlOps;

                        dbCommand.ExecuteNonQuery();
                }
                else
                {
                    //Row doesn't exist.
                        var sqlText = "INSERT INTO StoreInventory(StoreID, ProductID, StockLevel) VALUES(@storeID, @inventoryID, @quantity)";

                        SqlCommand dbCommand = new SqlCommand(sqlText, sqlOps);
                        dbCommand.Parameters.AddWithValue("quantity", quantity);
                        dbCommand.Parameters.AddWithValue("inventoryID", productId);
                        dbCommand.Parameters.AddWithValue("storeID", storeId);

                        dbCommand.Connection = sqlOps;

                        dbCommand.ExecuteNonQuery();
                }

 

            }
            finally
            {

                var ownerDelete = @"UPDATE OwnerInventory
                SET StockLevel = @quantity
                WHERE ProductID = @inventoryID";

                SqlCommand dbCommand = new SqlCommand(ownerDelete, sqlOps);
                dbCommand.Parameters.AddWithValue("quantity", quantity);
                dbCommand.Parameters.AddWithValue("inventoryID", productId);
                //dbCommand.Parameters.AddWithValue("storeID", storeSelect);

                dbCommand.Connection = sqlOps;

                dbCommand.ExecuteNonQuery();

                sqlOps.Close();
            }


        }
    }
}
