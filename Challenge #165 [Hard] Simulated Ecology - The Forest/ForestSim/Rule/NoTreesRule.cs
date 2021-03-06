﻿//
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
    using Entity;

    /// <summary>
    /// A rule that will end the simulation when all trees
    /// have been removed from the forest.
    /// </summary>
    class NoTreesRule : SimulationRule
    {
        public override void ProcessRule(Forest forest, long currentTick)
        {
            if ((from e in forest.Entities
                 where typeof(Tree).IsAssignableFrom(e.GetType())
                 select e).Count() == 0)
                throw new EndSimulationException("All trees have been removed from the forest.");
        }
    }
}
