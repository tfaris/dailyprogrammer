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
    using Rule;

    /// <summary>
    /// Represents the simulated forest.
    /// </summary>
    public class Forest
    {
        ForestEntity[,] _entities;
        List<SimulationRule> _rules;
        Random _random = new Random();
        int _width, _height;
        long _currentTick;

        public const int YEAR_LENGTH = 12;

        /// <summary>
        /// Get the current simulation tick.
        /// </summary>
        public long CurrentTick
        {
            get { return _currentTick; }
        }

        /// <summary>
        /// Get the width of the forest.
        /// </summary>
        public int Width
        {
            get { return _width; }
        }

        /// <summary>
        /// Get the height of the forest.
        /// </summary>
        public int Height
        {
            get { return _height; }
        }

        /// <summary>
        /// Get a read-only collection of entities in this forest.
        /// </summary>
        public System.Collections.ObjectModel.ReadOnlyCollection<ForestEntity> Entities
        {
            get
            {
                List<ForestEntity> entities = new List<ForestEntity>();
                for (int x = 0; x < Width; x++)
                {
                    for (int y = 0; y < Height; y++)
                    {
                        entities.Add(_entities[x, y]);
                    }
                }
                return entities.AsReadOnly();
            }
        }

        /// <summary>
        /// Create a forest of specified width and height.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Forest(int width, int height)
        {
            _entities = new ForestEntity[width, height];
            // Fill the array with empty entities.            
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Meadow m = new Meadow();
                    m.Location = new System.Drawing.Point(x, y);
                    _entities[x, y] = m;
                }
            }
            _rules = new List<SimulationRule>();
            _width = width;
            _height = height;
        }

        /// <summary>
        /// Add an entity to the forest.
        /// </summary>
        /// <param name="entity"></param>
        public void AddEntity(ForestEntity entity)
        {
            _entities[entity.Location.X, entity.Location.Y] = entity;
        }

        /// <summary>
        /// Remove an entity from the forest.
        /// </summary>
        /// <param name="entity"></param>
        public void RemoveEntity(ForestEntity entity)
        {
            // Replace the existing entity with an empty one.
            Meadow m = new Meadow();
            m.Location = entity.Location;
            _entities[entity.Location.X, entity.Location.Y] = m;
        }

        /// <summary>
        /// Add the specified simulation rule.
        /// </summary>
        /// <param name="rule"></param>
        public void AddRule(SimulationRule rule)
        {
            _rules.Add(rule);
        }

        /// <summary>
        /// Remove the specified simulation rule.
        /// </summary>
        /// <param name="rule"></param>
        public void RemoveRule(SimulationRule rule)
        {
            _rules.Add(rule);
        }

        /// <summary>
        /// Move the entity to the specified position.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void MoveEntity(ForestEntity entity, int x, int y)
        {
            // Remove from its current position
            RemoveEntity(entity);
            _entities[x, y] = entity;
            entity.Location = new System.Drawing.Point(x, y);
        }

        /// <summary>
        /// Get the entity at the specified position.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public ForestEntity GetEntityAtPosition(int x, int y)
        {
            // Check boundaries and wrap around. Our forest is apparently
            // a magical endless forest.
            if (x < 0)
                x = Width - 1;
            else if (x >= Width)
                x = 0;
            if (y < 0)
                y = Height - 1;
            else if (y >= Height)
                y = 0;
            return _entities[x, y];
        }

        /// <summary>
        /// Return a randomly selected empty position in the forest.
        /// </summary>
        /// <returns></returns>
        public ForestEntity GetRandomEmptyPosition()
        {
            do
            {
                int x = _random.Next(Width), y = _random.Next(Height);
                if (_entities[x, y].IsEmpty)
                    return _entities[x, y];
            }
            while (true);
        }

        /// <summary>
        /// Get the number of simulated months that the simulation has
        /// ran.
        /// </summary>
        /// <returns></returns>
        public virtual long GetMonths()
        {
            return _currentTick;
        }

        /// <summary>
        /// Perform a single simulation tick.
        /// </summary>
        public void Tick()
        {
            _currentTick++;
            foreach (ForestEntity entity in _entities)
                entity.Tick(this, _currentTick);
            foreach (SimulationRule rule in _rules)
                rule.ProcessRule(this, _currentTick);
        }
    }
}
