using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PredPreySquares
{
    public abstract class Organism
    {
        public Description ds;

        public Image img;

        public int turncount { get; set; }

        public bool alive { get; set; }

        public Point currentPos {get; set;}

        

        
    
    }
}
