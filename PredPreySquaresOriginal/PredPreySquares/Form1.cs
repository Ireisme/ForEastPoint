using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//idea of wrapping up check prey, check null, check orthogonal all into one
namespace PredPreySquares
{
    public partial class Form1 : Form
    {

        Board theBoard;
        Random rand = new Random();
        //Point priorScorpLocation, priorSnakeLocation;
        
        public delegate bool bothPredsMoved();
        private bothPredsMoved bpMoved;
        
        public Form1()
        {
            InitializeComponent();
            theBoard = new Board(this,rand);
            //theBoard.findPredators(out priorScorpLocation, out priorSnakeLocation);
            //System.Diagnostics.Debug.WriteLine("Prior: " + priorScorpLocation.ToString());
            bpMoved += theBoard.Callback;
        }
        
        private void nextTurnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //still picking up both old snake and old scorpion position
            //Point scorpLocation, snakeLocation;
            //System.Diagnostics.Debug.Write(priorScorpLocation.ToString() + " ");
            //theBoard.findPredators(out scorpLocation, out snakeLocation);
            //System.Diagnostics.Debug.WriteLine(scorpLocation.ToString());
            if (bpMoved())
            {
                theBoard.moveBoard();
                //priorScorpLocation = scorpLocation;
                //priorSnakeLocation = snakeLocation;
                for (int i = 0; i < theBoard.predatorsMoved.Count; i++)
                    theBoard.predatorsMoved[i] = false;
            }
            else if (theBoard.predatorsMoved.Contains(false))
            {
                List<int> falseindex = new List<int>();
                for (int i = 0; i < theBoard.predatorsMoved.Count; i++)
                {
                    if (!theBoard.predatorsMoved[i])
                        falseindex.Add(i);
                }

                List<Point> unmoved = new List<Point>();
                foreach (int i in falseindex)
                    unmoved.Add(theBoard.IdentifierToPoint(i));

                List<Point> moves = theBoard.findPredators();

                List<List<Direction>> preylists = new List<List<Direction>>();
                for (int i = 0; i < unmoved.Count; i++)
                {
                    preylists.Add(theBoard.getPrey(unmoved[i]));
                }

                List<List<Direction>> dirnoprey = new List<List<Direction>>();
                for (int i = 0; i < preylists.Count; i++) //WILL HAVE TO ACCOUNT FOR THE IDEA OF BEING IN TOP/BOTTOM ROW, CORNER,ETC
                {
                    if (!preylists[i].Contains(Direction.DOWN))
                        dirnoprey[i].Add(Direction.DOWN);

                    if (!preylists[i].Contains(Direction.UP))
                        dirnoprey[i].Add(Direction.UP);

                    if (!preylists[i].Contains(Direction.LEFT))
                        dirnoprey[i].Add(Direction.LEFT);

                    if (!preylists[i].Contains(Direction.RIGHT))
                        dirnoprey[i].Add(Direction.RIGHT);

                }

                List<List<Point>> pointstocheck = new List<List<Point>>();
                int iprime, jprime;
                for (int i = 0; i < unmoved.Count; i++)
                {
                    if (dirnoprey[i].Count > 0)
                    {
                        List<Point> ptstochecktemp = new List<Point>();
                        for (int j = 0; j < dirnoprey[i].Count; j++)
                        {
                            Board.DirToCoords(unmoved[i].X, unmoved[i].Y, dirnoprey[i][j], out iprime, out jprime);
                            ptstochecktemp.Add(new Point(iprime, jprime));
                        }
                        pointstocheck.Add(ptstochecktemp);

                    }

                    else
                        pointstocheck.Add(null); //might need to check for this down the line, but to keep the indicies straight
                }
                bool [] whichisok = new bool[unmoved.Count];
                int k = 0;
                for (int i = 0; i < unmoved.Count; i++)
                {
                    if (pointstocheck[i] != null)
                    {
                        for (int j = 0; j < moves.Count; j++)
                        {
                            if (pointstocheck[i].Contains(moves[j]))
                                k++;
                        }

                        if (k < pointstocheck[i].Count)
                            whichisok[i] = false; //may need to account for null
                    }

                    else
                        whichisok[i] = true;
                 }

                //compare whichisok with falseindex

                int numofftrues = whichisok.Where(x => x == true).Select(x => x).Count();
                if (numofftrues < whichisok.Count())
                {
                    MessageBox.Show("Please move all predators before continuing");
                }

                else
                    theBoard.moveBoard();

            
            }
            else
            {
                MessageBox.Show("Please move all predators before continuing");
            }

           
        }

        
        

        
    }
}
