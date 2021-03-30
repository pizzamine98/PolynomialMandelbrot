using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralMandel
{
    class Complex
    {
        public Decimal[] num;
        public int cpow;
        public void Set0()
        {
            num = new Decimal[2];
            num[0] = Decimal.Zero;
            num[1] = Decimal.Zero;
            cpow = 0;
        }
        
    }
}
