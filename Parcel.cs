using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Coursework
{
    public class Parcel
    {
        
        private string delivery_id;
        private string postcode;
        private string address;
        private string name;

        //Contructor that gets 2 the address and name and executes a method in the class
        public Parcel(string address, string name)
        {
            this.address = address;
            this.name = name;
            addToPostcode();
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Delivery_id
        {
            get { return delivery_id; }
            set { delivery_id = value; }
        }

        public string Postcode
        {
            get { return postcode; }
            set { postcode = value; }
        }
       
        //This divides the postcode from the name delivery Id (as the postcode is the important thing)
        public void addToPostcode() { 
            Postcode = this.address.Split()[0];
            Delivery_id = this.address.Split()[1];
        }
    }
}
