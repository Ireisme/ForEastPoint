using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PredPreySquares
{
    public class Scorpion : Predator
    {
        public Scorpion(int id)
        {
            ds = Description.SCORPION;
            img = Resource1.scorpion;
            identifier = id;
            turncount = 0;
            hasEaten = false;
        }
        
        public override bool Eat(Edible se)
        {
            if (se is ScorpionEdible)
            {
                hasEaten = true;
                return true;
            }
            else
                return false;
        }

        public override void Move(Point nextpos)
        {

            currentPos = nextpos;
            turncount++;
            
        }

    }
}
