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

        public List<Item> localStoreInventory = new List<Item>();//Local copy of the stores items

        public List<int> thresholdIDs = new List<int>();

        public string getName()
        {
            return name;
        }

        public int getId()
        {
            return id;
        }
        public void setName(String name)
        {
            this.name = name;
        }

        public void setId(int id)
        {
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
                //int valCheck = int.Parse(userinput);

                //string line = Console.ReadLine();

                int value;
                if (int.TryParse(userinput, out value))
                {
                    if (value > 5)
                    {
                        Console.WriteLine("Please select a Store ID between 1 and 5");
                        storePrint(storeSelect);
                    }
                }
                else
                {
                    Console.WriteLine("Please select a Store ID between 1 and 5");
                    storePrint(storeSelect);
                }

                //if(valCheck > 5) {
                //    Console.WriteLine("Please select a Store ID between 1 and 5");
                //    storePrint(storeSelect);
                //}



                /*
                 * 
                 * Validation here 
                 *
                 */

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

            return storeSelect;
        }//End of store print


        public List<int> getStoreInv(List<int> storeItemsIntID) //Gets the store innventory as ints to cross check just the Product ID's
        {
            if (storeItemsIntID != null)
            {
                var selectedID = this.getId();

                using (var connection = new SqlConnection("server=wdt2018.australiaeast.cloudapp.azure.com;uid=s3609685;database=s3609685;pwd=abc123"))
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
                        int stockLevel = (int)row["StockLevel"];

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
                    connection.Close();
                }
            }
            return storeItemsIntID;
        }//END OF getStoreInv 

        public int makeItem(int itemInStoreID, int stockLevel, List<int> storeItemsIntID) //Makes new Item objects in the local store object
        {
            Item addingItem = new Item(null, 0, 0);
            addingItem.setId(itemInStoreID);
            addingItem.setStock(stockLevel);

            string storeRetrievedName = addingItem.listStore(itemInStoreID); //Gets item name from Item db
         
            addingItem.setName(storeRetrievedName);//Gets the item name
            localStoreInventory.Add(addingItem);//Adds the new item to the current store inventory

            return itemInStoreID;

        }//end of makeItem

        public List<int> getOwnerInv(List<int> storeItemsIntID) //Gets the owner inventory, which is treated like a local Store
        {
            var selectedID = this.getId();

             using (var connection = new SqlConnection("server=wdt2018.australiaeast.cloudapp.azure.com;uid=s3609685;database=s3609685;pwd=abc123"))
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
                    int itemInStoreInv = (int)row["ProductID"];
                    int stockLevel = (int)row["StockLevel"];
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
                connection.Close();
            }
            return storeItemsIntID;
        }//END OF getOwnerInv 
    }
}
