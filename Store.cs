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


        public List<Item> storeInventory = new List<Item>();

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

        public int storePrint(int storeSelect)
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
                    string StoreName = row["Name"].ToString();

                    if (userinput == StoreID)
                    {
                        //storeSelect = ((int)row["StoreId"]);
                        this.setId((int)row["StoreId"]);
                        this.setName(row["Name"].ToString());

                    }

                }

                connection.Close();

            }
            //this.setId(storeSelect);

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
                    //Use something to write to the local Store List of Items

                    int itemInStoreInv = (int)row["ProductID"];//This is the old one
                    int stockLevel = (int)row["StockLevel"];
                    Console.WriteLine("ID is: " +itemInStoreInv + " Stock is: " + stockLevel);

                    makeItem(itemInStoreInv, stockLevel);

                    //THIS IS RIGHT, NOW JUST ADD ITEMS TO LOCAL LIST

                    //Item itemToAdd = new Item(null, 0, 0);

                    //itemToAdd.setId((int)row["ProductID"]);
                    //itemToAdd.setName(row["Name"].ToString());
                    //this.storeInventory.Add(itemToAdd);

                    //Console.WriteLine("Passing to :" +itemInStoreInv);

                    storeItemsIntID.Add(itemInStoreInv);//This is the ID to match to quantity in the StoreInventory table

                  

                    foreach(int i in storeItemsIntID) {
                        //Console.WriteLine("Item id's is: " + i);
                    }
                }


                connection.Close();

            }

            return storeItemsIntID;
        }//END OF getStoreInv 
      
        public int makeItem (int itemInStoreID, int stockLevel ) {

            Item addingItem = new Item(null,itemInStoreID, stockLevel);

            string storeRetrievedName = addingItem.listStore(itemInStoreID);

            Console.WriteLine("storeRetrievedName is "+ storeRetrievedName);



            addingItem.setName(storeRetrievedName);

            //ADD TO LOCAL ITEM LIST HERE!!!!!!!!!!!!!!


            return itemInStoreID;


            
        }
    }
}
