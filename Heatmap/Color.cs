using System;
using System.Collections.Generic;
using System.Drawing;


namespace Heatmap
{
    public static class Others
    {
        public static Colour get_color(int level, int maximum)
        {
            double alfa = level;
            alfa /= maximum;
            if (level == 0) return new Colour();
            if (level < maximum * 1 / 3)
                return (new Colour(0, 0, 255)).Update(new Colour(0, 255, 0), alfa);
            if (level < maximum * 2 / 3)
                return (new Colour(0, 255, 0)).Update(new Colour(255, 255, 0), alfa);
            else
                return (new Colour(255, 255, 0)).Update(new Colour(255, 0, 0), alfa);
        }

    }

    public class Colour
    {
        public int r, g, b;
        public Colour(int r = 255, int g = 255, int b = 255)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }
        public override string ToString()
        {
            return string.Format("R: {0}, G: {1}, B: {2}", r, g, b);
        }
        public Colour Update(Colour c, double alfa)
        {

            return new Colour
                (r + Convert.ToInt32(Math.Abs(c.r - r) * alfa),
                g + Convert.ToInt32(Math.Abs(c.g - g) * alfa),
                b + Convert.ToInt32(Math.Abs(c.b - b) * alfa));


        }
        public Color transform()
        {

            return Color.FromArgb(r > 255 ? 255 : r, g > 255 ? 255 : g, b > 255 ? 255 : b);
        }
    }
    public class CONST
    {
        public const int DEEP_OF_SEARCHING = 3;
        public const int MARGIN = 5;
        public const int ZOOM = 3000; // size of picture (max 20000)
        public const int WIDTH_OF_ROUTE = 2; // don't change, it's enough (max 2)
    }
    public static class Extensions
    {
        private static void AddOrIncrement(this Dictionary<KeyValuePair<int, int>, int> dict, KeyValuePair<int, int> p)
        {
            if (dict.ContainsKey(p)) dict[p] += 1;
            else dict[p] = 1;
        }
        public static void AddAllNearBy(this Dictionary<KeyValuePair<int, int>, int> dict, KeyValuePair<int, int> p)
        {
            for (int i = -CONST.WIDTH_OF_ROUTE; i <= CONST.WIDTH_OF_ROUTE; ++i)
            {
                for (int j = -CONST.WIDTH_OF_ROUTE; j <= CONST.WIDTH_OF_ROUTE; ++j)
                {
                    dict.AddOrIncrement(new KeyValuePair<int, int>(p.Key + i, p.Value + j));
                }
            }
        }
        public static void AddPointsBetween
            (this Dictionary<KeyValuePair<int, int>, int> dict, KeyValuePair<int, int> p1, KeyValuePair<int, int> p2, int deep)
        {
            if (deep == 0) return;
            KeyValuePair<int, int> mid = new KeyValuePair<int, int>
                ((p1.Key + p2.Key) / 2, (p1.Value + p2.Value) / 2);
            dict.AddAllNearBy(mid);
            dict.AddPointsBetween(p1, mid, deep - 1);
            dict.AddPointsBetween(mid, p2, deep - 1);

        }
    }

}