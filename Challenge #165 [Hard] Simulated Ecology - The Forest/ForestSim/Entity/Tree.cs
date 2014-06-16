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
    /// Represents a grown simulated tree.
    /// </summary>
    class Tree : ForestEntity
    {
        Random _r = new Random();

        /// <summary>
        /// Get the probability per tick that a sapling will grow
        /// in an adjacent empty space.
        /// </summary>
        public virtual float SaplingProbability
        {
            get { return .1f; }
        }

        public override System.Drawing.Color Color
        {
            get
            {
                return System.Drawing.Color.Green;
            }
        }

        /// <summary>
        /// Attempt to spread a sapling from the tree.
        /// </summary>
        /// <param name="forest"></param>
        /// <param name="currentTick"></param>
        protected virtual void SpreadSapling(Forest forest, long currentTick)
        {
            // With a 10% chance, create a sapling at an adjacent empty space.
            if (_r.Next(100) / 100.0f < SaplingProbability)
            {
                ForestEntity[] adj = GetAdjacentEntities(forest);
                ForestEntity firstEmpty = adj.FirstOrDefault((e) => e.IsEmpty);
                if (firstEmpty != null)
                {
                    // First empty space. Create a sapling here.
                    Sapling sapling = new Sapling();
                    sapling.Location = firstEmpty.Location;
                    forest.AddEntity(sapling);
                }
            }
        }

        protected override void DoTick(Forest forest, long currentTick)
        {
            SpreadSapling(forest, currentTick);
            // After 10 years, a tree becomes an elder tree.
            if (Age >= Forest.YEAR_LENGTH * 10)
            {
                ElderTree elder = new ElderTree();
                elder.Location = Location;
                forest.AddEntity(elder);
            }
        }

        /// <summary>
        /// Chop down the tree, returning the number of logs
        /// received.
        /// </summary>
        /// <returns></returns>
        public virtual int Chop()
        {
            return 1;
        }
    }
}
