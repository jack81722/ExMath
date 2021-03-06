﻿using System;
using ExMath.Coordinate;

namespace ExMath
{
    public static class Random
    {
        private static System.Random _random = new System.Random();
        private static int randomCount = 0;
        private const int resetRandom = 1000;
        private static System.Random random
        {
            get
            {
                if (randomCount > resetRandom)
                {
                    randomCount = 0;
                    _random = new System.Random();
                }
                randomCount++;
                return _random;
            }
        }

        public static double Next()
        {
            return random.NextDouble();
        }

        public static int Next(int min, int max)
        {
            if (min > max)
            {
                var tmp = min;
                min = max;
                max = tmp;
            }
            double r = random.NextDouble();
            return min + (int)(r * (max - min));
        }

        public static double NextGaussian(double mu = 0, double sigma = 1)
        {
            return random.NextGaussian(mu, sigma);
        }

        public static double NextGaussian(this System.Random r, double mu = 0, double sigma = 1)
        {
            var u1 = r.NextDouble();
            var u2 = r.NextDouble();

            var rand_std_normal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                                Math.Sin(2.0 * Math.PI * u2);

            var rand_normal = mu + sigma * rand_std_normal;

            return rand_normal;
        }

        #region Coordinate Random Methods
        public static Vector2 insideUnitCircle
        {
            get
            {
                double angle = random.NextDouble() * System.Math.PI * 2;
                double x = System.Math.Cos(angle);
                double y = System.Math.Sin(angle);
                return new Vector2((float)x, (float)y) * Math.Sqrt(random.NextDouble());
            }
        }

        public static Vector3 insideUnitSphere
        {
            get
            {
                double u = random.NextDouble();
                Vector3 v = new Vector3(random.NextGaussian(), random.NextGaussian(), random.NextGaussian());
                v = v * Math.Pow(u, 1f / 3f);
                return v;
            }
        }

        public static Vector2 GetCirclePoint(float min, float max)
        {
            if (min > max)
            {
                var tmp = min;
                min = max;
                max = tmp;
            }
            double angle = random.NextDouble() * System.Math.PI * 2;
            double x = System.Math.Cos(angle);
            double y = System.Math.Sin(angle);
            return new Vector2((float)x, (float)y) * (min + Math.Sqrt(random.NextDouble()) * (max - min));
        }

        public static Vector3 GetSpherePoint(float min, float max)
        {
            if (min > max)
            {
                var tmp = min;
                min = max;
                max = tmp;
            }
            double u = random.NextDouble();
            Vector3 v = new Vector3(random.NextGaussian(), random.NextGaussian(), random.NextGaussian());
            double r = min + Math.Pow(u, 1f / 3f) * (max - min);
            return v * r;
        }

        public static Vector3 RandomInCircleOnXZ(float radius)
        {
            Vector2 v = insideUnitCircle;
            return new Vector3(v.x * radius, 0, v.y * radius);
        }

        public static Vector3 RandomOnCircleOnXZ(float radius)
        {
            double a = random.NextDouble() * 2 * Math.PI;
            return new Vector3((float)(radius * Math.Cos(a)), 0, (float)(radius * Math.Sin(a)));
        }

        public static Vector3 RandomInSphere(float radius)
        {
            return insideUnitSphere * radius;
        }

        public static Vector3 RandomOnSphere(float radius)
        {
            double x = random.NextGaussian(), y = random.NextGaussian(), z = random.NextGaussian();
            Vector3 v = new Vector3((float)x, (float)y, (float)z);
            return v.normalized * radius;
        }
        #endregion

        public static double Range(double min, double max)
        {
            if (min > max)
                throw new InvalidOperationException("Min cannot greater than max");
            return random.NextDouble() * (max - min) + min;
        }

        public static float Range(float min, float max)
        {
            if (min > max)
                throw new InvalidOperationException("Min cannot greater than max");
            return (float)random.NextDouble() * (max - min) + min;
        }

        public static int Range(int min, int max)
        {
            return random.Next(min, max);
        }

        public static void Shuffle<T>(params T[] array)
        {
            for(var i = 0; i < array.Length; i++)
            {
                var r = Range(i, array.Length);
                var temp = array[i];
                array[i] = array[r];
                array[r] = temp;
            }
        }
    }
}