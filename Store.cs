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


      
    }
}
