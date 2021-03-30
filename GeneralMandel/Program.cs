using System;

namespace GeneralMandel
{
    class Program
    {
        static void Main(string[] args)
        {
            int which = 1;
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
            }
        }
    }
}
