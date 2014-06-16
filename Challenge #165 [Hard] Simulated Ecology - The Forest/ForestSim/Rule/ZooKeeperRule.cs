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
    using Entity;

    /// <summary>
    /// A rule for controlling the bear population of the simulation.
    /// </summary>
    class ZooKeeperRule : SimulationRule
    {
        long _ruleAge = 0;
        bool _bearNextYear;

        public override void ProcessRule(Forest forest, long currentTick)
        {
            _ruleAge++;
            if (_ruleAge % Forest.YEAR_LENGTH == 0)
            {
                if (_bearNextYear)
                {
                    AddBear(forest);
                    // There were no bears last year. A new one has been born this year.
                    _bearNextYear = false;
                }

                var bears = from e in forest.Entities
                            where typeof(Bear).IsAssignableFrom(e.GetType())
                            select e as Bear;
                if (bears.Count() == 0)
                {
                    // No bears left this year. Add a new bear next year.
                    _bearNextYear = true;
                }
                else
                {
                    int maulCount = 0;
                    bears.ToList().ForEach((b) => maulCount += b.Maulings);
                    if (maulCount == 0)
                    {
                        // No maulings have occurred this year... add a new bear
                        // (because obviously someone out there doesn't like
                        // lumberjacks and is breeding bears)
                        AddBear(forest);
                    }
                    else
                    {
                        // If there's even one mauling, zookeepers come and remove a bear.
                        forest.RemoveEntity(bears.First());
                    }
                }
                // End of year. Reset number of maulings.
                bears.ToList().ForEach((e) => e.Maulings = 0);
            }
        }

        private void AddBear(Forest forest)
        {
            Bear newBear = new Bear();
            newBear.Location = forest.GetRandomEmptyPosition().Location;
            forest.AddEntity(newBear);
        }
    }
}
