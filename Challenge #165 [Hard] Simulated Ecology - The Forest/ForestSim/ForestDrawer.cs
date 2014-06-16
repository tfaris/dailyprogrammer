//
//  A solution to reddit's /r/dailyprogrammer challenge #165 found here:
//  http://www.reddit.com/r/dailyprogrammer/comments/27h53e/662014_challenge_165_hard_simulated_ecology_the/
//
//  Author: Tom Faris <ta.faris@gmail.com>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ForestSim
{
    using Entity;

    /// <summary>
    /// Draws a Forest simulation to a graphics surface.
    /// </summary>
    public class ForestDrawer
    {
        Forest _forest;
        Font _txtFont = new Font("Terminal", 10.0f);
        float SQUARE_SCALE = 8;

        /// <summary>
        /// Get the forest being drawn.
        /// </summary>
        public Forest Forest
        {
            get { return _forest; }
        }

        public ForestDrawer(Forest forest)
        {
            _forest = forest;
        }

        /// <summary>
        /// Draw the forest to the specified graphics surface.
        /// </summary>
        /// <param name="g"></param>
        public void Draw(Graphics g)
        {
            float pX = 0, pY = 0;
            long monthsRun = _forest.GetMonths(),
                 yearsRun = monthsRun / Forest.YEAR_LENGTH;
            monthsRun = monthsRun % Forest.YEAR_LENGTH + 1;
            string timeMsg = string.Format("Year: {0}, Month: {1}", yearsRun, monthsRun);
            SizeF txtSize = g.MeasureString(timeMsg, _txtFont);
            g.DrawString(timeMsg,
                _txtFont,
                Brushes.Black,
                pX, pY);
            pY += txtSize.Height;

            for (int x = 0; x < _forest.Width; x++)
            {
                for (int y = 0; y < _forest.Height; y++)
                {
                    Color c;
                    ForestEntity entity = _forest.GetEntityAtPosition(x, y);
                    c = entity.Color;

                    RectangleF bounds = new RectangleF(
                        pX + x * SQUARE_SCALE, 
                        pY + y * SQUARE_SCALE,
                        1 * SQUARE_SCALE,
                        1 * SQUARE_SCALE);

                    using (SolidBrush brush = new SolidBrush(c))
                        g.FillRectangle(brush, bounds);
                    g.DrawRectangle(Pens.Black, bounds.X, bounds.Y, bounds.Width, bounds.Height);
                }
            }
        }
    }
}
