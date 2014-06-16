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

namespace ForestSim.Entity
{
    /// <summary>
    /// Represents a big old simulated tree.
    /// </summary>
    class ElderTree : Tree
    {
        public override System.Drawing.Color Color
        {
            get
            {
                return System.Drawing.Color.DarkGreen;
            }
        }

        public override float SaplingProbability
        {
            get
            {
                // Elder trees have 20% chance of spreading a sapling.
                return .2f;
            }
        }

        public override int Chop()
        {
            // Elder trees give 2 logs.
            return 2;
        }
    }
}
