using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    public class Courier
    {
        public List<Parcel> parcel_list = new List<Parcel>();
        public List<string> postcodes = new List<string>();
        private int id;
        private string postcode;

        //Courier constructor that gets 3 values and executes the Add_area method
        public Courier(int id, List<string> postcodes)
        {
            this.id = id;
            this.postcodes = postcodes;
            addArea();
            fixArray();
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        //This value changes depending on the type of Courier
        public virtual int Num_parcels { get; }

        //This here adds the postcode/s for the area/s the courier has to travel through
        private void addArea() => postcodes.Add(postcode);


        //This method removes an empty area that is added when createing a courier
        private void fixArray()
        {
            postcodes.RemoveAt(postcodes.Count - 1);
        }
    }
}
