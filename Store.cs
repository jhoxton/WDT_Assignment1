using System;
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
            this.name = name;
            this.id = id;
        }
    }
}
