using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralMandel
{
    class Settings
    {
        public string name;
        public string root;
        public int nitts;
        public int nsteps;
        public List<Complex> coefficients;
        public Decimal zoomfactoreach;
        public int nzoom;
        public Decimal checktill;
        public Decimal[] boundingrec;
        public Complex startval;
        public int ncheckforbval;
        public int id;
        public List<string> notes;
        public Polynomial poly;
    }
}
