using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PredPreySquares
{
    public abstract class Prey : Organism
    {
        public abstract Direction Move(List<Direction> dirlist, Random rand);
    
    }
}
