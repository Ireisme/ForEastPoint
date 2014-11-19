using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace PredPreySquares
{
    //TODO: Able to squash prey if they occupy a square
    //      Also, if the predator has no moves it should be allowed to pass into the next turn
    public class Board
    {
        Form mainform;
        
        Random rand;
        public const int rows = 12;
        public const int cols = 12;
        Square[,] sqarray = new Square[rows, cols];
        public const int initialBugs = 12;
        public const int initialAnts = 12;
        public const int initialWorms = 12;
        public const int turnstobreed = 24;//seems to produce too many...
        public const int turnspreybreed = 12;
        public const int turnspredstarve = 12;
        PictureBox temp;
        Point tempoint;
        int predatorcount = 0;
        bool clickflag = false;
        public List<bool> predatorsMoved = new List<bool>();
        public Board(Form mainform,Random rand)
        {
            this.mainform = mainform;
            this.rand = rand;
            mainform.ClientSize = new Size(912, 912);
            makeBoard();
        }
        
        
        private void makeBoard()
        {
            List<Organism> listorg = animalarrange();
            int height = mainform.ClientRectangle.Height;
            int width = mainform.ClientRectangle.Width;
            PictureBox temp;
            int k = 0;
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                {
                    temp = new PictureBox();
                    temp.Click += new EventHandler(pb_Click);

                    temp.Size = new Size(height / rows, width / cols);
                    temp.Location = new Point(j * width / cols, i* height / rows);
                    
                    temp.BorderStyle = BorderStyle.Fixed3D;
                    temp.BackgroundImageLayout = ImageLayout.Stretch;
                    
                    mainform.Controls.Add(temp);
                    //if (listorg[k] == null)
                      //  temp.Tag = ""; //prevents old tag from hanging around
                    //else
                      //  temp.Tag = listorg[k].ds.ToString();
                    temp.Tag = new Point(i, j);
                    sqarray[i, j] = new Square(temp, listorg[k],new Point(i,j));
                    if(sqarray[i, j].animal !=null)
                        sqarray[i,j].animal.currentPos = new Point(i, j);
                    k++;
                }
        }

        private List<Organism> animalarrange()
        {
            List<Organism> orglist = new List<Organism>();
            orglist.Add(new Snake(0));
            predatorcount++;
            predatorsMoved.Add(false);
            orglist.Add(new Scorpion(1));
            predatorcount++;
            predatorsMoved.Add(false);
            for (int i = 0; i < initialAnts; i++)
                orglist.Add(new Ant());
            for (int i = 0; i < initialBugs; i++)
                orglist.Add(new Bug());
            for (int i = 0; i < initialWorms; i++)
                orglist.Add(new Worm());
            while (orglist.Count <= 144)
                orglist.Add(null);


            List<Organism> randomList = new List<Organism>();
            int randomIndex;
            Random r = new Random();
            while (orglist.Count > 0)
            {
                randomIndex = r.Next(0, orglist.Count); //Choose a random object in the list
                randomList.Add(orglist[randomIndex]); //add it to the new, random list
                orglist.RemoveAt(randomIndex); //remove to avoid duplicates
            }
            return randomList;

        }

        public bool Callback() 
        {
            //need to make this into an array for multiple snakes and scorpions
            bool moved = true;
            foreach (bool bl in predatorsMoved)
                moved = moved && bl;
            return moved;
        }
        private void pb_Click(object sender, EventArgs e)
        {
            if (!clickflag)
            {
                temp = sender as PictureBox;
                
                tempoint = (Point) temp.Tag; //as doesn't work
                if (sqarray[tempoint.X, tempoint.Y].animal is Snake || sqarray[tempoint.X, tempoint.Y].animal is Scorpion)
                {
                    clickflag = true;
                   // System.Diagnostics.Debug.WriteLine("First leg of click: "+tempoint.ToString());
                }

                else
                {
                    MessageBox.Show("Please choose another square"); //adopt this later so no repeat clicks on moved animal
                }
            }

            else
            {
                
                Point senderPoint = (Point)((sender as PictureBox).Tag);
                Predator tempAnimal = sqarray[tempoint.X, tempoint.Y].animal as Predator;
                Predator senderAnimal = sqarray[senderPoint.X, senderPoint.Y].animal as Predator;
                
                if (senderAnimal == null && checkOrthogonal(tempoint,senderPoint))
                {
                    //System.Diagnostics.Debug.WriteLine("Premove(click): "+tempAnimal.currentPos.ToString());
                    tempAnimal.Move(senderPoint);
                    //System.Diagnostics.Debug.WriteLine("Postmove (click): " +tempAnimal.currentPos.ToString());
                    sqarray[senderPoint.X, senderPoint.Y] = new Square(sender as PictureBox, sqarray[tempoint.X, tempoint.Y].animal, new Point(senderPoint.X, senderPoint.Y));
                    sqarray[tempoint.X, tempoint.Y] = new Square(temp as PictureBox, null, new Point(tempoint.X, tempoint.Y));
                    sqarray[senderPoint.X, senderPoint.Y].pb.BackgroundImage = temp.BackgroundImage;
                    sqarray[tempoint.X, tempoint.Y].pb.BackgroundImage = null;

                    if (sqarray[senderPoint.X, senderPoint.Y].animal is Predator)
                        predatorsMoved[(sqarray[senderPoint.X, senderPoint.Y].animal as Predator).identifier] = true;
                    
                    clickflag = false; //sanity for later
                }
                
                else
                {
                    MessageBox.Show("Choose another square");
                }

                int i = senderPoint.X, j = senderPoint.Y;
                int iprime, jprime;
                if (sqarray[senderPoint.X,senderPoint.Y].animal !=null && (sqarray[senderPoint.X, senderPoint.Y].animal as Predator).turncount == turnstobreed)
                {
                    List<Direction> dirs = getMoves(senderPoint);
                    Direction dir = dirs[rand.Next(0,dirs.Count)];
                    if ((dir != Direction.NOMOVE))
                    {
                        DirToCoords(i, j, dir, out iprime, out jprime);


                        sqarray[iprime, jprime] = new Square(sqarray[iprime, jprime].pb, sqarray[i, j].animal, new Point(iprime, jprime));
                        sqarray[iprime, jprime].animal.currentPos = new Point(iprime, jprime);
                        sqarray[iprime, jprime].pb.BackgroundImage = sqarray[i, j].animal.img;
                        MessageBox.Show("Predator at " + iprime.ToString() + " " + jprime.ToString() + " has bred");
                        predatorcount++;
                        predatorsMoved.Add(false);


                    }

                    

                }

                else if (sqarray[i, j].animal.turncount == turnspredstarve && !(sqarray[i, j].animal as Predator).hasEaten)
                {
                    sqarray[i, j].animal.alive = false; //keep this there for later
                    sqarray[i, j] = new Square(sqarray[i, j].pb, null, new Point(i, j));
                    sqarray[i, j].pb.BackgroundImage = null;

                    MessageBox.Show("Predator at " + i.ToString() + " " + j.ToString() + " has starved");
                }
            }

        }

        private bool checkOrthogonal(Point origin, Point destination)
        {
            bool ortho = false;
            if (origin.X == destination.X && (origin.Y == destination.Y + 1 || origin.Y == destination.Y - 1))
                ortho = true;
            else if (origin.Y == destination.Y && (origin.X == destination.X + 1 || origin.X == destination.X - 1))
                ortho = true;

            return ortho;

        
         }

        public void moveBoard()
        {
            List<Point> marked = new List<Point>();


            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (sqarray[i, j].animal != null)
                        System.Diagnostics.Debug.WriteLine(i.ToString() + " " + j.ToString() + sqarray[i, j].animal.ds.ToString());
                    if (!marked.Contains(new Point(i, j)) && sqarray[i, j].animal != null && !(sqarray[i, j].animal is Snake) && !(sqarray[i, j].animal is Scorpion))
                    {
                        Direction dir = (sqarray[i, j].animal as Prey).Move(getMoves(sqarray[i, j].Location), rand);
                        //System.Diagnostics.Debug.WriteLine(i.ToString() + " " + j.ToString() + "  " + dir.ToString());
                        int iprime, jprime;
                        //check to make sure there are no scorpions or snakes also before moving
                        DirToCoords(i, j, dir, out iprime, out jprime);

                        if (dir != Direction.NOMOVE)
                        {
                            sqarray[iprime, jprime] = new Square(sqarray[iprime, jprime].pb, sqarray[i, j].animal, new Point(iprime, jprime));
                            sqarray[i, j] = new Square(sqarray[i, j].pb, null, new Point(i, j));

                            sqarray[iprime, jprime].pb.BackgroundImage = sqarray[i, j].pb.BackgroundImage;
                            sqarray[i, j].pb.BackgroundImage = null;
                            marked.Add(new Point(iprime, jprime));
                        }
                    }
                }
            }

            /*System.Diagnostics.Debug.WriteLine("");
            System.Diagnostics.Debug.WriteLine("");
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    if(sqarray[i,j].animal !=null)
                        System.Diagnostics.Debug.WriteLine(i.ToString() + " " + j.ToString() + sqarray[i, j].animal.ds.ToString());
                }
            }*/
            eat();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (sqarray[i, j].animal is Prey && (sqarray[i, j].animal as Edible).eaten)
                    {
                        sqarray[i, j] = new Square(sqarray[i, j].pb, null, new Point(i, j));
                        sqarray[i, j].pb.BackgroundImage = null;
                    }
                }
            }
        }
        
        public static void DirToCoords(int i, int j, Direction dir, out int iprime, out int jprime)
        {
            switch (dir)
            {
                case Direction.DOWN: iprime = i + 1; jprime = j;
                    break;
                case Direction.LEFT: iprime = i; jprime = j - 1;
                    break;
                case Direction.RIGHT: iprime = i; jprime = j + 1;
                    break;
                case Direction.UP: iprime = i - 1; jprime = j;
                    break;
                default: iprime = i; jprime = j;
                    break;

            }
        }

        private List<Direction> getMoves(Point currentlocation)
        {
            List<Direction> dirlist = new List<Direction>();
            
            if (currentlocation.X > 0 && currentlocation.X < rows-1 && currentlocation.Y > 0 && currentlocation.Y <cols-1)
            {
                if (sqarray[currentlocation.X, currentlocation.Y-1].animal == null)
                    dirlist.Add(Direction.LEFT);
                if (sqarray[currentlocation.X-1, currentlocation.Y].animal == null)
                    dirlist.Add(Direction.UP);
                if (sqarray[currentlocation.X, currentlocation.Y+1].animal == null)
                    dirlist.Add(Direction.RIGHT);
                if (sqarray[currentlocation.X+1, currentlocation.Y].animal == null)
                    dirlist.Add(Direction.DOWN);
            }

            else if (currentlocation.X == 0 && currentlocation.Y !=0 && currentlocation.Y !=cols-1)//top row
            {
                if (sqarray[currentlocation.X, currentlocation.Y - 1].animal == null)
                    dirlist.Add(Direction.LEFT);                
                if (sqarray[currentlocation.X, currentlocation.Y + 1].animal == null)
                    dirlist.Add(Direction.RIGHT);
                if (sqarray[currentlocation.X + 1, currentlocation.Y].animal == null)
                    dirlist.Add(Direction.DOWN);
            }

            else if (currentlocation.X == rows-1 && currentlocation.Y != 0 && currentlocation.Y !=cols-1) //bottom row
            {
                if (sqarray[currentlocation.X, currentlocation.Y - 1].animal == null)
                    dirlist.Add(Direction.LEFT);
                if (sqarray[currentlocation.X - 1, currentlocation.Y].animal == null)
                    dirlist.Add(Direction.UP);
                if (sqarray[currentlocation.X, currentlocation.Y + 1].animal == null)
                    dirlist.Add(Direction.RIGHT);               
            }

            else if (currentlocation.Y == 0 && currentlocation.X != 0 && currentlocation.X !=rows-1) //left column
            {
                
                if (sqarray[currentlocation.X - 1, currentlocation.Y].animal == null)
                    dirlist.Add(Direction.UP);
                if (sqarray[currentlocation.X, currentlocation.Y + 1].animal == null)
                    dirlist.Add(Direction.RIGHT);
                if (sqarray[currentlocation.X + 1, currentlocation.Y].animal == null)
                    dirlist.Add(Direction.DOWN);                
            }

            else if (currentlocation.Y == cols-1 && currentlocation.X != 0 && currentlocation.X !=rows-1) //right column
            {
                if (sqarray[currentlocation.X, currentlocation.Y - 1].animal == null)
                    dirlist.Add(Direction.LEFT);
                if (sqarray[currentlocation.X - 1, currentlocation.Y].animal == null)
                    dirlist.Add(Direction.UP);                
                if (sqarray[currentlocation.X + 1, currentlocation.Y].animal == null)
                    dirlist.Add(Direction.DOWN);
            }

            else if (currentlocation.X == 0 && currentlocation.Y == 0) //upper left hand corner
            {                
                if (sqarray[currentlocation.X, currentlocation.Y + 1].animal == null)
                    dirlist.Add(Direction.RIGHT);
                if (sqarray[currentlocation.X + 1, currentlocation.Y].animal == null)
                    dirlist.Add(Direction.DOWN);               
            }

            else if (currentlocation.X == 0 && currentlocation.Y == cols-1) //upper right hand corner
            {
                if (sqarray[currentlocation.X, currentlocation.Y - 1].animal == null)
                    dirlist.Add(Direction.LEFT);
               
                if (sqarray[currentlocation.X + 1, currentlocation.Y].animal == null)
                    dirlist.Add(Direction.DOWN);
            }

            else if (currentlocation.X == rows-1 && currentlocation.Y == 0) //lower left hand corner
            {                
                if (sqarray[currentlocation.X - 1, currentlocation.Y].animal == null)
                    dirlist.Add(Direction.UP);
                if (sqarray[currentlocation.X, currentlocation.Y + 1].animal == null)
                    dirlist.Add(Direction.RIGHT);
            }

            else // lower right hand corner
            {
                if (sqarray[currentlocation.X, currentlocation.Y - 1].animal == null)
                    dirlist.Add(Direction.LEFT);
                if (sqarray[currentlocation.X - 1, currentlocation.Y].animal == null)
                    dirlist.Add(Direction.UP);
            }


            return dirlist;
        }

        public List<Direction> getPrey(Point currentlocation)
        {
            List<Direction> dirlist = new List<Direction>();
            Organism og;
            if (currentlocation.X > 0 && currentlocation.X < rows - 1 && currentlocation.Y > 0 && currentlocation.Y < cols - 1)
            {
                if ((og = sqarray[currentlocation.X, currentlocation.Y - 1].animal) != null && og is Prey)
                    dirlist.Add(Direction.LEFT);
                if ((og=sqarray[currentlocation.X - 1, currentlocation.Y].animal) !=null && og is Prey)
                    dirlist.Add(Direction.UP);
                if ((og=sqarray[currentlocation.X, currentlocation.Y + 1].animal) !=null && og is Prey)
                    dirlist.Add(Direction.RIGHT);
                if ((og=sqarray[currentlocation.X + 1, currentlocation.Y].animal) !=null && og is Prey)
                    dirlist.Add(Direction.DOWN);
            }

            else if (currentlocation.X == 0 && currentlocation.Y != 0 && currentlocation.Y != cols - 1)//top row
            {
                if ((og=sqarray[currentlocation.X, currentlocation.Y - 1].animal) !=null && og is Prey)
                    dirlist.Add(Direction.LEFT);
                if ((og=sqarray[currentlocation.X, currentlocation.Y + 1].animal) !=null && og is Prey)
                    dirlist.Add(Direction.RIGHT);
                if ((og=sqarray[currentlocation.X + 1, currentlocation.Y].animal) !=null && og is Prey)
                    dirlist.Add(Direction.DOWN);
            }

            else if (currentlocation.X == rows - 1 && currentlocation.Y != 0 && currentlocation.Y != cols - 1) //bottom row
            {
                if ((og=sqarray[currentlocation.X, currentlocation.Y - 1].animal) !=null && og is Prey)
                    dirlist.Add(Direction.LEFT);
                if ((og=sqarray[currentlocation.X - 1, currentlocation.Y].animal) !=null && og is Prey)
                    dirlist.Add(Direction.UP);
                if ((og=sqarray[currentlocation.X, currentlocation.Y + 1].animal) !=null && og is Prey)
                    dirlist.Add(Direction.RIGHT);
            }

            else if (currentlocation.Y == 0 && currentlocation.X != 0 && currentlocation.X != rows - 1) //left column
            {

                if ((og=sqarray[currentlocation.X - 1, currentlocation.Y].animal) !=null && og is Prey)
                    dirlist.Add(Direction.UP);
                if ((og=sqarray[currentlocation.X, currentlocation.Y + 1].animal) !=null && og is Prey)
                    dirlist.Add(Direction.RIGHT);
                if ((og=sqarray[currentlocation.X + 1, currentlocation.Y].animal) !=null && og is Prey)
                    dirlist.Add(Direction.DOWN);
            }

            else if (currentlocation.Y == cols - 1 && currentlocation.X != 0 && currentlocation.X != rows - 1) //right column
            {
                if ((og=sqarray[currentlocation.X, currentlocation.Y - 1].animal) !=null && og is Prey)
                    dirlist.Add(Direction.LEFT);
                if ((og=sqarray[currentlocation.X - 1, currentlocation.Y].animal) !=null && og is Prey)
                    dirlist.Add(Direction.UP);
                if ((og=sqarray[currentlocation.X + 1, currentlocation.Y].animal) !=null && og is Prey)
                    dirlist.Add(Direction.DOWN);
            }

            else if (currentlocation.X == 0 && currentlocation.Y == 0) //upper left hand corner
            {
                if ((og=sqarray[currentlocation.X, currentlocation.Y + 1].animal) !=null && og is Prey)
                    dirlist.Add(Direction.RIGHT);
                if ((og=sqarray[currentlocation.X + 1, currentlocation.Y].animal) !=null && og is Prey)
                    dirlist.Add(Direction.DOWN);
            }

            else if (currentlocation.X == 0 && currentlocation.Y == cols - 1) //upper right hand corner
            {
                if ((og=sqarray[currentlocation.X, currentlocation.Y - 1].animal) !=null && og is Prey)
                    dirlist.Add(Direction.LEFT);

                if ((og=sqarray[currentlocation.X + 1, currentlocation.Y].animal) !=null && og is Prey)
                    dirlist.Add(Direction.DOWN);
            }

            else if (currentlocation.X == rows - 1 && currentlocation.Y == 0) //lower left hand corner
            {
                if ((og=sqarray[currentlocation.X - 1, currentlocation.Y].animal) !=null && og is Prey)
                    dirlist.Add(Direction.UP);
                if ((og=sqarray[currentlocation.X, currentlocation.Y + 1].animal) !=null && og is Prey)
                    dirlist.Add(Direction.RIGHT);
            }

            else // lower right hand corner
            {
                if ((og=sqarray[currentlocation.X, currentlocation.Y - 1].animal) !=null && og is Prey)
                    dirlist.Add(Direction.LEFT);
                if ((og=sqarray[currentlocation.X - 1, currentlocation.Y].animal) !=null && og is Prey)
                    dirlist.Add(Direction.UP);
            }


            return dirlist;
        }

        public List<Point> findPredators()
        {
            List<Point> preds = new List<Point>();

            foreach (Square sq in sqarray)
            {
                if (sq.animal is Scorpion || sq.animal is Snake)
                    preds.Add(sq.animal.currentPos);
                
            }

            return preds;            
              
        }

        private void eat()
        {
            List<Point> preds = findPredators();
            foreach (Point pd in preds)
            {
                List<Direction> preydirs = getPrey(pd);
                if (preydirs.Count > 0)
                {
                    List<int> indexes = new List<int>();
                    bool ate = false;
                    do
                    {
                        if (indexes.Count >= preydirs.Count)
                            break;
                        int index = rand.Next(0, preydirs.Count);
                        if (!indexes.Contains(index))
                            indexes.Add(index);
                        int iprime, jprime;
                        Board.DirToCoords(pd.X, pd.Y, preydirs[index], out iprime, out jprime);
                        if (sqarray[iprime, jprime].animal.alive && !(sqarray[iprime, jprime].animal as Edible).eaten && sqarray[iprime, jprime].animal is Edible)
                            ate = (sqarray[pd.X, pd.Y].animal as Predator).Eat(sqarray[iprime, jprime].animal as Edible);
                        //System.Diagnostics.Debug.WriteLine("I am stuck in the do/while loop");
                    } while (!ate);
                }

                else
                    continue;
            }
        }

        public Point IdentifierToPoint(int identifier)
        {
            List<Predator> predlist = new List<Predator>();
            foreach (Square sq in sqarray)
            {
                if (sq.animal is Predator)
                    predlist.Add(sq.animal as Predator);
            }
            var position = predlist.Where(x => x.identifier == identifier).Select(x=>x.currentPos);
            if (position.Count() > 1 || position.Count() == 0)
                MessageBox.Show("Error, returning default of 20,20");
            else
                foreach (Point pt in position)
                    return pt;  //yield return here causes errors, I don't think it's right

            return new Point(20, 20);
                

        }

        public int PointToIdentifier(Point location)
        {
            List<Predator> predlist = new List<Predator>();
            foreach (Square sq in sqarray)
            {
                if (sq.animal is Predator)
                    predlist.Add(sq.animal as Predator);
            }

            var id = predlist.Where(x => x.currentPos == location).Select(x => x.identifier);

            foreach (int i in id)
                return i;

            return -999;
        }
    }
}
