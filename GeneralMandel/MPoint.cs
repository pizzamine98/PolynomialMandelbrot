using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralMandel
{
    class MPoint
    {
        public Complex cval;
        public bool inset,isboundary;
        public int zoomnum,ittbreak,nneigh;
        public ColorVo color;
        public int[] ijvals;
        public List<int> neighbors;
    }
}
