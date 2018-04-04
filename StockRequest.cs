using System;
using System.Data.SqlClient;

namespace Assignment1
{
    public class StockRequest
    {
        //static int requestID = requestID + 1;
        private int requestID;
        private int storeID;
        private int productID;
        private int quantity;
        private bool available;
        private string productName;
        private string storeName;


        public StockRequest(int requestID, int storeID, int productID, int quantity, bool available, string productName, string storeName)
        {
            this.requestID = requestID;
            this.storeID = storeID;
            this.productID = productID;
            this.quantity = quantity;
            this.available = available;
            this.ProductName = productName;
            this.StoreName = storeName;
        }


        public int RequestID { get => requestID; set => requestID = value; }
        public int StoreID { get => storeID; set => storeID = value; }
        public int ProductID { get => productID; set => productID = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public bool Available { get => available; set => available = value; }

        public string ProductName { get => productName; set => productName = value; }
        public string StoreName { get => storeName; set => storeName = value; }

        //public string getStoreName(String name){
        //    return storeName;
        //}


        public void findStoreName()//This is a cheap and dirty method to get store names without having to call the db again
        {
            switch (this.StoreID)
            {
                case 1:
                    StoreID = 1;
                    this.storeName = "Melbourne CBD";
                    //return storeName;
                    break;
                case 2:
                    StoreID = 2;
                    this.storeName = "North Melbourne";
                    //return storeName;
                    break;
                case 3:
                    StoreID = 3;
                    this. storeName = "East Melbourne";
                    break;
                case 4:
                    StoreID = 4;
                    this. storeName = "South Melbounre";
                    break;
                case 5:
                    StoreID = 5;
                    this.storeName = "West Melbourne";
                    //return storeName;
                    break;
                default:
                    break;
            } //switch
            //return storeName;
        }
    }



}