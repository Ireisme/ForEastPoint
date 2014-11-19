using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PredPreySquares
{
    public class Snake : Predator
    {
        public Snake(int id)
        {
            ds = Description.SNAKE;
            img = Resource1.cobra5;
            identifier = id;
            turncount = 0;
            hasEaten = false;
        }
        
        public override bool Eat(Edible se)
        {
            if (se is SnakeEdible)
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
