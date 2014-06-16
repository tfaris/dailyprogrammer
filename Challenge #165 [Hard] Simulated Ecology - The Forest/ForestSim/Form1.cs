//
//  A solution to reddit's /r/dailyprogrammer challenge #165 found here:
//  http://www.reddit.com/r/dailyprogrammer/comments/27h53e/662014_challenge_165_hard_simulated_ecology_the/
//
//  Author: Tom Faris <ta.faris@gmail.com>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ForestSim
{
    /// <summary>
    /// The main form.
    /// </summary>
    public partial class Form1 : Form
    {
        Forest _forest;
        ForestDrawer _drawer;
        Timer _simulationTimer;

        public Form1(Forest forest)
        {
            InitializeComponent();
            _forest = forest;

            _drawer = new ForestDrawer(forest);
            // Set up the simulation timer.
            _simulationTimer = new Timer();
            _simulationTimer.Tick += _simulationTimer_Tick;
            _simulationTimer.Interval = 100;
        }

        void _simulationTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                _forest.Tick();
            }
            catch (EndSimulationException ese)
            {
                // Simulation has ended for some reason or another.
                _simulationTimer.Stop();
                MessageBox.Show(ese.Message);
            }
            panelForest.Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _simulationTimer.Start();
        }

        private void panelForest_Paint(object sender, PaintEventArgs e)
        {
            _drawer.Draw(e.Graphics);
        }
    }
}
