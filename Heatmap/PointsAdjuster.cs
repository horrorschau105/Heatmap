using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heatmap
{
    /// <summary>
    /// Manipulations with points come here
    /// </summary>
    public static class PointsAdjuster
    {
        public static List<KeyValuePair<int, int>> Adjust(List<KeyValuePair<double, double>> incomingPoints)
        {
            List<KeyValuePair<int, int>> result = new List<KeyValuePair<int, int>>();
            foreach(var pair in incomingPoints)
            {
                result.Add(new KeyValuePair<int, int>(Convert.ToInt32(Constants.ZOOM * pair.Key), Convert.ToInt32(Constants.ZOOM * pair.Value)));
            } // map it, try in one line?
            return result;
        }

    }
}
