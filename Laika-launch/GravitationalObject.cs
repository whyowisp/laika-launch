using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laika_launch
{
    public class GravitationalObject
    {

        public double GravityFactor { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public GravitationalObject(double x, double y, double gravityFactor)
        {
            X = x;
            Y = y;
            GravityFactor = gravityFactor;
        }
    }
}
