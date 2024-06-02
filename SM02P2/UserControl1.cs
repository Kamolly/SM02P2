using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SM02P2
{
    public partial class UserControl1: UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
            this.Paint += new PaintEventHandler(Form1_Paint);
            this.MouseClick += mouseClick;

            points = new List<PointF>();
            minimumTriangle = new List<PointF>();

            dynamicTextBox = new TextBox();
            dynamicTextBox.Location = new Point(10, 50);
            dynamicTextBox.Height = 80;
            dynamicTextBox.Width = 150;
            Controls.Add(dynamicTextBox);
        }

        private List<PointF> points;
        private List<PointF> minimumTriangle;
        private TextBox dynamicTextBox;


        private void mouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Console.WriteLine($"Mouse clicked");
                points.Add(new PointF(e.X, e.Y));

                this.Invalidate();
            }
            else if (e.Button == MouseButtons.Right)
            {
                Console.WriteLine("Mouse Right");
                minimumTriangle = FindMinAreaTriangle(points);
                this.Invalidate();
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush brush = new SolidBrush(Color.Black);
            Brush triangle = new SolidBrush(Color.Green);

            foreach (PointF p in points)
            {
                g.FillEllipse(brush, p.X - 3, p.Y - 3, 6, 6);
            }

            if (minimumTriangle.Count >= 3)
            {
                dynamicTextBox.Text = TriangleArea(minimumTriangle[0], minimumTriangle[1], minimumTriangle[2]).ToString();
                g.FillPolygon(triangle, new PointF[] { minimumTriangle[0], minimumTriangle[1], minimumTriangle[2] });
            }
        }
    /////////////////////
        private double TriangleArea(PointF p1, PointF p2, PointF p3)
        {
            return Math.Abs(p1.X * (p2.Y - p3.Y) + p2.X * (p3.Y - p1.Y) + p3.X * (p1.Y - p2.Y)) / 2.0;
        }

        private List<PointF> FindMinAreaTriangle(List<PointF> points)
        {
            double minArea = double.MaxValue;
            List<PointF> minTriangle = new List<PointF>();

            PointF bestp1 = new PointF();
            PointF bestp2 = new PointF();
            PointF bestp3 = new PointF();

            int n = points.Count;
            for (int i = 0; i < n - 2; i++)
            {
                for (int j = i + 1; j < n - 1; j++)
                {
                    for (int k = j + 1; k < n; k++)
                    {
                        double area = TriangleArea(points[i], points[j], points[k]);
                        if (area < minArea)
                        {
                            minArea = area;
                            bestp1 = points[i];
                            bestp2 = points[j];
                            bestp3 = points[k];
                        }
                    }
                }
            }

            minTriangle.Add(bestp1);
            minTriangle.Add(bestp2);
            minTriangle.Add(bestp3);

            return minTriangle;
        }
        /////////////////////////////
        private void UserControl1_Load(object sender, EventArgs e)
        {

        }
    }
}
