using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using static System.Net.Mime.MediaTypeNames;

namespace GeneralMandel
{
    class MPlot
    {
        public Settings set;
        public List<MPoint> points;
        public int zoomnum,nboundary,ntot;
        public Decimal zoomfactor;
        public Complex cplug,lastplug;
        public ComplexOp cop;
        public Decimal[] starter,curpos;
        public Decimal deltax,deltay,smallx,smally;
        public int id;
        public List<int> boundinds;
        public TimeSpan dt;
        public DateTime starttime, endtime;
        public Stopwatch stopwatch;
        
        public Bitmap GetDataPicture(int w, int h, byte[] data)
        {
            Bitmap pic = new Bitmap(w, h, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            int aria = 0;
            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    
                    
                    Color c = Color.FromArgb(
                       data[aria],
                       data[aria + 1],
                       data[aria + 2],
                       data[aria + 3]
                    );
                    pic.SetPixel(y, x, c);
                    aria += 4;
                }
            }

            return pic;
        }
        public void MakePoints()
        {
            stopwatch = new Stopwatch();
            starttime = DateTime.Now;
            stopwatch.Start();
            nsteps = set.nsteps;
            random = new System.Random();
            ntot = nsteps * nsteps;
            opm = new PolyOp();
            cop = new ComplexOp();
            boundinds = new List<int>();
            boundinds.Clear();
            opm.set = set;
            points = new List<MPoint>();
            zoomfactor = (Decimal)(Math.Pow((double)set.zoomfactoreach, zoomnum));
            opm.StartUp();
            if(zoomnum == 0)
            {
                lastplug = new Complex();
                lastplug.num = new Decimal[2] { Decimal.Zero, Decimal.Zero };
                lastplug.cpow = 0;
                starter = new Decimal[2] { set.boundingrec[0],set.boundingrec[1]};
                deltax = (set.boundingrec[2] - set.boundingrec[0]) / (Decimal)set.nsteps;
                deltay = (set.boundingrec[3] - set.boundingrec[1]) / (Decimal)set.nsteps;
                curpos = new Decimal[2] { set.boundingrec[0], set.boundingrec[1] };
                GoThrough();
            } else
            {
                deltax = (set.boundingrec[2] - set.boundingrec[0]) / (Decimal)(set.nsteps * zoomfactor);
                deltay = (set.boundingrec[3] - set.boundingrec[1]) / (Decimal)(set.nsteps * zoomfactor);
                starter = new Decimal[2] { lastplug.num[0] - (Decimal)(deltax * set.nsteps / 2) , lastplug.num[1] - (Decimal)(deltay * set.nsteps / 2) };
                curpos = new Decimal[2] { lastplug.num[0] - (Decimal)(deltax * set.nsteps / 2), lastplug.num[1] - (Decimal)(deltay * set.nsteps / 2) };
                GoThrough();
            }
            stopwatch.Stop();
            dt = stopwatch.Elapsed;
            stopwatch.Reset();
            endtime = DateTime.Now;
           
        }
        public void CycleIt()
        {
            bool ranoo = false;
            while (!ranoo)
            {
                rindex = random.Next(0, nsteps * nsteps);
                if( points[rindex].isboundary)
                {
                    ranoo = true;
                }
               
            }
            if(false)
            cop.PrintNumber(points[rindex].cval);
            Console.WriteLine("ISBOUNDARY " + points[rindex].isboundary);
            lastplug = points[rindex].cval;
            zoomnum++;

        }
        public System.Random random;
        public int nsteps,newind,dk,dl,ind,newl,newk,noutside,ninside,nop,nip,rindex,arind;
        public PolyOp opm;
        
        public byte[] coldata;

