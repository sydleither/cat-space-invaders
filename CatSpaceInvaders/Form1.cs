using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CatSpaceInvaders
{
    public partial class Form1 : Form
    {
        bool left;
        bool right;
        int speed = 10;

        PictureBox ball;
        bool ballThrown = false;
        int ballSpeed = 30;

        int catSpeed = 3;

        public Form1()
        {
            InitializeComponent();
            player.Location = new Point(450,650);

            NewWave();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (left)
            {
                player.Left -= speed;
            }

            if (right)
            {
                player.Left += speed;
            }

            if (ballThrown)
            {
                ball.Top -= ballSpeed;

                if (ball.Top < 0)
                {
                    this.Controls.Remove(ball);
                    ballThrown = false;
                }
            }

            foreach (Control c in this.Controls)
            {
                if (c is PictureBox && c.Tag == "cat")
                {
                    if (API.Movement(c, this.Controls, catSpeed))
                        NewWave();

                    if (this.Controls.Contains(ball))
                    {
                        ballThrown = API.Collision(ball, c, this.Controls);
                    }

                    if (player.Bounds.IntersectsWith(c.Bounds))
                    {
                        timer.Stop();
                    }
                }
            }
        }

        private void keydown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                left = true;
            }

            if (e.KeyCode == Keys.Right)
            {
                right = true;
            }

            if(e.KeyCode == Keys.Space && ballThrown == false)
            {
                ball = API.ThrowBall(player, this.Controls);
                ballThrown = true;
            }
        }

        private void keyup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                left = false;
            }

            if (e.KeyCode == Keys.Right)
            {
                right = false;
            }
        }

        public void NewWave()
        {
            Bitmap[] catArray = { Properties.Resources.angrycat, Properties.Resources.loopscat, Properties.Resources.cyclonecat };
            Random random = new Random();
            for (int j = 0; j < 5; j++)
            {
                for (int i = 0; i < 19; i++)
                {
                    API.CreateCat(i, j, catArray[random.Next(0, 3)], this.Controls);
                }
            }
        }
    }
}
