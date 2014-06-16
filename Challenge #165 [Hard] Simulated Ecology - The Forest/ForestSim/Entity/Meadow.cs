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
    /// Represents an empty meadow.
    /// </summary>
    class Meadow : ForestEntity
    {
        public override bool IsEmpty
        {
            get
            {
                return true;
            }
        }

        protected override void DoTick(Forest forest, long currentTick)
        {
            // Empty space does nothing...
        }
    }
}
