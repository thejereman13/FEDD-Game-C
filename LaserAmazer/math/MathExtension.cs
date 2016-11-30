using System;

namespace LaserAmazer.Math
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
            return (System.Math.PI / 180) * val;
        }

        public static double Hypotenuse(float a, float b)
        {
            return System.Math.Sqrt(System.Math.Pow(a, 2) + System.Math.Pow(b, 2));
        }
    }
}
