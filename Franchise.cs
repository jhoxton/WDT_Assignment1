﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Assignment1
{
    public class Franchise
    {

        public static void franchiseMenu()
        //Prompt for Store ID here first
        {
            Store currentStore = new Store(null, 0); //Empty store object
            //int prodInput = 0;
            int storeSelect = 0;
            int threshold = 0;

            if (currentStore.getId() == 0)
            {
                storeSelect = currentStore.storePrint(storeSelect);
            }
            else
            {
                //Double check to make sure the currentStore is blank/null to avoid local data being messed up
            }
            try
            {
                int choice = 0;
                while (true)
                {
                    Console.WriteLine("Welcome to Marvelous Magic(Franchise Holder - " + currentStore.getName() + ")\n ============== ");
                    Console.WriteLine("1. Display Inventory");
                    Console.WriteLine("2. Stock Request(Threshold)");
                    Console.WriteLine("3. Add New Inventory Item");
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
                            franchiseProductPrint(currentStore);
                            break;
                        case 2:
                            choice = 2;
                            setThreshold(threshold, currentStore);
                            break;
                        case 3:
                            choice = 3;
                            addNewItem(currentStore);
                            //Get all Owner product IDs into a list, cross check against Franchise IDs and then remove any that are in both
                            //Then print said list and allow user to select an ID, which makes a single item to add to Stores database
                            break;
                        case 4:
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
        }//end of franchiseMenu

        public static void addNewItem(Store currentStore)
        {
            List<int> ownerItemsIntID = new List<int>(); //ID's of the owner ie all products
            List<int> franchiseItemsIntID = new List<int>();//ID's of the current store that are in stock
            List<int> unstockedItemsIntId = new List<int>();//ID's that are currently not in stock in the current store, to print

            List<Item> itemsToPrint = new List<Item>();//List of items to print

            currentStore.getStoreInv(franchiseItemsIntID);//YAY for modular reusable methods! Better check for artifacts though

            using (var connection = new SqlConnection("server=wdt2018.australiaeast.cloudapp.azure.com;uid=s3609685;database=s3609685;pwd=abc123"))
            {
                connection.Open();

                var sqlText = "select * from OwnerInventory";

                SqlCommand dbCommand = new SqlCommand(sqlText, connection);
                dbCommand.ExecuteNonQuery();

                var table = new DataTable();
                var adapter = new SqlDataAdapter(dbCommand);

                adapter.Fill(table);

                foreach (var row in table.Select())
                {
                    int itemInStoreInv = (int)row["ProductID"];
                    int stockLevel = (int)row["StockLevel"];
                    ownerItemsIntID.Add(itemInStoreInv);//Gets item ID's for Owner Stock to cross check

                    Item addingItem = new Item(null, 0, 0);//Adds items to a new list to print items that the store does not have

                    addingItem.setId(itemInStoreInv);
                    addingItem.setStock(stockLevel);

                    string storeRetrievedName = addingItem.listStore(itemInStoreInv); //Gets item name from Item db
                    addingItem.setName(storeRetrievedName);
                    itemsToPrint.Add(addingItem);

                }

                foreach (Item i in currentStore.localStoreInventory)
                {
                    int storeID = i.getId();
                    franchiseItemsIntID.Add(storeID);//Gets item ID's from store stock to cross check

                }
                connection.Close();

                ownerItemsIntID.RemoveAll(l => franchiseItemsIntID.Contains(l));//Removers any duplicates in both lists from the Owner list (it has the important info)
                List<int> itemsNotInStockIntID = ownerItemsIntID; //Makes a new list of item ID's not in store

                Console.WriteLine("\nInventory");

                Console.WriteLine("{0,-5}  {1,-22} {2,-30}", "ID", "Product", "Current Stock\n");
                foreach (int i in itemsNotInStockIntID)
                {
                    foreach (Item j in itemsToPrint)
                    {
                        if (i == j.getId())
                        {
                            Console.WriteLine("{0,-5}  {1,-22} {2,-30}", j.getId(), j.getName(), j.getStock());
                        }
                    }
                }
                Console.WriteLine();
                Console.WriteLine("Enter product to add: \n");
                StockRequest singleItemRequest = new StockRequest(0, 0, 0, 1, true, null, null); 
                //Makes a new stock request item but with the quantity set to 1

                makeStockReguest(singleItemRequest, currentStore);
            }
        }//end of addNewItem

        public static void franchiseProductPrint(Store currentStore)
        {
            List<int> storeItemsIntID = new List<int>();//List to locally store items selected stores inventory, via ItemID

            Console.WriteLine("Inventory");

            currentStore.getStoreInv(storeItemsIntID); 
            //Accesses the StoreInventory db to get items and qunatity in store         
            // Then creates new items (id, name and quantity) and adds them to the local store objects inventory

            Console.WriteLine("{0,-5}  {1,-22} {2,-30}", "ID", "Product", "Current Stock");

            foreach (Item i in currentStore.localStoreInventory) //Test print on current stores stock
            {
                Console.WriteLine("{0,-5}  {1,-22} {2,-30}", i.getId(), i.getName(), i.getStock());
            }

            Console.WriteLine();

            storeItemsIntID.Clear();
            currentStore.localStoreInventory.Clear();


        }//End of productPrint


        public static void setThreshold(int threshold, Store currentStore)
        {

            List<int> storeItemsIntID = new List<int>();
            currentStore.getStoreInv(storeItemsIntID);
            StockRequest request = new StockRequest(0, 0, 0, 0, true, null, null);

            Console.WriteLine("Enter threshold for re-stocking ");
            int restockThreshold = 0;
            int choice;
            bool stockFull = false;

            string userinput = Console.ReadLine();
            if (int.TryParse(userinput, out choice))
            {
                restockThreshold = choice;
            }

            while (stockFull == false)
                foreach (Item i in currentStore.localStoreInventory)
                {
                   //Getting item ID's. If they are higher than userinput, ignore, if lower add to currentStore.thresholdIDs
                    if (i.getStock() <= restockThreshold)
                    {

                        currentStore.thresholdIDs.Add(i.getId());
                        stockFull = true;
                    }
                    else
                    {
                        stockFull = false;

                    }

                }
            if (stockFull == true)
            {
                Console.WriteLine("{0,-5}  {1,-22} {2,-30}", "ID", "Product", "Current Stock");
                foreach (Item itemPrint in currentStore.localStoreInventory)
                {
                    foreach (int idPrint in currentStore.thresholdIDs)
                    {
                        if (idPrint == itemPrint.getId())
                        {
                            Console.WriteLine("{0,-5}  {1,-22} {2,-30}", itemPrint.getId(), itemPrint.getName(), itemPrint.getStock());

                        }
                    }
                }

                request.Quantity = restockThreshold;//Sets the threshold as the quantity of the stock request item

                //Console.WriteLine("1ST Request Item = ID {0} StoreID {1} Quantity {2} ItemID {3}", request.RequestID, request.StoreID, request.Quantity, request.ProductID);
                Console.WriteLine("Enter request to process: ");
                makeStockReguest(request, currentStore);
            } else {
                Console.WriteLine("All inventory stock levels are equal to or above {0}", restockThreshold);
                franchiseMenu();
            }
           


        }
        public static StockRequest makeStockReguest(StockRequest request, Store currentStore)
        {
            Console.WriteLine();

            string input = Console.ReadLine();

            int requestToProcess = 0;
            int choice;

            if (int.TryParse(input, out choice))
            {
                requestToProcess = choice;
            }


            using (var con = new SqlConnection("server=wdt2018.australiaeast.cloudapp.azure.com;uid=s3609685;database=s3609685;pwd=abc123"))
            //This is to make sure the primary key in the StockRequest table is incremental
            {
                con.Open();

                SqlCommand cmd = new SqlCommand();

                String query = "select max(StockRequestID) from StockRequest;";
                cmd.Connection = con;
                cmd.CommandText = query;
                String retrievedID = cmd.ExecuteScalar().ToString();

                int maxID =0;
                //int choice;
                if (int.TryParse(retrievedID, out choice))
                {
                    maxID = choice;
                }

                maxID = maxID + 1; 
        /*
         * For some reason the whole thing crashes if there are no other Stock Requests in the database!
         */
                con.Close();
                request.RequestID = maxID;
            }

            request.StoreID = currentStore.getId();
            request.ProductID = requestToProcess;

            var stockRequestID = request.RequestID;
            var storeID = request.StoreID;
            var productID = request.ProductID;
            var quanity = request.Quantity;

            var insertCmd = "SET IDENTITY_INSERT StockRequest ON INSERT INTO StockRequest (StockRequestID, StoreID, ProductID, Quantity) VALUES (@stockRequestIDval, @storeIDval, @productIDval, @quantityVal)";

            using (var sqlOp = new SqlConnection("server=wdt2018.australiaeast.cloudapp.azure.com;uid=s3609685;database=s3609685;pwd=abc123"))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = sqlOp;
                    comm.CommandText = insertCmd;
                    //comm.CommandText = "SET IDENTITY_INSERT StockRequest ON";
                    //sqlOp.Open();
                    //comm.ExecuteNonQuery();
                    comm.Parameters.AddWithValue("@stockRequestIDval", stockRequestID);
                    comm.Parameters.AddWithValue("@storeIDval", storeID);
                    comm.Parameters.AddWithValue("@productIDval", productID);
                    comm.Parameters.AddWithValue("@quantityVal", quanity);

                    sqlOp.Open();
                    comm.ExecuteNonQuery();

                }
                sqlOp.Close();
            }
            return request;
        }
    }
}


