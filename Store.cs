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


        public void storePrint()
        {
            using (var connection = new SqlConnection("server=wdt2018.australiaeast.cloudapp.azure.com;uid=s3609685;database=s3609685;pwd=abc123"))
            //Creates a new SQL connection "object"
            {

                //Item test = new Item("TEST", 0);
                //this.Inventory.Add(test);


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
                    //this.setId = row["StoreID"].ToString();

                    //setId((int)row["StoreId"]);
                    string name = row["StoreID"].ToString();

                    if (userinput == name)
                    {

                       

                        Console.WriteLine("YEAHHH");
                        //Console.WriteLine(dbStore.name + " store name");

                        this.setName(row["name"].ToString());
                        setId((int)row["StoreId"]);
                        //Write store stock and shit here

                    }

                }

                connection.Close();
               
            }
        }//End of store print
    }
}
