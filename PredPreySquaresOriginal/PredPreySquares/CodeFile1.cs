/*
if (clickflag)
            {
                if ((sender as PictureBox).Tag.ToString() == "")
                {
                    PictureBox senderBox = (sender as PictureBox);
                    senderBox.BackgroundImage = temp.BackgroundImage;
                    temp.BackgroundImage = null;
                    int sbx = senderBox.Location.X;
                    int sby = senderBox.Location.Y;
                    int tx = temp.Location.X;
                    int ty = temp.Location.Y;
                    int sbw = senderBox.Width;
                    int sbh = senderBox.Height;
                    sqarray[sby/sbw,sbx/sbh] = 
                        new Square(sqarray[sby/sbw,sbx/sbh].pb,sqarray[ty/sbw,tx/sbh].animal,new Point(sby/sbw,sbx/sbh));
                    sqarray[sby/sbw, sbx / sbh].animal.currentPos = new Point(sby/sbw, sbx / sbh);
                    sqarray[ty / sbw, tx / sbh].animal = null;
                    
                   
                    clickflag = false;
                }

                else
                {
                    MessageBox.Show("Choose another square");
                }
            }
            else
            {
                if ((sender as PictureBox).Tag.ToString() == Description.SCORPION.ToString() || (sender as PictureBox).Tag.ToString() == Description.SNAKE.ToString())
                {
                    temp = sender as PictureBox;
                    clickflag = true;
                }

                else
                {
                    MessageBox.Show("You must select a predator");
                }
            }

*/

/*
foreach(Point pt in moves)
                {
                    //if the squares are covered with prey OR another predator we can't move
                    List<Direction> dirlist = new List<Direction>();
                    int iprime,jprime;
                    dirlist = theBoard.getPrey(pt);
                    if(dirlist.Count == 4)
                        theBoard.predatorsMoved[(sqarray[senderPoint.X, senderPoint.Y].animal as Predator).identifier] = true;
                    else if(!dirlist.Contains(Direction.DOWN))

                    if (theBoard.getPrey(pt)
                    
                }

*/