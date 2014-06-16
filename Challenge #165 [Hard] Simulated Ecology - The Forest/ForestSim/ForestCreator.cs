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
    using Entity;

    /// <summary>
    /// The default forest creator. Creates a forest based on the original challenge rules.
    /// </summary>
    class ForestCreator
    {
        Random r = new Random();

        /// <summary>
        /// Create a forest of the specified width and height.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public Forest CreateForest(int width, int height)
        {
            Forest f = new Forest(width, height);
            
            int area = width * height;

            // Forest is primed with 10% lumberjacks...
            for (int i = 0; i < area * .1; i++)
                f.AddEntity(DetermineStartLocation(f, new LumberJack()));

            // 50% trees...
            for (int i = 0; i < area * .5; i++)
                f.AddEntity(DetermineStartLocation(f, new Tree()));

            // 2% bears...
            for (int i = 0; i < area * .02; i++)
                f.AddEntity(DetermineStartLocation(f, new Bear()));

            // Remaining 38% is empty (grass? ents?)
            
            // Add rules
            f.AddRule(new Rule.HireLumberjacksRule());
            f.AddRule(new Rule.ZooKeeperRule());
            f.AddRule(new Rule.NoTreesRule());
            f.AddRule(new Rule.TimeoutRule());

            return f;
        }

        /// <summary>
        /// Determine the starting position for the specified entity in the specified forest.
        /// </summary>
        /// <param name="forest"></param>
        /// <param name="entity"></param>
        protected virtual ForestEntity DetermineStartLocation(Forest forest, ForestEntity entity)
        {
            // Probably not the best algorithm.. potential for infinite loop if the forest is
            // already full.
            do
            {
                int x = r.Next(forest.Width),
                    y = r.Next(forest.Height);
                // Look for an existing entity in the chosen random position.
                ForestEntity existingInPosition = forest.GetEntityAtPosition(x, y);
                if (existingInPosition == null || existingInPosition.IsEmpty)
                {
                    // If position is empty, assign it and return.
                    entity.Location = new System.Drawing.Point(x, y);
                    return entity;
                }
            }
            while (true);
        }
    }
}
