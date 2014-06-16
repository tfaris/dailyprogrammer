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
    /// <summary>
    /// An error indicating that the simulation should be ended.
    /// </summary>
    class EndSimulationException : Exception
    {
        /// <summary>
        /// Create an instance of EndSimulationException.
        /// </summary>
        public EndSimulationException() : base() { }

        /// <summary>
        /// Create and instance of EndSimulationException with the specified
        /// message.
        /// </summary>
        /// <param name="message"></param>
        public EndSimulationException(string message) : base(message) { }
    }
}
