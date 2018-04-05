using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;

namespace Assignment1
{
    public class MainClass
    {
        //ONCE EVERYTHING IS DONE, BREAK THIS UP INTO CLASS LIBRARIES TO USE
      
        static void Main(string[] args)
        {
            //Item currentItem = new Item(null, 0);

            //productPrint(currentItem);
            SqlConnection sqlOp = new SqlConnection("server=wdt2018.australiaeast.cloudapp.azure.com;uid=s3609685;database=s3609685;pwd=abc123");

            mainMenu();           
        }

        public static void connectionTest()
        {
            Console.WriteLine("Connecting to database");

            SqlConnection sqlOp = new SqlConnection("server=wdt2018.australiaeast.cloudapp.azure.com;uid=s3609685;database=s3609685;pwd=abc123");

            sqlOp.Open();

            //DataSet setProducts = new DataSet();
            //SqlDataAdapter adapProducts = new SqlDataAdapter("select * from Product", sqlOp);

            //SqlCommandBuilder cmdBldr = new SqlCommandBuilder(adapProducts);

            //adapProducts.Fill(setProducts, "Product");

            //string updateString = @"
            //update Product
            //set Name = 'Did it work?'
            //where Name = 'Test'";

            var newQuant = 10;
            var selectedID = 7;

            var sqlText = @"UPDATE StoreInventory
                SET StockLevel = @price
                WHERE ProductID = @inventoryID";
            
            SqlCommand dbCommand = new SqlCommand(sqlText, sqlOp);
            dbCommand.Parameters.AddWithValue("price", newQuant);
            dbCommand.Parameters.AddWithValue("inventoryID",selectedID );

            dbCommand.Connection = sqlOp;

            dbCommand.ExecuteNonQuery();

                            //string updateString = @"
                            //         update StoreInventory
                            //         set StockLevel = '10'
                            //         where ProductID = '3'";

                            //// 1. Instantiate a new command with command text only
                            //SqlCommand cmd = new SqlCommand(updateString);

                            //// 2. Set the Connection property
                            //cmd.Connection = sqlOp;

                            //// 3. Call ExecuteNonQuery to send command
                            //cmd.ExecuteNonQuery();
            sqlOp.Close();

        }

        //public static void setRequestID() {
            //using (var con = new SqlConnection("server=wdt2018.australiaeast.cloudapp.azure.com;uid=s3609685;database=s3609685;pwd=abc123"))

            //{
            //    con.Open();
            //    //Opens said "object"

            //    SqlCommand cmd = new SqlCommand();


            //    String query = "select max(StockRequestID) from StockRequest;";
            //    cmd.Connection = con;
            //    cmd.CommandText = query;
            //    String retrievedID = cmd.ExecuteScalar().ToString();

            //    int maxID = int.Parse(retrievedID) +1;

            //    Console.WriteLine("Next stock request id should be" + maxID);
            //    con.Close();

            //}

            //}

        public static void mainMenu()  //The main menu method
        {

            try
            {
                int choice = 0;
                while (true)
                {
                    Console.WriteLine("==============\nWelcome to Marvelous Magic\n==============");
                    Console.WriteLine("1. Owner");
                    Console.WriteLine("2. Franchise Holder");
                    Console.WriteLine("3. Customer");
                    Console.WriteLine("4. Quit\n");

                    string userinput = Console.ReadLine();

                    //User input validation
                    int userChoice = 0;
                    if (int.TryParse(userinput, out userChoice))//Turns user inout from a string to an int
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
                            Owner.ownerMenu();

                            //ownerMenu();
                            break;
                        case 2:
                            choice = 2;
                            //Franchise franchise = new Franchise();
                            Franchise.franchiseMenu();
                            //franchise

                            break;
                        case 3:
                            choice = 3;
                            //Customer customer = new Customer();
                            Customer.customerMenu();
                            //customer = null;

                            //customerMenu();
                            break;
                        case 4:
                            Environment.Exit(0);
                            break;
                        default:
                            break;
                    } //End Switch

                } // end of while


            }
            catch (Exception e)
            {
                Console.WriteLine("System Exception : " + e.Message);
            }

        } // end of mainMenu

     



    } //class



} //namespace