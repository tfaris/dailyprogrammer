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

namespace ForestSim.Rule
{
    /// <summary>
    /// Represents a simulation rule.
    /// </summary>
    public abstract class SimulationRule
    {
        /// <summary>
        /// Process the rule for a simulation on the currently specified tick.
        /// </summary>
        /// <param name="currentTick"></param>
        public abstract void ProcessRule(Forest forest, long currentTick);
    }
}
