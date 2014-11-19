using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace PredPreySquares
{
    
    public class Square
    {
        public PictureBox pb { get; set; }
        public Point Location { get; private set; }
        public Organism animal { get; set; }
        public Square(PictureBox pb,Organism animal,Point location)
        {
            this.pb = pb;
            this.animal = animal;
            Location = location;
            if(animal !=null)
                setPicture();
        }

        private void setPicture()
        {
            
            pb.BackgroundImage = animal.img;
        }
    
    }
}
