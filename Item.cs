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
        private int quantity;

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

        public void setQuantity(int quantity) {
            this.quantity = quantity;
        }

        public int getQuantity(int quantity) {
            return quantity;
        }

        public Item(string itemName, int itemId, int itemQuantity)
        {
            id = itemId;
            name = itemName;
            quantity = itemQuantity;
        }
       
        public string listStore(int itemInStoreID) {

            string storeName = "FSAD";
            DATABASE ACCESS HERE TO GET STORE NAME USING PARAMETER STORE ID
            return storeName;
        }

    }
}
