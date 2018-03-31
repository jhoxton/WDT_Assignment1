using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;

namespace Assignment1
{
    public class Store
    {
        private string name;
        private int id;


        public List<Item> localStoreInventory = new List<Item>();

        public string getName() {
            return name;
        }

        public int getId() {
            return id;
        }
        public void setName(String name) {
            this.name = name;
        }

        public void setId(int id) {
            this.id = id;
        }
        public Store(string name, int id)
        {
            //this.StoreInventory = storeInventory;
            this.name = name;
            this.id = id;
        }

        public int storePrint(int storeSelect) //Matches user input StoreID to database to write local Store Object
        { 
            using (var connection = new SqlConnection("server=wdt2018.australiaeast.cloudapp.azure.com;uid=s3609685;database=s3609685;pwd=abc123"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "select * from Store"; 

                var table = new DataTable();
                var adapter = new SqlDataAdapter(command); 

                adapter.Fill(table);

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
                    string StoreName = row["Name"].ToString();

                    if (userinput == StoreID) //Cross check input and StoreID in db
                    {
                        //storeSelect = ((int)row["StoreId"]);
                        this.setId((int)row["StoreId"]);
                        this.setName(row["Name"].ToString());
                    }
                }
                connection.Close();
            }

            //Console.WriteLine("Store ID:" + getId());
            //Console.WriteLine("Store Name: " + getName());
            //Console.WriteLine("Inside store object, ID is : " + this.getId());
            return storeSelect;
        }//End of store print


        public List<int> getStoreInv(List<int> storeItemsIntID) //Gets the store innventory
        {
            var selectedID = this.getId();

            //WRITE TO ITEM OBJECTS TO STORE LIST HERE AND PASS IT BACK!!!

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

                    //THE PROBLEM IS HERE WITH THE LOOP NEED TO REMOVE DUPLICATION SOMEHOW
                  

                    int itemInStoreInv = (int)row["ProductID"];//This is the old one
                    int stockLevel = (int)row["StockLevel"];

                    //Console.WriteLine("BEFORE MAKE ITEM ID is: " +itemInStoreInv +  " Stock is: " + stockLevel);

                   
                    foreach(int i in storeItemsIntID) {
                        if(i == itemInStoreInv) {
                            //Dulpicate found
                        } else {
                            storeItemsIntID.Add(itemInStoreInv);//This is the ID to match to quantity in the StoreInventory table
                        }
                    }
                  
                    makeItem(itemInStoreInv, stockLevel, storeItemsIntID); //Creates items then adds them to the local soter inventory
                 
                }

                //foreach (Item i in localStoreInventory) //Test print on current stores stock
                //{
                //    Console.WriteLine("ID is " + i.getId() + " Product Name is " + i.getName() + " Stock is " + i.getStock());
                //}

                connection.Close();

            }
            //foreach (int i in storeItemsIntID)
            //{
            //    Console.WriteLine("storeItemsIntID list: " + i);
             
            //}
            return storeItemsIntID;
        }//END OF getStoreInv 
      
        public int makeItem (int itemInStoreID, int stockLevel, List<int> storeItemsIntID ) {

            Item addingItem = new Item(null, 0, 0);
            addingItem.setId(itemInStoreID);
            addingItem.setStock(stockLevel);
           

            string storeRetrievedName = addingItem.listStore(itemInStoreID); //Gets item name from Item db
            addingItem.setName(storeRetrievedName);
            //Console.WriteLine("Item found is {0}",storeRetrievedName);
            //Console.WriteLine("storeRetrievedName is "+ storeRetrievedName);
            //Console.WriteLine("itemInStoreID is: " + itemInStoreID);

            localStoreInventory.Add(addingItem);

            return itemInStoreID;


            
        }
        public List<int> getOwnerInv(List<int> storeItemsIntID) //Gets the store innventory
        {
            var selectedID = this.getId();

            //WRITE TO ITEM OBJECTS TO STORE LIST HERE AND PASS IT BACK!!!

            using (var connection = new SqlConnection("server=wdt2018.australiaeast.cloudapp.azure.com;uid=s3609685;database=s3609685;pwd=abc123"))
            //Creates a new SQL connection "object"
            {
                connection.Open();

                var sqlText = "select * from OwnerInventory";

                SqlCommand dbCommand = new SqlCommand(sqlText, connection);
                dbCommand.Parameters.AddWithValue("find", selectedID);

                dbCommand.Connection = connection;

                dbCommand.ExecuteNonQuery();

                var table = new DataTable();
                var adapter = new SqlDataAdapter(dbCommand);

                adapter.Fill(table);

                foreach (var row in table.Select())
                {

                    //THE PROBLEM IS HERE WITH THE LOOP NEED TO REMOVE DUPLICATION SOMEHOW


                    int itemInStoreInv = (int)row["ProductID"];//This is the old one
                    int stockLevel = (int)row["StockLevel"];

                    //Console.WriteLine("BEFORE MAKE ITEM ID is: " +itemInStoreInv +  " Stock is: " + stockLevel);


                    foreach (int i in storeItemsIntID)
                    {
                        if (i == itemInStoreInv)
                        {
                            //Dulpicate found
                        }
                        else
                        {
                            storeItemsIntID.Add(itemInStoreInv);//This is the ID to match to quantity in the StoreInventory table
                        }
                    }

                    makeItem(itemInStoreInv, stockLevel, storeItemsIntID); //Creates items then adds them to the local soter inventory

                }

                //foreach (Item i in localStoreInventory) //Test print on current stores stock
                //{
                //    Console.WriteLine("ID is " + i.getId() + " Product Name is " + i.getName() + " Stock is " + i.getStock());
                //}

                connection.Close();

            }
            //foreach (int i in storeItemsIntID)
            //{
            //    Console.WriteLine("storeItemsIntID list: " + i);

            //}
            return storeItemsIntID;
        }//END OF getStoreInv 
    }
}
