using System;

namespace LaserAmazer.math
{
    class MathExtension
    {
        public static int RandomInt(int min, int max)
        {
            Random random = new Random();
            return random.Next() * (max - min + 1) + min;
        }

        public static double ToRadians(double val)
        {
            return (Math.PI / 180) * val;
        }

        public static double Hypotenuse(float a, float b)
        {
            return Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));
        }
    }
}
