using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    class Van_C : Courier
    {
        public override int Num_parcels => 100;

        public Van_C(int id, List<string> postcodes) : base(id, postcodes)
        {

        }
    }
}
