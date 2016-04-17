using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace raypad
{
    public partial class RayPad : Form
    {
        public RayPad()
        {
            InitializeComponent();

        }


        private void RayPad_Load(object sender, EventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void ShowLineJoin(PaintEventArgs e)
        {

            // Create a new pen.
            Pen skyBluePen = new Pen(Brushes.DeepSkyBlue);

            // Set the pen's width.
            skyBluePen.Width = 8.0F;

            // Set the LineJoin property.
            skyBluePen.LineJoin = System.Drawing.Drawing2D.LineJoin.Bevel;

            // Draw a rectangle.
            e.Graphics.DrawRectangle(skyBluePen,
                new Rectangle(40, 40, 150, 200));

            //Dispose of the pen.
            skyBluePen.Dispose();

        }
    }
}
