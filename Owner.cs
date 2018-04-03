using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Assignment1
{
    public class Owner
    {

        public static void ownerMenu()
        {
            Store ownerInventoryStore = new Store(null, 0); //Empty store object
            //int prodInput = 0;
            //int storeSelect = 0;

            //storeSelect = ownerInventory.storePrint(storeSelect);


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
                    } //End Switch
                } // end of while


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

            //loop over current store items here

            foreach (Item i in ownerInventoryStore.localStoreInventory) //Test print on current stores stock
            {

                Console.WriteLine("{0,-5}  {1,-22} {2,-30}", i.getId(), i.getName(), i.getStock());

            }

            Console.WriteLine();



            //Console.WriteLine("Prod input AT END OF PRODUCTPRINT is " + prodInput);

            //return prodInput;


        }//End of productPrint

        public static void displayStockRequests() {
            
        }
    }
}
