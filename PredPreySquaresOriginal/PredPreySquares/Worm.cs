using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PredPreySquares
{
    public class Worm : Prey,SnakeEdible
    {

        public bool eaten { get; set; }
        public Worm()
        {
            ds = Description.WORM;
            img = Resource1.earthworm1;
            turncount = 0;
            alive = true;
            
        }

        public override Direction Move(List<Direction> dirlist, Random rand)
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

        

        public void beEatenBySnake()
        {
            eaten = true;
            alive = false;
        }
    }
}
