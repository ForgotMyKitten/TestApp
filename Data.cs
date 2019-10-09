using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    public class Data
    {
        public int A { get; set; }
        public int B { get; set; }
        public int Sum
        {
            get
            {
                return A + B;
            }
        }

        public Data(int a, int b)
        {
            this.A = a;
            this.B = b;
        }
    }
}
