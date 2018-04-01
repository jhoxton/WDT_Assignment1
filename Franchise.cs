using System;
using System.Collections.Generic;
using System.Data.SqlClient;

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

            storeSelect = currentStore.storePrint(storeSelect);

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
                            franchiseProductPrint(currentStore);
                            break;
                        case 2:
                            choice = 2;
                            //int threshold = 0;
                            //List<int> storeItemsIntID = new List<int>();

                            //currentStore.getStoreInv(storeItemsIntID);
                            setThreshold(threshold, currentStore);

                            //StockRequest request = new StockRequest(0,0,0,0,true);
                            //Console.WriteLine("THRESHOLD IS " + threshold);
                            //makeStockReguest(currentStore, threshold);

                            //TODO
                            break;
                        case 3:
                            choice = 3;
                            //productPrint();
                            //TODO
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
        public static void franchiseProductPrint(Store currentStore)
        {

            List<int> storeItemsIntID = new List<int>();//List to locally store items selected stores inventory, via ItemID

            //TODO Need to validate input to this List to avoid duplicates???
            Console.WriteLine("Inventory");

            currentStore.getStoreInv(storeItemsIntID); //Accesses the StoreInventory db to get items and qunatity in store         

            // Then creates new items (id, name and quantity) and adds them to the local store objects inventory

            Console.WriteLine("{0,-5}  {1,-22} {2,-30}", "ID", "Product", "Current Stock");

            //loop over current store items here

            foreach (Item i in currentStore.localStoreInventory) //Test print on current stores stock
            {

                Console.WriteLine("{0,-5}  {1,-22} {2,-30}", i.getId(), i.getName(), i.getStock());

            }

            Console.WriteLine();
            //franchiseProductPrint(currentStore);
            storeItemsIntID = null;
            //TODO how to stop the store items list doubling up?


            //Console.WriteLine("Prod input AT END OF PRODUCTPRINT is " + prodInput);

            //return prodInput;


        }//End of productPrint


        public static void setThreshold(int threshold, Store currentStore)
        {

            List<int> storeItemsIntID = new List<int>();
            currentStore.getStoreInv(storeItemsIntID);
            StockRequest request = new StockRequest(0, 0, 0, 0, true);


            //foreach(Item i in currentStore.localStoreInventory) {
            //    Console.WriteLine("Items found " +i.getName());
            //}

            Console.WriteLine("Enter threshold for re-stocking ");
            int restockThreshold = 0;
            int choice;
            bool stockFull = false;

            string userinput = Console.ReadLine();
            if (int.TryParse(userinput, out choice))
            {
                restockThreshold = choice;
            }

            //Console.WriteLine("restockThreshold is "+ restockThreshold);
            //Console.WriteLine("choice is" + choice);

            while(stockFull == false) foreach (Item i in currentStore.localStoreInventory)
            {
                //Console.WriteLine(i.getId());//Getting item ID's. If they are higher than userinput, ignore, if lower add to currentStore.thresholdIDs
                if (i.getStock() <= restockThreshold)
                {

                    currentStore.thresholdIDs.Add(i.getId());
                    stockFull = true;
                    //foreach(int test in currentStore.thresholdIDs){
                    //    Console.WriteLine(test);
                    //}
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
                //NEED TO WRITE TO THE REQUEST ITEM HERE?

                request.Quantity = restockThreshold;
                Console.WriteLine("1ST Request Item = ID {0} StoreID {1} Quantity {2} ItemID {3}", request.RequestID, request.StoreID, request.Quantity, request.ProductID);

                makeStockReguest(request, currentStore);
            }
            else
            {
                Console.WriteLine("All inventory stock levels are equal to or above {0}", restockThreshold);
                franchiseMenu();
            }


        }
        public static StockRequest makeStockReguest(StockRequest request, Store currentStore)
        {
           
            //Item itemToRequest = new Item(null, 0, 0);

            //Console.WriteLine("{0,-5}  {1,-22} {2,-30}", "ID", "Product", "Current Stock");
            //foreach (Item itemPrint in currentStore.localStoreInventory)
            //{
                //foreach (int idPrint in currentStore.thresholdIDs)
                //{
                //    if (idPrint == itemPrint.getId())
                //    {
                //        Console.WriteLine("{0,-5}  {1,-22} {2,-30}", itemPrint.getId(), itemPrint.getName(), itemPrint.getStock());

                //    }
                //}

            //}

            Console.WriteLine();
            Console.WriteLine("Enter request to process: ");

            string input = Console.ReadLine();

            int requestToProcess = 0;
            int choice;


            if (int.TryParse(input, out choice))
            {
                requestToProcess = choice;
            }

            //request.setRequestId(requestID);

            //request.setRequestId(request.getRequestID());

            request.RequestID = request.RequestID + 1;
            request.StoreID = currentStore.getId();
            request.ProductID = requestToProcess;



            Console.WriteLine("2nd Request Item = ID {0} StoreID {1} Quantity {2} ItemID {3}", request.RequestID, request.StoreID, request.Quantity, request.ProductID);

            //StockRequest request = new StockRequest(1,currentStore.getId(),requestToProcess,threshold,true);


            //foreach(Item loopItem in currentStore.localStoreInventory) {
            //    if(requestToProcess == loopItem.getId()) {
            //        itemToRequest.setId(loopItem.getId());
            //        itemToRequest.setName(loopItem.getName());

            //    }
                
            //}

            //Console.WriteLine("Item to process is " + requestToProcess);
            ////Console.WriteLine("Quanity is " + threshold);


            //StockRequest request = new StockRequest(0, 0, 0, 0, true);


            //////

            using (var sqlOp = new SqlConnection("server=wdt2018.australiaeast.cloudapp.azure.com;uid=s3609685;database=s3609685;pwd=abc123"))

            {
                var stockRequestID = request.RequestID;
                var item = request.StoreID;
                var quanity = request.Quantity;
                var productID =request.ProductID;

                //Console.WriteLine("3rd Request Item = ID {0} StoreID {1} Quantity {2} ItemID {3}", request.RequestID, request.StoreID, request.Quantity, request.ProductID);

               


                //sqlOp.Open();

                ////var sqlText = @"UPDATE StoreInventory
                ////SET StockLevel = @quantity
                ////WHERE ProductID = @inventoryID
                ////AND StoreID = @storeID";

                //var sqlText = @"INSERT INTO StockRequest
                //VALUES(@stockRequestID, @item, @quanity, @productID)";


                //SqlCommand dbCommand = new SqlCommand(sqlText, sqlOp);
                //dbCommand.Parameters.AddWithValue("stockRequestID", stockRequestID);
                //dbCommand.Parameters.AddWithValue("item", item);
                //dbCommand.Parameters.AddWithValue("quanity", quanity);

                //dbCommand.Parameters.AddWithValue("productID", productID);

                //dbCommand.Connection = sqlOp;

                //dbCommand.ExecuteNonQuery();

                //sqlOp.Close();

                //Console.WriteLine("\n======================\n");
           

            }
            return request; }




      
    }

}


