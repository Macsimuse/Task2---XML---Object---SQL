using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassContainer
{
    [Serializable]
   public class Position
    {
       
        public long Latitude { get; set; }
        public long Altitude { get; set; }
        public DateTime Time { get; set; }
    }
}
