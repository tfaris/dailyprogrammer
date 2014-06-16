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

namespace ForestSim
{
    /// <summary>
    /// A double buffered panel for more efficient drawing.
    /// </summary>
    class DoubleBufferedPanel : System.Windows.Forms.Panel
    {
        public DoubleBufferedPanel() : base()
        {
            DoubleBuffered = true;
        }
    }
}
