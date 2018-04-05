using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;
namespace Assignment1
{
    public class Item
    {
        private string name;
        private int id;
        private int stock;

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

        public void setStock(int stock) {
            this.stock = stock;
        }

        public int getStock () {
            return stock;
        }

        public Item(string itemName, int itemId, int itemStock)
        {
            
            name = itemName;
            id = itemId;
            stock = itemStock;
        }
       
        public string listStore(int itemInStoreID) { //This gets the item name when a store is populated with stock

            string prodName = null;

            using (var connection = new SqlConnection("server=wdt2018.australiaeast.cloudapp.azure.com;uid=s3609685;database=s3609685;pwd=abc123"))
         
            {
                connection.Open();
                //Opens said "object"

                var command = connection.CreateCommand();
                //Creates a command
                command.CommandText = "select * from Product"; //Sets the text for the command

                var table = new DataTable();//Creates a datatable object to store what has been retrieved from the db
                var adapter = new SqlDataAdapter(command); //Creats a new SqlDataAdapter object with the above command

                adapter.Fill(table);//Fills the DataTable (table) obeject with items from the SqlDataAdapter

                foreach (var row in table.Select())
                {
                    int productID = (int)row["ProductID"];

                    if( itemInStoreID == productID) {
                       prodName = row["Name"].ToString();
                    }                    
                }
                connection.Close();
            }


            return prodName;
        }

    }
}
