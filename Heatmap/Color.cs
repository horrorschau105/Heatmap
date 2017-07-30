using System;
using System.Collections.Generic;
using System.Drawing;


namespace Heatmap
{
    public static class Others // greatest shit
    {
        public static Colour get_color(int level, int maximum)
        {
            double alfa = level / maximum;
            if (level == 0) return new Colour();
            //return new Colour(Convert.ToInt32(alfa * 255),
           //                    Convert.ToInt32(alfa * 255),
            //                   Convert.ToInt32(alfa * 255));
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
       /* public override string ToString()
        {
            return string.Format("R: {0}, G: {1}, B: {2}", r, g, b);
        }*/
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

}