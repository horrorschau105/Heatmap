using System;
using System.Collections.Generic;
using System.Drawing;


namespace Heatmap
{
    public static class ColorHandler
    {
        public static Color SetColor(int level, int maximum)
        {
            double alfa = 1 - 1.0 * level / maximum;
            int greyness = Convert.ToInt32(alfa * 255);
            return Color.FromArgb(greyness, greyness, greyness);
        }
    }
}