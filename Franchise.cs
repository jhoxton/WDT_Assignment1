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

            while(stockFull == false) 
                foreach (Item i in currentStore.localStoreInventory)
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

                request.Quantity = restockThreshold;//Sets the threshold as the quantity of the stock request item

                //Console.WriteLine("1ST Request Item = ID {0} StoreID {1} Quantity {2} ItemID {3}", request.RequestID, request.StoreID, request.Quantity, request.ProductID);

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

            //Sets the rest of the values of the stock request item
            request.RequestID = request.RequestID + 1;
            request.StoreID = currentStore.getId();
            request.ProductID = requestToProcess;

            Console.WriteLine("2nd Request Item = ID {0} StoreID {1} Quantity {2} ItemID {3}", request.RequestID, request.StoreID, request.Quantity, request.ProductID);

                var stockRequestID = request.RequestID + 1;
                var storeID = request.StoreID;              
                var productID =request.ProductID;
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
                    //try
                    //{
                        sqlOp.Open();
                        comm.ExecuteNonQuery();
                    //}
                    //catch (SqlException)
                    //{
                        //Console.WriteLine("Error");
                    //}

                }
                sqlOp.Close();
            
            }

            return request; }




      
    }

}


