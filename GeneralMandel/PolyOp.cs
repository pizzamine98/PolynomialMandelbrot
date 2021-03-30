using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Text;

namespace GeneralMandel
{
    class PolyOp
    {
        public Polynomial res,der0;
        public List<Complex> newco;
        public ComplexOp cop;
        public ColorConv conv;
        public void StartUp()
        {
            cop = new ComplexOp();
            hardd = Decimal.MaxValue / 100;
            der0 = DerivativeC(set.poly);
            donuts = new double[3];
            doom = new double[3];
            conv = new ColorConv();
            cook = new ColorUtils();
        }
        public Polynomial Derivative(Polynomial pin)
        {
            newco = new List<Complex>();
            for(int ayd = 1; ayd < pin.ncoeffs; ayd++)
            {
                Complex cold = new Complex();
                Complex hot = new Complex();
                hot.num = new Decimal[2];
                string moo = "" + ayd;
                hot.num[0] = Decimal.Parse(moo);
                hot.num[1] = Decimal.Zero;
                hot.cpow = 0;
                
                cold = cop.Multiply(hot, pin.coeffs[ayd]);
                newco.Add(cold);
                int aydd = ayd - 1;
                if (false)
                {
                    Console.WriteLine("COEFF " + aydd + ":");
                    cop.PrintNumber(cold);
                }
            }
            res = new Polynomial();
            res.coeffs = newco;
            res.ncoeffs = newco.Count;
            return res;
        }
        public Polynomial DerivativeC(Polynomial pin)
        {
            newco = new List<Complex>();
            for (int ayd = 0; ayd < pin.ncoeffs; ayd++)
            {
                Complex cold = new Complex();
                Complex hot = new Complex();
                hot.num = new Decimal[2];
                string moo = "" + pin.coeffs[ayd].cpow;
                hot.num[0] = Decimal.Parse(moo);
                hot.num[1] = Decimal.Zero;
                hot.cpow = 0;

                cold = cop.Multiply(hot, pin.coeffs[ayd]);
                newco.Add(cold);
                int aydd = ayd - 1;
                if (false)
                {
                    Console.WriteLine("COEFF " + aydd + ":");
                    cop.PrintNumber(cold);
                }
            }
            res = new Polynomial();
            res.coeffs = newco;
            res.ncoeffs = newco.Count;
            return res;
        }
        public Settings set;
        public int curitt,ittbreak;
        public Decimal magcur;
        public ColorUtils cook;
        public bool inset;
        public double bvo,cvo,zvo;
        public Complex val0, val1, val2,cvalplug;
        public ColorVo col;
        public void PlugIn(int modein, Polynomial pin)
        {
            curitt = 0;
            magcur = 0;
            // modes: 0 - plotting, 1 - bval
            val0 = set.startval;
            
            if(modein == 0)
            {
                inset = true;
                ittbreak = -1;
                
                while(curitt < set.nitts && magcur <= set.checktill)
                {
                   
                    curitt++;
                    if(false)
                    Console.WriteLine("MAGCUR: " + magcur + " ITOO " + curitt);
                    val1 = new Complex();
                    val1.num = new decimal[2];val1.num[0] = Decimal.Zero; val1.num[1] =  Decimal.Zero;
                    val1.cpow = 0;
                    for(int az = 0; az < pin.ncoeffs; az++)
                    {
                        if(az != 0)
                        {
                            
                            val2 = cop.Pow(val0, az);
                        } else
                        {
                            val2 = new Complex();
                            val2.Set0();
                            val2.num[0] = Decimal.Parse("1.0");
                        }
                        if(pin.coeffs[az].cpow != 0)
                        val2 = cop.Multiply(cop.Pow(cvalplug, pin.coeffs[az].cpow),val2);

                        val2 = cop.Multiply(pin.coeffs[az], val2);
                        val1 = cop.Add(val1, val2);
                        
                    }
                    magcur = cop.Mag(val1);
                    if (debug0 )
                    {
                        if (curitt < 10 && false)
                        {
                            Console.WriteLine("Mode0 CURITT " + curitt);
                            Console.WriteLine();
                            Console.WriteLine("VAL0: magcur = " + cop.Mag(val0));
                            cop.PrintNumber(val0);
                            Console.WriteLine("VAL1: magcur = " + magcur);
                            cop.PrintNumber(val1);
                            }
                    }
                    if(magcur > set.checktill)
                    {
                        inset = false;
                        ittbreak = curitt;
                    }
                    val0 = new Complex();
                    val0.num = new Decimal[2];
                    val0.num[0] = val1.num[0];
                    val0.num[1] = val1.num[1];
                    
                }
                
                doom[0] = ((double)255.0 * (double)curitt / (double)set.nitts);
                doom[1] = 255.0;
                doom[2] = 255.0;
                
                if(set.nitts == curitt)
                {
                    doom[2] = 0.0;
                }
                col = new ColorVo();
                col.hue = (int)doom[0];
                col.saturation = (int)doom[1];
                col.value = (int)doom[2];
                Color max = ColorUtils.HsvToRgb(doom[0], doom[1], doom[2]);
                col.rgb = new int[3] { (int)(max.R), (int)(max.G), (int)(max.B) };
                if (false)
                {
                    Console.WriteLine("DOOM " + doom[0] + " " + doom[1] + " " + doom[2]);
                    Console.WriteLine("HUE = " + col.hue + " SAT = " + col.saturation + " VALUE = " + col.value);
                    Console.WriteLine("R = " + col.rgb[0] + " G = " + col.rgb[1] + " B = " + col.rgb[2]);
                }
                    if (false)
                {
                    Console.WriteLine("NITTS: " + curitt);
                    Console.WriteLine(bvo);
                }
            } else if(modein == 1)
            {
              
                while (curitt < set.ncheckforbval && magcur <= set.checktill)
                {
                    curitt++;
                    val1 = new Complex();
                    val1.num = new Decimal[2]; val1.num[0] = Decimal.Zero; val1.num[1] = Decimal.Zero;
                    val1.cpow = 0;
                    for (int az = 0; az < pin.ncoeffs; az++)
                    {
                        if (az != 0)
                        {
                            if (false)
                            {
                                Console.WriteLine("PWOW " + " " + az + " VAL0");
                                cop.PrintNumber(val0);
                            }
                                val2 = cop.Pow(val0, az);
                            if (false)
                            {
                                Console.WriteLine("VAL2:");
                                cop.PrintNumber(val2);
                            }
                        }
                        else
                        {
                            val2 = new Complex();
                            val2.Set0();
                            val2.num[0] = Decimal.Parse("1.0");
                        }
                        if (pin.coeffs[az].cpow != 0)
                            val2 = cop.Multiply(cop.Pow(cvalplug, pin.coeffs[az].cpow), val2);

                        val2 = cop.Multiply(pin.coeffs[az], val2);
                        val1 = cop.Add(val1, val2);

                    }
                    magcur = cop.Mag(val1);
                    if (debug0)
                    {
                        if (curitt < 10)
                        {
                            Console.WriteLine("Mode1 CURITT " + curitt);
                            Console.WriteLine();
                            Console.WriteLine("VAL0: magcur = " + cop.Mag(val0));
                            cop.PrintNumber(val0);
                            Console.WriteLine("VAL1: magcur = " + magcur);
                            cop.PrintNumber(val1);
                        }
                    }
                    
                    val0 = new Complex();
                    val0.num = new Decimal[2];
                    val0.num[0] = val1.num[0];
                    val0.num[1] = val1.num[1];

                }

            }
        }
        
        public double[] doom;
        public double[] donuts;
        public Decimal hardd;
        public bool debug0;
    }
}
