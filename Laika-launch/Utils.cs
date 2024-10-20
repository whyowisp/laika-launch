using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Laika_launch
{
    public static class Utils
    {
        public static double G { get; set; }
        public static double DeltaV { get; set; }
        public static double CalculateDeltaV(double coordinate, double distance) // Laskee ja palauttaa nopeuden muutoksen.
        {
            if (distance != 0) // Nollalla ei voi jakaa
            {            
            G = ((-1) / Math.Pow(distance, 2)) * 10; // Gravitaatiovoima etäisyyden funktiona. Aina negatiivinen
            DeltaV = coordinate * G; 
            }
         return DeltaV;
        }
    }
}
