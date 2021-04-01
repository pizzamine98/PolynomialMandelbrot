using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Diagnostics;
namespace GeneralMandel
{
    class SimpleItterator
    {
        public int nsteps,ncold,zoomnum,w,h;
        public Settings set;
        public byte[,,] colz;
        
        public bool hasome;
        public System.Random rando;
        public Decimal zoomfactor,deltax,deltay;
        public Decimal[] starter, curpos;
        public Complex lastplug;
        public PolyOp opm;
        public Stopwatch watch,watch2;
        public TimeSpan dt,dtper;
        public void Setup()
        {
            watch = new Stopwatch();
            watch2 = new Stopwatch();
            watch.Start();
            rando = new System.Random();
            hasome = false;
            nsteps = set.nsteps;
            ncold = h * w * 4;
            colz = new byte[h, w, 4];
            zoomfactor = (Decimal)(Math.Pow((double)set.zoomfactoreach, zoomnum));
            if (zoomnum == 0)
            {
                lastplug = new Complex();
                lastplug.num = new Decimal[2] { Decimal.Zero, Decimal.Zero };
                lastplug.cpow = 0;
                starter = new Decimal[2] { set.boundingrec[0], set.boundingrec[1] };
                deltax = (set.boundingrec[2] - set.boundingrec[0]) / (Decimal)w;
                deltay = (set.boundingrec[3] - set.boundingrec[1]) / (Decimal)h;
                curpos = new Decimal[2] { set.boundingrec[0], set.boundingrec[1] };
                
            }
            else
            {
                deltax = (set.boundingrec[2] - set.boundingrec[0]) / (Decimal)(w * zoomfactor);
                deltay = (set.boundingrec[3] - set.boundingrec[1]) / (Decimal)(h * zoomfactor);
                starter = new Decimal[2] { lastplug.num[0] - (Decimal)(deltax * w / 2), lastplug.num[1] - (Decimal)(deltay * h / 2) };
                curpos = new Decimal[2] { lastplug.num[0] - (Decimal)(deltax * w / 2), lastplug.num[1] - (Decimal)(deltay * h / 2) };
                
            }
            int hill = (int)(Math.Sqrt((h * h) + (w * w)));
            
            for (int ii = 0; ii < h; ii++)
            {
                watch2.Start();
                int stepnu = ii + 1;
                
                for(int jj = 0; jj < w; jj++)
                {
                    opm.cvalplug = new Complex();
                    opm.cvalplug.cpow = 0;
                    opm.cvalplug.num = new Decimal[2] { curpos[0], curpos[1] };
                    
                    
                    opm.PlugIn(0, opm.set.poly);
                    colz[ii, jj, 0] = 255;
                    colz[ii, jj, 1] = (byte)opm.col.rgb[0];
                    colz[ii, jj, 2] = (byte)opm.col.rgb[1];
                    colz[ii, jj, 3] = (byte)opm.col.rgb[2];
                    if(ii != 0 && jj != 0)
                    {
                        if(colz[ii,jj,1] == 0 && colz[ii,jj,2] == 0 && colz[ii,jj,3] == 0)
                        {
                           
                            if (colz[ii, jj-1, 1] == 0 && colz[ii, jj-1, 2] == 0 && colz[ii, jj-1, 3] == 0)
                            {

                            } else
                            {
                                if (!hasome)
                                {
                                    lastplug = new Complex();
                                    lastplug.cpow = opm.cvalplug.cpow;
                                    hasome = true;
                                    lastplug.num = new Decimal[2] { curpos[0], curpos[1] };
                                } else
                                {
                                    int mook = rando.Next(0, hill);
                                    
                                    if(mook == 0)
                                    {
                                        Console.WriteLine("GOT ONE");
                                        lastplug = new Complex();
                                        lastplug.cpow = opm.cvalplug.cpow;
                                        lastplug.num = new Decimal[2] { curpos[0], curpos[1] };
                                    }
                                }
                            }
                        }
                    }
                    curpos[0] += deltax;
                }
                curpos[0] = starter[0];
                curpos[1] += deltay;
                watch2.Stop();
                dtper = watch2.Elapsed;
                watch2.Reset();
                if(ii%20 == 0)
                {
                    Console.WriteLine("Line " + stepnu + " out of " + h + " for zoomnum = " + zoomnum);
                    Console.WriteLine("TIME FOR LINE: " + dtper);
                }
                
            }
            watch.Stop();
            dt = watch.Elapsed;
            Console.WriteLine("TIME FOR THING TOTAL: " + dt);

            moopo = GetDataPicture();
            moopo.Save(set.root + "plot_" + set.id + "_" + zoomnum + ".jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
            
        }
        public Bitmap GetDataPicture()
        {
            Bitmap pic = new Bitmap(w, h, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            for(int y = 0; y < h; y++)
            {
                for(int x = 0; x < w; x++)
                {
                    
                    Color c = Color.FromArgb(colz[y, x, 1], colz[y, x, 2], colz[y, x, 3]);
                    pic.SetPixel(x, y, c);

                }
            }
            return pic;
        }
        public Bitmap moopo;
    }
    
}
