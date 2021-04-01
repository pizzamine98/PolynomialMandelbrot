using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json.Serialization;

namespace GeneralMandel
{
    class SettingsFileLoader
    {
        public string root,folderpath,sool;
        public string[] setpaths;
        public int nsettingsfiles,nlines;
        public string[] lines;
        public void LoadSettingsFiles()
        {
            folderpath = root + "settingsfiles";
            setpaths =Directory.GetFiles(folderpath);
            nsettingsfiles = setpaths.Length;
            sets = new List<Settings>();
            for(int ayy = 0; ayy < nsettingsfiles; ayy++)
            {
                set = new Settings();
                sool = setpaths[ayy];
                lines = System.IO.File.ReadAllLines(setpaths[ayy]);
                nlines = lines.Length;
                set.notes = new List<string>();
                for(int byy = 0; byy < nlines; byy++)
                {
                    ReadLine(lines[byy]);
                }
                set.poly = new Polynomial();
                set.poly.coeffs = new List<Complex>();
                for(int byy = 0; byy < set.coefficients.Count; byy++)
                {
                    set.poly.coeffs.Add(set.coefficients[byy]);
                }
                set.poly.ncoeffs = set.poly.coeffs.Count;
                sets.Add(set);
            }
        }
        public int GetIndexForGivenIndex(int indexin)
        {
            for(int ayy = 0; ayy < nsettingsfiles; ayy++)
            {
                if(sets[ayy].id == indexin)
                {
                    return ayy;
                }
            }
            return 0;
        }
        public string path;
        public List<Settings> sets;
        public void MakeJsons()
        {
            folderpath = root + "settingsjsons";

            for(int ayy = 0; ayy < nsettingsfiles; ayy++)
            {
                path = folderpath + "\\settings" + sets[ayy].id + ".json";
                json = JsonConvert.SerializeObject(sets[ayy]);
                System.IO.File.WriteAllText(path, json);
            }
            
        }
        public string json;
        public Settings set;
        public void ReadLine(string linein)
        {
            if (linein.StartsWith("//"))
            {
                set.notes.Add(linein);
            } else
            {
                things = linein.Split(" = ");
                if (things[0].Equals("name"))
                {
                    set.name = things[1];
                } else if (things[0].Equals("root"))
                {
                    set.root = @"" + things[1];
                } else if (things[0].Equals("nitts"))
                {
                    set.nitts = int.Parse(things[1]);
                } else if (things[0].Equals("coeffs"))
                {
                    string[] things2 = things[1].Split(":");
                    List<Complex> cough = new List<Complex>();
                    for(int kyy = 0; kyy < things2.Length; kyy++)
                    {
                        string[] things3 = things2[kyy].Split(",");
                        Complex compp = new Complex();
                        compp.num = new Decimal[2];
                        compp.num[0] = System.Convert.ToDecimal(things3[0]);
                        compp.num[1] = System.Convert.ToDecimal(things3[1]);
                        compp.cpow = int.Parse(things3[2]);
                        cough.Add(compp);
                    }
                    set.coefficients = cough;
                } else if (things[0].Equals("zoom_factor_each"))
                {
                    set.zoomfactoreach = System.Convert.ToDecimal(things[1]);
                } else if (things[0].Equals("n_zooms"))
                {
                    set.nzoom = int.Parse(things[1]);
                } else if (things[0].Equals("n_steps"))
                {
                    set.nsteps = int.Parse(things[1]);
                } else if (things[0].Equals("bounding_rectangle"))
                {
                    set.boundingrec = new Decimal[4];
                    string[] things2 = things[1].Split(",");
                   
                    for(int kyy = 0; kyy < 4; kyy++)
                    {
                        set.boundingrec[kyy] = System.Convert.ToDecimal(things2[kyy]);
                    }
                } else if (things[0].Equals("check_point_upto_magnitude")){
                    set.checktill = Decimal.Parse(things[1]);
                } else if (things[0].Equals("n_itterate_for_bval"))
                {
                    set.ncheckforbval = int.Parse(things[1]);
                } else if (things[0].Equals("id"))
                {
                    set.id = int.Parse(things[1]);
                } else if (things[0].Equals("starting_value"))
                {
                    set.startval = new Complex();
                    string[] things2 = things[1].Split(",");
                    set.startval.num = new Decimal[2];
                    set.startval.num[0] = System.Convert.ToDecimal(things2[0]);
                    set.startval.num[1] = System.Convert.ToDecimal(things2[1]);
                    set.startval.cpow = int.Parse(things2[2]);
                } else
                {
                    Console.WriteLine("ERROR FOR READING SETTINGSFILE " + sool);
                    Console.WriteLine(linein);
                }
            }
        }
        public string[] things;
    }
    
}
