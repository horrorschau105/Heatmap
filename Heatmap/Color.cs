using System;
using System.Drawing;
namespace Heatmap
{
    public static class ColorHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="level">occurence of point in collection</param>
        /// <param name="maximum">maximal occurence of point in collection</param>
        /// <returns>grey color</returns>
        public static Color SetColor(int level, int maximum)
        {
            double alfa = 1 - 1.0 * level / maximum;
            int greyness = Convert.ToInt32(alfa * 255);
            return Color.FromArgb(greyness, greyness, greyness);
        }
    }
}