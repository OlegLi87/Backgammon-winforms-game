using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Drawing;
using System.Net;
using System.Diagnostics;

namespace BackgammonUI
{
    class Triangle : Panel
    {
        private static int checkerImageHolderHeight;
        internal int Index { get; private set; }

        internal Triangle(int index, Size size)
        {
            Index = index;
            BackColor = Color.Transparent;
            Size = size;
            DoubleBuffered = true; // Eleminates screen stuttering at re-rendering.
        }

        internal void Add(PictureBox checkerImageHolder)
        {
            Controls.Add(checkerImageHolder);
            arrangeCheckerImages();
        }

        internal bool TrySetImageHolderBackLight(Color color)
        {
            if (Controls.Count > 0)
            {
                Color tagColor = (Color)((Controls[0] as PictureBox).Image.Tag);
                if (color == Color.Transparent || tagColor == color)
                {
                    Control imageHolder = Controls[Controls.Count - 1];
                    imageHolder.BackColor = color;
                    return true;
                }
            }
            return false;
        }

        private void arrangeCheckerImages()
        {
            if (checkerImageHolderHeight == 0)
                checkerImageHolderHeight = Controls[0].Size.Height;

            int actualCheckerImageHeight = checkerImageHolderHeight;

            if (Controls.Count > 5)
                actualCheckerImageHeight = Size.Height / Controls.Count;

            int startingPointY = 0;
            if (Index > 11)
                startingPointY = Size.Height - actualCheckerImageHeight;
            if (Index < 0)
            {
                startingPointY = Size.Height / 2 - 31;
                actualCheckerImageHeight = Controls.Count > 2 ? Size.Height / 2 / Controls.Count : actualCheckerImageHeight;
            }


            for (int i = 0; i < Controls.Count; i++)
            {
                Controls[i].Size = new Size(Size.Width, actualCheckerImageHeight);
                Controls[i].Location = new Point(Controls[i].Location.X, Math.Abs(startingPointY - actualCheckerImageHeight * i));
            }
        }
    }
}
