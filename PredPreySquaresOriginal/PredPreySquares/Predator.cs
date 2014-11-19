using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PredPreySquares
{
    public abstract class Predator : Organism
    {
        public int identifier { get; set; } //how to deal with when one leaves the board??, set the array entry to true

        public bool hasEaten { get; set; }
        
        public abstract bool Eat(Edible edible); //perhaps identify what the predator ate

        public abstract void Move(Point pt);
    
        //maybe add an identifier to use the hasmoved array
    }
}
