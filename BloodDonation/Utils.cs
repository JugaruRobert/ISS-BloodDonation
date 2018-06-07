using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation
{
    class Utils
    {
        public string Type { get; set; }
        public Guid ID { get; set; }

        public Utils() { }

        public Utils(string t, Guid i)
        {
            Type = t;
            ID = i;
        }
    }
}
