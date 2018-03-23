using System;
namespace Assignment1
{
    public class Store
    {
        public string name { get; set; }
       
        private int id { get; set; }

        public Store(string name, int id)
        {
            this.name = name;
            this.id = id;
        }
    }
}
