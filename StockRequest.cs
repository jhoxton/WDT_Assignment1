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


        public StockRequest(int requestID, int storeID, int productID, int quantity, bool available)
        {
            this.requestID = requestID;
            this.storeID = storeID;
            this.productID = productID;
            this.quantity = quantity;
            this.available = available;
        }


        //public int getrequestID()
        //{
        //    return requestID;
        //}


        //public int setRequestID(int requestID) {
        //    //this.requestID = requestID;
        //using (var con = new SqlConnection("server=wdt2018.australiaeast.cloudapp.azure.com;uid=s3609685;database=s3609685;pwd=abc123"))

        //{
        //    con.Open();
        //    //Opens said "object"

        //    SqlCommand cmd = new SqlCommand();


        //    String query = "select max(StockRequestID) from StockRequest;";
        //    cmd.Connection = con;
        //    cmd.CommandText = query;
        //    String retrievedID = cmd.ExecuteScalar().ToString();

        //    int maxID = int.Parse(retrievedID);
        //        maxID = maxID + 1;
           
        //    con.Close();
        //        requestID = maxID;
        //        Console.WriteLine("Next stock request id should be " + requestID);

        //}
        //    //return requestID;
        //}

        public int RequestID { get => requestID; set => requestID = value; }
        public int StoreID { get => storeID; set => storeID = value; }
        public int ProductID { get => productID; set => productID = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public bool Available { get => available; set => available = value; }

        //public int getRequestID()
        //{
        //    return requestID;
        //}

        //public void setRequestId(int requestID)
        //{
        //    requestID = requestID +1;
        //}
    }


}