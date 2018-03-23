using System;
namespace Assignment1
{
    public class Request
    {
        private int id;
        private Item reqItem;
        private int quantity;
        private int currentStock;
        private bool available;


        public Request(int id, Item reqItem, int quantity, int currentStock, bool available)
        {
            this.id = id;
            this.reqItem = reqItem;
            this.quantity = quantity;
            this.currentStock = currentStock;
            this.available = available;

        }
    }

 

}
