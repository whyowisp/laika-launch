using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laika_launch
{
    public class Laika
    {
        private double thrustForce = 0.02;
        private int minPropellant = 0;
        public double X { get; set; }
        public double Y { get; set; }
        public double VelocityX { get; set; }
        public double VelocityY { get; set; }
        public double Angle { get; set; }
        public int Propellant { get; set; }
        public int MinPropellant { get; }
        public double ThrustForce { get; }
        public Laika(double x, double y, double angle, int propellant)
        {
            X = x;
            Y = y;
            Angle = angle;
            Propellant = propellant;
            MinPropellant = minPropellant;
            ThrustForce = thrustForce;
        }
    }
}
