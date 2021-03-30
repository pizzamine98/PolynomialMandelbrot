using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GeneralMandel
{
    class PlotExporter
    {
        public Settings set;
        public string root, foldpath,foldpath2,path,path2,json;
        public RunInfo info;
        public string[] things;
        public List<string> csvlines;
        public int zoomnum,runsetid,runid,nthi,maxrunid,cpoz;
        public void Setup(MPlot plotin)
        {
            root = set.root;
            foldpath = root + "rundata\\info\\";
            foldpath2 = root + "rundata\\csv\\";

            string[] existing = Directory.GetFiles(foldpath);
            runid = existing.Length;
            if(existing.Length == 0)
            {
                runsetid = 0;
            } else
            {
                maxrunid = 0;
                for(int ig = 0; ig < existing.Length; ig++)
                {
                    things = existing[ig].Split("_");
                    nthi = things.Length;
                    cpoz = int.Parse(things[nthi - 2]);
                    if(cpoz > maxrunid)
                    {
                        maxrunid = cpoz;
                    }
                }
                runsetid = maxrunid + 1;
            }
            plotin.zoomnum = 0;
            while(plotin.zoomnum <= set.nzoom)
            {
                Console.WriteLine();
                Console.WriteLine("ZOOM NUMBER: " + plotin.zoomnum);
                Console.WriteLine();
                plotin.set = set;
                plotin.MakePoints();
                info = new RunInfo();
                info.runid = runid;
                info.settingid = set.id;
                info.zoomnum = plotin.zoomnum;
                info.zoomfactor = plotin.zoomfactor;
                info.starttime = plotin.starttime;
                info.runsetid = runsetid;
                info.endtime = plotin.endtime;
                info.dt = plotin.dt;
                info.npoints = plotin.points.Count;
                info.nsteps = plotin.nsteps;
                info.polyused = plotin.set.poly;
                info.center = plotin.lastplug;
                csvlines = new List<string>();
                line = "xval,yval,inset,isboundary,ittbreak,r,g,b";
                csvlines.Add(line);
                foreach(MPoint pill in plotin.points)
                {
                    line = "" + pill.cval.num[0] + "," + pill.cval.num[1] + "," + pill.inset + "," + pill.isboundary + "," + pill.color.rgb[0] +
                        "," + pill.color.rgb[1] + "," + pill.color.rgb[2];
                    csvlines.Add(line);
                }
                path = foldpath + "runinfo_" + info.runsetid + "_" + info.zoomnum + ".json";
                json = JsonConvert.SerializeObject(info);
                System.IO.File.WriteAllText(path, json);
                path2 = foldpath2 + "rundata_" + info.runsetid + "_" + info.zoomnum + ".csv";
                System.IO.File.WriteAllLines(path2, csvlines.ToArray());
                plotin.CycleIt();
            }
            
            
        }
        public string line;
    }
}
