using System;

namespace GeneralMandel
{
    class Program
    {
        static void Main(string[] args)
        {
            int which = 2;
            if (which == 0)
            {
                SettingsFileLoader loado = new SettingsFileLoader();
                loado.root = @"C:\Users\Pizzamine98\Desktop\genmandel\";
                loado.LoadSettingsFiles();
                loado.MakeJsons();
                PolyOp opp = new PolyOp();
                opp.set = loado.sets[0];
                opp.StartUp();
                Polynomial deriv = opp.DerivativeC(opp.set.poly);
                opp.cvalplug = new Complex();
                opp.cvalplug.num = new Decimal[2];
                opp.cvalplug.num[0] = Decimal.Parse("0.1");
                opp.cvalplug.num[1] = Decimal.Parse("0.1");
                opp.cvalplug.cpow = 0;
                opp.debug0 = true;
                opp.PlugIn(0, opp.set.poly);
                
            } else if(which == 1)
            {
                SettingsFileLoader loado = new SettingsFileLoader();
                loado.root = @"C:\Users\Pizzamine98\Desktop\genmandel\";
                loado.LoadSettingsFiles();
                loado.MakeJsons();
                MPlot poo = new MPlot();
                poo.set = loado.sets[0];
                PlotExporter export = new PlotExporter();
                export.set = loado.sets[0];
                export.Setup(poo);
            } else if(which == 2)
            {
                SettingsFileLoader loado = new SettingsFileLoader();
                loado.root = @"C:\Users\Pizzamine98\Desktop\genmandel\";
                loado.LoadSettingsFiles();
                loado.MakeJsons();
                SimpleItterator simp = new SimpleItterator();
                int whichid = 3;
                int cores = loado.GetIndexForGivenIndex(whichid);
                simp.set = loado.sets[cores];
                PolyOp opp = new PolyOp();
                opp.set = loado.sets[cores];
                simp.set.nzoom = 10;
                opp.StartUp();
                simp.opm = opp;
                simp.zoomnum = 0;
                simp.h = 1080;
                simp.w = 1920;
                while(simp.zoomnum < simp.set.nzoom)
                {
                    
                    simp.Setup();
                    simp.zoomnum++;
                }
            }
        }
    }
}
