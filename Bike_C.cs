using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    class Bike_C : Courier
    {
        public override int Num_parcels => 10;

        public Bike_C(int id, List<string> postcodes) : base(id, postcodes)
        {

        }
    }
}
