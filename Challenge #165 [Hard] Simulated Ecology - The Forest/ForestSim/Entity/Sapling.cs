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
    /// Represents a growing tree sapling.
    /// </summary>
    class Sapling : Tree
    {
        public override System.Drawing.Color Color
        {
            get
            {
                return System.Drawing.Color.LightGreen;
            }
        }

        public override int Chop()
        {
            // Chopping down a sapling gets you nothing.
            return 0;
        }

        protected override void DoTick(Forest forest, long currentTick)
        {
            // After x amount of months, this sapling grows into a tree.
            if (Age >= Forest.YEAR_LENGTH)
            {
                Tree grown = new Tree();
                grown.Location = Location;
                forest.AddEntity(grown);
            }
        }
    }
}
