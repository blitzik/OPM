using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ExtensionMethods
{
    public static class RandomExtensions
    {
        public static double NextDouble(this Random random, double min, double max)
        {
            // 0.2 * (6.0 - 2.5) + 2.5
            // 
            return min + (random.NextDouble() * (max - min));
        }


        public static int NextInt32(this Random rng)
        {
            int firstBits = rng.Next(0, 1 << 4) << 28;
            int lastBits = rng.Next(0, 1 << 28);
            return firstBits | lastBits;
        }


        public static decimal NextDecimal(this Random rng)
        {
            byte scale = (byte)rng.Next(29);
            bool sign = rng.Next(2) == 1;
            return new decimal(rng.NextInt32(),
                               rng.NextInt32(),
                               rng.NextInt32(),
                               sign,
                               scale);
        }


        public static decimal NextDecimal(this Random rng, decimal min, decimal max)
        {
            return min + ((decimal)rng.NextDouble() * (max - min));
        }
    }
}
