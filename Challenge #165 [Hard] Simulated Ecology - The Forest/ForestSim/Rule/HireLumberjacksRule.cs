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
    /// A rule regarding the adding and removal of lumberjacks
    /// during the simulation.
    /// </summary>
    class HireLumberjacksRule : SimulationRule
    {
        Random _r = new Random();
        long _ruleAge = 0;

        public override void ProcessRule(Forest forest, long currentTick)
        {
            _ruleAge++;
            if (_ruleAge % Forest.YEAR_LENGTH == 0)
            {
                // Get all lumberjacks
                var lumberJacks =
                  from e in forest.Entities
                  where typeof(LumberJack).IsAssignableFrom(e.GetType())
                  select e as LumberJack;

                // Get the total number of logs collected from all lumberjacks
                int logSum = 0;
                lumberJacks.ToList().ForEach((e) => logSum += e.Logs);

                // If more logs have been collected than we have lumberjacks, we
                // might need to hire some people
                if (logSum >= lumberJacks.Count())
                {
                    int newHires = (logSum - lumberJacks.Count()) / 10;
                    for (int i = 0; i < newHires; i++)
                    {
                        ForestEntity randomEmpty = forest.GetRandomEmptyPosition();
                        LumberJack l = new LumberJack();
                        l.Location = randomEmpty.Location;
                        forest.AddEntity(l);
                    }
                }
                else
                {
                    // They're not working hard enough! Fire someone!
                    // (But make sure we have at least one person working for us...)
                    if (lumberJacks.Count() > 1)
                        forest.RemoveEntity(lumberJacks.First());
                }

                if (lumberJacks.Count() == 0)
                {
                    // No more lumberjacks. We have to make sure there's someone working.
                    LumberJack nl = new LumberJack();
                    nl.Location = forest.GetRandomEmptyPosition().Location;
                    forest.AddEntity(nl);
                }

                // Start of a new year... reset log counts for each lumberjack.
                lumberJacks.ToList().ForEach((e) => e.Logs = 0);
            }
        }
    }
}
