using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace raypad
{
    public partial class RayPad : Form
    {
        Bitmap plotbit;
        Graphics draw_graph;
         string SensorData;
         string current_time;
        int zoom = 10, pd = 20,ya = 5;
        string[] cp;
        

        public RayPad()
        {
            InitializeComponent();
            draw_graph = GraphPanel.CreateGraphics();

        }

        Pen pen_X = new Pen(Color.Green, 2);
        Pen pen_Y = new Pen(Color.Blue, 2);
        Pen eraser = new Pen(Color.White, 5);
        Pen pen_grid = new Pen(Color.LightGray);
        Pen pen_curve = new Pen(Color.Red);
                

        private void RayPad_Load(object sender, EventArgs e)
        {
         
        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        public void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                draw_graph.DrawLine(pen_X, 30, 450, 650, 450);
                draw_graph.DrawLine(pen_Y, 30, 450, 30, 10);
                for (int i = 0; i < 700;i=i+zoom )
                {
                    draw_graph.DrawLine(pen_grid, 0, i, 1000, i);
                    draw_graph.DrawLine(pen_grid, i, 0, i, 1000);
                }
            }
            else
            {
                draw_graph.DrawLine(eraser, 30, 450, 650, 450);
                draw_graph.DrawLine(eraser, 30, 450, 30, 10);
                for (int i = 0; i < 700;i=i+zoom )
                {
                    draw_graph.DrawLine(eraser, 0, i, 1000, i);
                    draw_graph.DrawLine(eraser, i, 0, i, 1000);
                }
            }
        }

        public void convert(string sen_data)
        {
            cp = SensorData.Split(',');
            
        }
        


        private void Scan_Click(object sender, EventArgs e)
        {
            if (connection.Text == "Connected !")
            {
                SensorData = "0";
                SensorData = serialPortArduino.ReadLine();
                label10.Text = SensorData;
                convert(SensorData);
                Point point1 = new Point(270, 450);
                Point point2 = new Point(270 + pd, 450 - ya*(Convert.ToInt32(cp[0])));
                Point point3 = new Point(270 + 2 * pd, 450 - ya*(Convert.ToInt32(cp[1])));
                Point point4 = new Point(270 + 3 * pd, 450 - ya*(Convert.ToInt32(cp[2])));
                Point point5 = new Point(270 + 4 * pd, 450);
                /* 
                *
                Point point6 = new Point(270 + 5 * pd, 450 - (Convert.ToInt32(cp[6])));
                Point point7 = new Point(270 + 6 * pd, 450 - (Convert.ToInt32(cp[7])));
                * 
                */
                Point[] curvePoints = { point1, point2, point3,point4,point5  };
                draw_graph.DrawCurve(pen_curve, curvePoints);
                DateTime Scan_time = new DateTime();
                current_time = Convert.ToString(Scan_time.Date);
                status.Text = current_time;
            }
            else
            {
                status.Text = "Please connect to Sensor";
            }
        }

        private void ZoomIn_Click(object sender, EventArgs e)
        {
            
            for (int i = 0; i < 700;i=i+zoom )
                {
                    draw_graph.DrawLine(eraser, 0, i, 1000, i);
                    draw_graph.DrawLine(eraser, i, 0, i, 1000);
                }
            zoom=zoom+5;
             for (int i = 0; i < 700;i=i+zoom )
                {
                    draw_graph.DrawLine(pen_grid, 0, i, 1000, i);
                    draw_graph.DrawLine(pen_grid, i, 0, i, 1000);
                }
             draw_graph.DrawLine(pen_X, 30, 450, 650, 450);
             draw_graph.DrawLine(pen_Y, 30, 450, 30, 10);
        }

        private void ZoomOut_Click(object sender, EventArgs e)
        {
            
            if (zoom > 5)
            {
                 for (int i = 0; i < 700; i = i + zoom)
                {
                    draw_graph.DrawLine(eraser, 0, i, 1000, i);
                    draw_graph.DrawLine(eraser, i, 0, i, 1000);
                }
                zoom = zoom - 5;
                for (int i = 0; i < 700; i = i + zoom)
                {
                    draw_graph.DrawLine(pen_grid, 0, i, 1000, i);
                    draw_graph.DrawLine(pen_grid, i, 0, i, 1000);
                }
            }
            draw_graph.DrawLine(pen_X, 30, 450, 650, 450);
            draw_graph.DrawLine(pen_Y, 30, 450, 30, 10);

        }

        private void ComConnect_Click(object sender, EventArgs e)
        {
            
            
            
            
            
            if (connection.Text == "Disconnected")
            {
                if (portTextBox.Text != "")
                {
                    serialPortArduino.PortName = portTextBox.Text;
                }
                serialPortArduino.Open();
                connection.ForeColor = Color.Green;
                connection.Text = "Connected !";
                ComConnect.Text = "Disconnect";
            }

            else if (connection.Text == "Connected !")
            {
                serialPortArduino.Close();
                connection.ForeColor = Color.Red;
                connection.Text = "Disconnected";
                ComConnect.Text = "Connect";
            }
            else
            {
                status.Text = "Error with COM PORT";
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            pd = Convert.ToInt32(numericUpDown1.Value);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            draw_graph.Clear(Color.White);

            draw_graph.DrawLine(pen_X, 30, 450, 650, 450);
            draw_graph.DrawLine(pen_Y, 30, 450, 30, 10);
            for (int i = 0; i < 700; i = i + zoom)
            {
                draw_graph.DrawLine(pen_grid, 0, i, 1000, i);
                draw_graph.DrawLine(pen_grid, i, 0, i, 1000);
            }
            serialPortArduino.Close();
            serialPortArduino.Open();
            cp[0] = "";
            cp[1] = "";
            cp[2] = "";
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            portTextBox.Text = comboBox1.Text;
        }

        public void button2_Click(object sender, EventArgs e)
        {
            string ConnectionString = "server=localhost;database=astroeye;uid=root;pwd=nargish10;";
            MySqlConnection cnn;
            cnn = new MySqlConnection(ConnectionString);
            try
            {
                cnn.Open();

                
                MySqlCommand command = new MySqlCommand("INSERT INTO sensor (time,data) VALUES (@0,@1);",cnn);
                command.Parameters.Add(new MySqlParameter("0",DateTime.Now));
                command.Parameters.Add(new MySqlParameter("1",SensorData));
                command.ExecuteNonQuery();
                MessageBox.Show("Connection Open ! \n Data Sent!! ");

                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! ");
            }



        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        private void GraphPanel_Paint(object sender, PaintEventArgs e)
        {

        }

      
       

       

      

        

     


    }
}
