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
    /// Represents a simulated bear.
    /// </summary>
    class Bear : ForestEntity
    {
        Random _r = new Random();
        int _movesPerMonth = 5;

        public override System.Drawing.Color Color
        {
            get
            {
                return System.Drawing.Color.Brown;
            }
        }

        /// <summary>
        /// Get the number of times this bear has mauled someone.
        /// </summary>
        public int Maulings
        {
            get;
            set;
        }

        protected override void DoTick(Forest forest, long currentTick)
        {
            for (int i = 0; i < _movesPerMonth; i++)
            {
                ForestEntity[] adj = GetAdjacentEntities(forest);
                ForestEntity pickedEntity = adj[_r.Next(adj.Length)];
                // Bears will maw lumberjacks if they come across them.
                if (typeof(LumberJack).IsAssignableFrom(pickedEntity.GetType()))
                {
                    LumberJack l = (LumberJack)pickedEntity;
                    Maulings++;
                    ReplaceEntity(forest, this, l);
                    break;
                }
                else if (pickedEntity.IsEmpty)
                    // Move here if its an empty location.
                    ReplaceEntity(forest, this, pickedEntity);
            }
        }
    }
}
