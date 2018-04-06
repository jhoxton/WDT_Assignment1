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
            //SqlConnection sqlOp = new SqlConnection("server=wdt2018.australiaeast.cloudapp.azure.com;uid=s3609685;database=s3609685;pwd=abc123");
            //pagingTest();
            mainMenu();           
        }



        public static void pagingTest()
        {
            var list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var list2 = new List<int>();

            const int pageSize = 3;
            var pageOffset = 0;

            while(true) 
            { 


                ///// LINQ METHOD

                foreach (var x in list.Skip(pageOffset).Take(pageSize).ToList()) {
                    Console.WriteLine(x);
                }

                ////
                //for (int i = pageOffset; i < list.Count && i - pageOffset < pageSize; i++)//Copied directly from Matthews example in tute
                    
                //{
                //    Console.WriteLine("Item is " + i);

                //}
                Console.WriteLine("Press N for next page: ");
                var input =Console.ReadLine();

                if(input.ToUpper() =="N") {
                    pageOffset += pageSize;
                    if(pageOffset >= list.Count)
                    {
                        pageOffset = 0;
                    }              
                }
            }







        }

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