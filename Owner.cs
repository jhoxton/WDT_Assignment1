﻿using System;
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
        public static void resetItemStock(int resetID, string resetName) //
        {
            Console.WriteLine("ID IS " + resetID);
            //DB call here using resetID

            using (var sqlOp = new SqlConnection("server=wdt2018.australiaeast.cloudapp.azure.com;uid=s3609685;database=s3609685;pwd=abc123"))

            {
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

                sqlOp.Close();
                Console.WriteLine("{0} stock reset to 20", resetName);
                Console.WriteLine("\n======================\n");
                sqlOp.Close();
                return;

            }



        
        } //End of resetItemStock

        public static void resetItemIDSelect(Store ownerInventoryStore){ //Gets an item ID to reset in the database
           
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

                    if (int.TryParse(userinput, out userChoice))
                    {
                        choice = userChoice;

                    string resetName = "Null";
                    foreach (Item nameLoop in ownerInventoryStore.localStoreInventory) {
                        if (choice == nameLoop.getId()) {
                            resetName = nameLoop.getName();
                        }
                        
                    }
                        resetItemStock(choice, resetName);
                    //Owner.ownerMenu();//??????

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

            //Console.WriteLine("Prod input AT END OF PRODUCTPRINT is " + prodInput);
        }//End of productPrint

        public static void displayStockRequests(Store ownerInventoryStore) { //Loops & prints all stock requests from the db, prompts the owner to process one. 
            List<int> ownerItemsIntID = new List<int>();//List to locally store items selected stores inventory, via ItemID

            int requestItem;
            int requestQuant;
            int requstStore;
            bool available;

            List<StockRequest> requestList = new List<StockRequest>(); //List to store then print all current stock requests locally

                                  //shift this method to the start of class so it doesn't get callec a milliontimes! Same with franchise and customer
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

                    StockRequest addingRequest = new StockRequest(requestID, storeID, productID, quantity, true);

                    foreach (Item ownerItem in ownerInventoryStore.localStoreInventory) {

                        if(addingRequest.ProductID == ownerItem.getId()) {
                           
                            if (ownerItem.getStock() < quantity)
                            {
                                addingRequest.Available = false;
  
                            }
 
                        }
                    }

                    requestList.Add(addingRequest);

                    connection.Close();
                }



                //GET STORE NAME AND PRODUCT NAME HERE
                //ALSO GET OWNERS CURRENT STOCK
                Console.WriteLine("{0,-5}  {1,-22} {2,-30} {3,-35} {4,-40} {5,-48} ", "ID", "Store", "Product", "Quantity", "Current Stock", " Stock Availability");
 
                foreach(StockRequest test in requestList) {
                    string storeName = null;
                    string productName = null;

                    foreach(Item checkItemName in ownerInventoryStore.localStoreInventory) {

                        int storeCheckId = test.StoreID;
                         storeName = getStoreName(storeCheckId, storeName);



                        if(test.ProductID == checkItemName.getId()) {
                            productName = checkItemName.getName();
                        }
                    }

                    Console.WriteLine("{0,-5}  {1,-22} {2,-30} {3,-35} {4,-40} {5,-48}",test.RequestID, storeName, productName, test.Quantity, 0, test.Available);
                }

            }

            //Get owner stock levels. Has this already been done?

            //Get all requsts from db, make new stock request items, find the match name method and print

            //User input

            //Cross check requst quantity against relevent stock item (use input and item ID)
            //If input > ID, bool is true, call process method and remove requst from db

        }//end of displaystock request

        public static string getStoreName(int storeCheckId, string storeName)//This is a lazy method to get store names without having to call the db again
        {
            switch (storeCheckId)
            {
                case 1:
                    storeCheckId = 1;
                    storeName = "Melbourne CBD";
                    //return storeName;
                    break;
                case 2:
                    storeCheckId = 2;
                    storeName = "North Melbourne";
                    //return storeName;
                    break;
                case 3:
                    storeCheckId = 3;
                    storeName = "East Melbourne";
                    break;
                case 4:
                    storeCheckId = 4;
                    storeName = "South Melbounre";
                    break;
                case 5:
                    storeCheckId = 5;
                    storeName = "West Melbourne";
                    //return storeName;
                    break;
                default:
                    break;
            } //switch
            return storeName;
        }

        public static void processStockRequest(int requestItem, int requestQuant, int requstStore) {
            //Call store db
            //Update stock with the 3 passed ints
        }
    }
}
