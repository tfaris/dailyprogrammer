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
    /// A rule taht will end the simulation after a certain amount of time.
    /// </summary>
    class TimeoutRule : SimulationRule
    {
        long _ruleAge;
        const int MAX_YEARS = 400;

        public override void ProcessRule(Forest forest, long currentTick)
        {
            if (_ruleAge / Forest.YEAR_LENGTH == MAX_YEARS)
                throw new EndSimulationException(string.Format("It's been {0} years. Simulation over.", MAX_YEARS));
            _ruleAge++;
        }
    }
}
