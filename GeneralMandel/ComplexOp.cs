using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralMandel
{
    class ComplexOp
    {
        public Complex result,result2;
        public Complex Add(Complex c0, Complex c1)
        {
            result = new Complex();
            result.num = new Decimal[2];
            result.num[0] = c0.num[0] + c1.num[0];
            result.num[1] = c0.num[1] + c1.num[1];
            result.cpow = c0.cpow;
            return result;
        }
        public Complex Multiply(Complex c0, Complex c1)
        {
            result = new Complex();
            result.num = new Decimal[2];
            result.num[0] = (c0.num[0] * c1.num[0]) - (c0.num[1] * c1.num[1]);
            result.num[1] = (c0.num[0] * c1.num[1]) + (c0.num[1] * c1.num[0]);
            result.cpow = c0.cpow + c1.cpow;
            return result;
        }
        public Complex Pow(Complex c0, int powin)
        {
            result2 = new Complex();
            result2.num = new Decimal[2];
            result2.num[0] = c0.num[0];
            result2.num[1] = c0.num[1];
            result2.cpow = c0.cpow;
            if (powin == 1)
            {
                
            }
            else
            {
                for (int ayyy = 1; ayyy < powin; ayyy++)
                {
                    result2 = Multiply(result2, c0);
                }
            }
            return result2;
        }

        public void PrintNumber(Complex c0)
        {
            string linn = "Number " + c0.num[0] + " + " + c0.num[1] + "i";
            if(c0.cpow != 0)
            {
                linn = linn + "c";
                if(c0.cpow != 1)
                {
                    linn = linn + "^" + c0.cpow;
                }
            }
            Console.WriteLine(linn);
        }
        public Decimal Mag(Complex c0)
        {
            Decimal magout = Decimal.Zero;
            double[] fool = new double[2] { (double)c0.num[0], (double)c0.num[1] };
            magout = (Decimal)Math.Sqrt((fool[0] * fool[0]) + (fool[1] * fool[1]));
            return magout;

        }
    }
}