        public Bitmap moopo;
        public void GoThrough()
        {
            opm.cvalplug = new Complex();
            opm.cvalplug.cpow = 0;
            ind = 0;
            noutside = 0;
            ninside = 0;
            coldata = new byte[nsteps * nsteps * 4];
            
            arind = 0;
            for (int ii = 0; ii < nsteps; ii++)
            {
                if(true)
                Console.WriteLine("STEP " + ii + " out of " + nsteps);
                for(int jj = 0; jj < nsteps; jj++)
                {
                    opm.cvalplug = new Complex();
                    opm.cvalplug.cpow = 0;
                    opm.cvalplug.num = new Decimal[2] { curpos[0], curpos[1] };
                    if(false)
                    cop.PrintNumber(opm.cvalplug);
                    opm.PlugIn(0, opm.set.poly);
                    points.Add(new MPoint());
                    
                    points[ind].ijvals = new int[2] { ii, jj };
                    points[ind].inset = opm.inset;
                    if (points[ind].inset)
                    {
                        ninside++;
                    } else
                    {
                        noutside++;
                    }
                    
                    points[ind].ittbreak = opm.curitt;
                    points[ind].zoomnum = zoomnum;
                    points[ind].cval = new Complex();
                    points[ind].cval.num = new Decimal[2] { curpos[0], curpos[1] };
                    points[ind].color = new ColorVo();
                    points[ind].color.hue = opm.col.hue;
                    points[ind].color.saturation = opm.col.saturation;
                    points[ind].color.value = opm.col.value;
                    points[ind].color.rgb = opm.col.rgb;
                    coldata[arind+1] = (byte)points[ind].color.rgb[0];
                    coldata[arind+2] = (byte)points[ind].color.rgb[1];
                    coldata[arind+3] = (byte)points[ind].color.rgb[2];
                    
                    coldata[arind] = 255;
                    arind += 4;
                    points[ind].neighbors = new List<int>();
                    points[ind].nneigh = 0;
                    for(int kk = 0; kk < 3; kk ++)
                    {
                        dk = kk -1;
                       
                        newk = ii + dk;
                        for(int ll = 0; ll < 3; ll ++)
                        {
                            dl = ll -1;
                            
                            newl = jj + dl;
                            newind = ind + dl;
                            newind += dk * nsteps;
                            bool kv = false;
                            if(ind == 6845&& false)
                            {
                                kv = true;
                                Console.WriteLine("COORDS: " + points[ind].ijvals[0] + "," + points[ind].ijvals[1] + " " + ind);
                            }
                            if(newind < 0 || newind >= nsteps * nsteps || newind == ind)
                            {
                          
                            } else
                            {
                               if(newk < 0 || newl < 0 || newk >= nsteps || newl >= nsteps)
                                {

                                } else
                                {
                                    if (kv)
                                    {
                                        Console.WriteLine("NOOB " + newk + "," + newl + " " + newind);
                                    }
                                    points[ind].neighbors.Add(newind);
                                    points[ind].nneigh++;
                                }
                            }
                        }
                    }
                    if(false)
                    cop.PrintNumber(points[0].cval);
                    if(false)
                    Console.WriteLine(points[ind].ittbreak + " " + points[ind].color.rgb[0] + " " + points[ind].color.rgb[1] + " " + points[ind].color.rgb[2] + " " + points[ind].inset);
                    curpos[0] += deltax;
                   
                    ind++;
                }
                curpos[0] = starter[0];
                curpos[1] += deltay;
            }
            nboundary = 0;
            moopo = GetDataPicture(nsteps, nsteps, coldata);
            moopo.Save(@"C:\Users\Pizzamine98\Desktop\genmandel\plot" + zoomnum + ".jpeg",System.Drawing.Imaging.ImageFormat.Jpeg);
            for (int ii = 0; ii < nsteps*nsteps; ii++)
            {
                nop = 0;
                nip = 0;
                if (points[ii].inset && false)
                {
                    Console.Write("NNEIGH: " + points[ii].nneigh);
                }
                foreach(int milk in points[ii].neighbors)
                {
                    if (!points[milk].inset)
                    {
                        
                        nop++;
                    } else
                    {
                        nip++;
                        
                    }
                }
                if (points[ii].inset && false)
                {
                    Console.WriteLine(" NOP = " + nop + " NIP = " + nip);
                }
                if(nop != 0 && nip != 0 && points[ii].inset)
                {
                    nboundary++;
                    points[ii].isboundary = true;
                    boundinds.Add(ii);
                    if(true)
                    Console.WriteLine("BOUND " + ii + " " + points[ii].cval.num[0] + " " + points[ii].cval.num[1]);
                } else
                {
                    points[ii].isboundary = false;
                }
            }
            Console.WriteLine("ZOOMNUM " + zoomnum + " DATA:");
            Console.WriteLine("NTOT: " + ntot + " NOUTSIDE = " + noutside + " NINSIDE = " + ninside + " NBOUNDARY = " + nboundary);
        }
    }
}
