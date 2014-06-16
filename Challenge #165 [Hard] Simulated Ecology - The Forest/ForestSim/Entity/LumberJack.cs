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
    /// Represents a simulated lumberjack.
    /// </summary>
    class LumberJack : ForestEntity
    {
        int _logs;
        Random _r = new Random();
        const int _movesPerMonth = 3;

        public override System.Drawing.Color Color
        {
            get
            {
                return System.Drawing.Color.Orange;
            }
        }

        /// <summary>
        /// Get the number of logs this lumberjack has collected.
        /// </summary>
        public int Logs
        {
            get { return _logs; }
            set { _logs = 0; }
        }

        protected override void DoTick(Forest forest, long currentTick)
        {
            for (int i=0; i < _movesPerMonth; i++){
                ForestEntity[] adj = GetAdjacentEntities(forest);
                ForestEntity pickedEntity = adj[_r.Next(adj.Length)];
                // Lumberjacks will chop down trees, but only if they're not saplings.
                if (typeof(Tree).IsAssignableFrom(pickedEntity.GetType()) && !(pickedEntity is Sapling))
                {
                    Tree tree = (Tree)pickedEntity;
                    _logs += tree.Chop();
                    ReplaceEntity(forest, this, tree);
                    break;
                }
                else if (pickedEntity.IsEmpty)
                    // Move here if its an empty location.
                    ReplaceEntity(forest, this, pickedEntity);
            }
        }
    }
}
