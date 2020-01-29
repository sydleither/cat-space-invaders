using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace CatSpaceInvaders
{
    class API
    {
        public static PictureBox ThrowBall(PictureBox player, Control.ControlCollection control)
        {
            PictureBox ball = new PictureBox
            {
                Image = Properties.Resources.ball,
                Size = new Size(25, 25),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Left = player.Left + player.Width / 2
            };
            ball.Top = player.Top - ball.Width;
            control.Add(ball);
            ball.BringToFront();
            return ball;
        }

        public static void CreateCat(int position, int row, Bitmap catType, Control.ControlCollection control)
        {
            PictureBox cat = new PictureBox
            {
                Image = catType,
                Size = new Size(50, 50),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Left = position * 50 + 15,
                Top = row * 50,
                Tag = "cat"
            };
            control.Add(cat);
        }

        public static bool Collision(PictureBox ball, Control c, Control.ControlCollection control)
        {
            if (ball.Bounds.IntersectsWith(c.Bounds))
            {
                control.Remove(ball);
                control.Remove(c);
                return false;
            }

            return true;
        }

        public static bool Movement(Control c, Control.ControlCollection control, int catSpeed)
        {
            c.Top += catSpeed;

            if ((c.Top > 0 && c.Top < 200) || (c.Top > 400 && c.Top < 600))
                c.Left -= catSpeed;
            else
                c.Left += catSpeed;

            if (c.Top >= 800)
            {
                control.Remove(c);
                if (control.Count < 5)
                    return true;
            }

            return false;
        }
    }
}
