using System;
namespace Assignment1
{
    public class Item
    {
        private string name;
        private int id;

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


        public Item(string itemName, int itemId)
        {
            id = itemId;
            name = itemName;
        }

        public Item()
        {
        }
    }
}
