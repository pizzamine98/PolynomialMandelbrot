using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralMandel
{
    class RunInfo
    {
        public int runid,runsetid;
        public int settingid,zoomnum;
        public DateTime starttime, endtime;
        public TimeSpan dt;
        public Decimal zoomfactor;
        public int npoints, nsteps;
        public Polynomial polyused;
        public Complex center;
    }
}
