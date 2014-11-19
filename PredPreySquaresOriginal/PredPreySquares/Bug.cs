using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PredPreySquares
{
    public class Bug : Prey,SnakeEdible,ScorpionEdible
    {
        public bool eaten { get; set; }
        
        public Bug()
        {
            ds = Description.BUG;
            img = Resource1.kissing_bug;
            turncount = 0;
            alive = true;
        }

        public override Direction Move( List<Direction> dirlist, Random rand)
        {
            turncount++;
            if (dirlist.Count > 0)
            {
                Direction temp = dirlist[rand.Next(0, dirlist.Count)];
                switch (temp)
                {
                    case Direction.DOWN: currentPos = new Point(currentPos.X + 1, currentPos.Y);
                        break;
                    case Direction.LEFT: currentPos = new Point(currentPos.X, currentPos.Y - 1);
                        break;
                    case Direction.RIGHT: currentPos = new Point(currentPos.X, currentPos.Y + 1);
                        break;
                    case Direction.UP: currentPos = new Point(currentPos.X - 1, currentPos.Y);
                        break;
                    default: break;
                }

                return temp;
            }
            else
                return Direction.NOMOVE;
        }

       

        public void beEatenByScorpion()
        {
            eaten = true;
            alive = false;
        }

        public void beEatenBySnake()
        {
            eaten = true;
            alive = false;
        }

       

        
    }
}
