using System;
namespace Assignment1
{
    public class StockRequest
    {
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

        public int RequestID { get => requestID; set => requestID = value; }
        public int StoreID { get => storeID; set => storeID = value; }
        public int ProductID { get => productID; set => productID = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public bool Available { get => available; set => available = value; }
    }
}
