using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace MOOC.Gameprogramming.Assignment._01
{
    /// <summary>
    /// ASSIGNMENT DESCRIPTION
    /// Imagine you’re working on developing a game similar to Scorched Earth, 
    /// a 2D tank-fighting game where tanks fire shells hoping to destroy the enemy tanks. 
    /// One of the challenges of the game is that the player has to predict where the shell will land after following a parabolic trajectory. 
    /// While you may not have taken physics, the equations are straightforward and 
    /// you should be able to develop a solution that will accurately calculate the shell path 
    /// given a starting shell speed and trajectory (angle of fire).
    /// </summary>
    class Program
    {
        /// <summary>
        /// acceleration due to gravity
        /// </summary>
        private const float g = (float)9.8;

        /// <summary>
        /// PI number
        /// </summary>
        private const float pi = (float)3.14159265;

        static void Main(string[] args)
        {
            //Print a “welcome” message to the user telling them that this application will calculate the maximum height of the shell and the distance it will travel along the ground
            Console.WriteLine("Welcome.");
            Console.WriteLine("This application will calculate the maximum height of the shell and the distance it will travel along the ground.");
            Console.WriteLine();

            //Prompt the user for the initial angle in degrees (ask for the angle, read the value, parse the value and then store the value)
            Console.Write("Firing angle: ");
            float theta = float.Parse(Console.ReadLine()) * pi / 180;

            //Prompt the user for the initial speed (ask for the speed, read the value, parse the value and then store the value)
            Console.Write("Firing speed: ");
            float speed = float.Parse(Console.ReadLine());
            Console.WriteLine();

            //Calculate vox using the Math Cos method
            float vox = speed * (float)Math.Cos(theta);

            //Calculate voy using the Math Sin method
            float voy = speed * (float)Math.Sin(theta);

            //Calculate t
            float t = voy / g;

            //Calculate h
            float h = voy * voy / (2 * g);

            //Calculate dx
            float dx = vox * 2 * t;

            //Print an appropriate message and the value for h
            Console.WriteLine("Maximum shell height: " + h.ToString("F3", CultureInfo.InvariantCulture) + " meters");

            //Print an appropriate message and the value for dx
            Console.WriteLine("Horizontal distance: " + dx.ToString("F3", CultureInfo.InvariantCulture) + " meters");
            Console.WriteLine();

        }
    }
}
