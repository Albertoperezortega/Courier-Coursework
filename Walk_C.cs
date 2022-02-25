using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Coursework
{
    class Walk_C : Courier
    {
        public override int Num_parcels => 5;

        public Walk_C(int id, List<string> postcodes) : base(id, postcodes)
        {

        }
    }
}
