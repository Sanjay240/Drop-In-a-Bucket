/**
 * Author : Sanjay kumar
 * Date: 11th December 2020
 * PURPOSE: this program draws a bucket with three lines and then draw some filled rectangular strips to 
 *          illustrate that the water is pouring in the bucket and has a timer to control the flow of liquid
 */

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab5a
{
    public partial class Form1 : Form
    {
        private Graphics g;                         //Encapsulates a GDI+ drawing surface
        private Pen p;                              //Pens are used to draw objects
        private Font f;                             //Defines a particular format for text, including font face, size, and style attributes
        private SolidBrush b;                       //Brushes are used to fill graphics shapes
        private Color c = Color.Silver;            //Represents a color, initially set to black

        /// <summary>
        /// This is the constructor which draws the screen and initilizes all the components.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            pictureBox1.ImageLocation = "Faucet.png";
            this.Paint += new PaintEventHandler(form1_Paint);  //Registers the Paint event handler 
           
        }
        /// <summary>
        /// this is the exit button event and close the program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// the color button will open the color dialog window and will set the color to the user choice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void colorbutton_Click(object sender, EventArgs e)
        {
            colorDialog.Color = Color.Aqua;                //Display with the previous colour already chosen
            colorDialog.ShowDialog();             //Display the actual dialog box
            c = colorDialog.Color;                //Save the colour choice the user made
        }
        /// <summary>
        /// this method initilizes all the graphic components and will draw the lines.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void form1_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;                          //Get the Graphics object from the PaintEventArgs
            p = new Pen(c);                          //Create a new Pen using the current colour
            f = new Font("Arial", 20);               //Create a new Font
            b = new SolidBrush(c);                   //Create a new brush using the current colour
            g.DrawLine(p, 120, 280, 120, 420);
            g.DrawLine(p, 120, 420, 300, 420);
            g.DrawLine(p, 300, 280, 300, 420);
        }
    
        /// <summary>
        /// this method will track the track bar position and will start the timer accordingly.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void flowTrackBar_Scroll(object sender, EventArgs e)
        {
            int speed = flowTrackBar.Value; // value of track bar
            if (speed != 0)
            {
                timer.Interval = 100;
                timer.Start();
              
            }
        }
        int h = 10;
        private void timer_Tick(object sender, EventArgs e)
        {
            int stripHeight = 205 - h; // heigth of rectangular steip grom tap to bucket level.
            if (h <= 130 && flowTrackBar.Value != 0)
            {
                g = this.CreateGraphics();
                g.FillRectangle(b, 145, 225, 18, stripHeight);
                g.FillRectangle(b, 120, (420 - h), 180, 10);
                h += 10;
                timer.Interval = 2000 - (100 * (flowTrackBar.Value));
            }
            else 
            {
                timer.Stop();
                g = this.CreateGraphics();
                SolidBrush z = new SolidBrush(Color.Black);
                g.FillRectangle(z, 145, 225, 18, stripHeight);
                if(h > 130)
                {
                    flowTrackBar.Value = flowTrackBar.Minimum;
                    stripHeight = 205;
                    h = 10;
                    g = this.CreateGraphics();
                    g.FillRectangle(z, 121, 280, 179, 140);
                    
                }
            }
        }
    }
}
