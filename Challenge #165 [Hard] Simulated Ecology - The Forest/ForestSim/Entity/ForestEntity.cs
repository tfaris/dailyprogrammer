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
using System.Drawing;

namespace ForestSim.Entity
{
    /// <summary>
    /// Represents an entity that exists in the forest.
    /// </summary>
    public abstract class ForestEntity
    {
        /// <summary>
        /// Get or set the age (in ticks) of the entity.
        /// </summary>
        public virtual int Age
        {
            get;
            set;
        }

        /// <summary>
        /// Get or set the world position of the entity.
        /// </summary>
        public virtual Point Location
        {
            get;
            set;
        }

        /// <summary>
        /// Get a color that represents the entity.
        /// </summary>
        public virtual Color Color
        {
            get { return Color.Transparent; }
        }

        /// <summary>
        /// Get whether the entity can be considered an empty space
        /// in the simulation.
        /// </summary>
        public virtual bool IsEmpty
        {
            get { return false; }
        }

        /// <summary>
        /// Get an array of entities adjacent to this entity in the specified forest.
        /// </summary>
        /// <param name="forest"></param>
        /// <returns></returns>
        protected virtual ForestEntity[] GetAdjacentEntities(Forest forest)
        {
            int x0 = Location.X - 1, y0 = Location.Y - 1,
                x1 = Location.X, y1 = Location.Y - 1,
                x2 = Location.X + 1, y2 = Location.Y - 1,

                x3 = Location.X - 1, y3 = Location.Y,
                // Skip over the current position, which is in the middle.
                x4 = Location.X + 1, y4 = Location.Y,

                x5 = Location.X - 1, y5 = Location.Y + 1,
                x6 = Location.X, y6 = Location.Y + 1,
                x7 = Location.X + 1, y7 = Location.Y + 1;
            return new ForestEntity[] {
                forest.GetEntityAtPosition(x0, y0),
                forest.GetEntityAtPosition(x1, y1),
                forest.GetEntityAtPosition(x2, y2),
                forest.GetEntityAtPosition(x3, y3),
                forest.GetEntityAtPosition(x4, y4),
                forest.GetEntityAtPosition(x5, y5),
                forest.GetEntityAtPosition(x6, y6),
                forest.GetEntityAtPosition(x7, y7) 
            };
        }

        /// <summary>
        /// Replace the specified old entity with the new in the specified forest.
        /// </summary>
        /// <param name="forest"></param>
        /// <param name="newEntity"></param>
        /// <param name="oldEntity"></param>
        protected virtual void ReplaceEntity(Forest forest, ForestEntity newEntity, ForestEntity oldEntity)
        {
            // Remove the old entity from its current location.
            forest.RemoveEntity(newEntity);
            newEntity.Location = oldEntity.Location;
            forest.AddEntity(newEntity);
        }

        /// <summary>
        /// Perform a game tick on the entity.
        /// </summary>
        /// <param name="forest"></param>
        /// <param name="currentTick"></param>
        public void Tick(Forest forest, long currentTick)
        {
            Age++;
            DoTick(forest, currentTick);
        }

        /// <summary>
        /// Override to provide the entities tick logic.
        /// </summary>
        /// <param name="currentTick"></param>
        protected abstract void DoTick(Forest forest, long currentTick);
    }
}
